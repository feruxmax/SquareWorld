using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SquareWorld.Engine;

namespace SquareWorld.Frontend
{
    class GL4Frontend : GameWindow, IFrontend
    {
        private int _program;
        private  GameObjectRenderer _gameObjectRenderer;
        private readonly Game  _game;
        private int _viewLoc;
        private Matrix4 _view;

        public GL4Frontend(Game game)
            : base(
                512,
                512,
                GraphicsMode.Default,
                "Square Game",
                GameWindowFlags.Default,
                DisplayDevice.Default,
                4,
                0,
                GraphicsContextFlags.ForwardCompatible)
        {
            _game = game;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _program = CompileShaders();
            GL.UseProgram(_program);

            var modelLoc = GL.GetUniformLocation(_program, "model");
            _viewLoc = GL.GetUniformLocation(_program, "view");
            var model = Matrix4.CreateScale(1.0f/(_game.WorldSize/2), 1.0f/(_game.WorldSize/2), 1.0f);
            // Move (0, 0) to left bottom corner of camera view
            _view = Matrix4.CreateTranslation(-1, -1, 0.0f);

            _gameObjectRenderer = new GameObjectRenderer(modelLoc, model);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.ClearColor(Color.Azure);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UniformMatrix4(_viewLoc, transpose: false, matrix: ref _view);

            //_gameObjectRenderer.Render(0, 1, 1);
            //_gameObjectRenderer.Render(0, 0, 1);
            _game.Render(_gameObjectRenderer);

            SwapBuffers();
        }

        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            var vertexShaderStr = 
            @"#version 420 core
            layout(location = 0) in vec4 vPosition;

            uniform mat4 model;
            uniform mat4 view;

            void
            main()
            {
                gl_Position = view * model * vPosition;
            }";
            GL.ShaderSource(vertexShader, vertexShaderStr);
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            var fragmentShaderStr = 
            @"#version 420 core
            out vec4 fColor;
            void
            main()
            {
                fColor = vec4(0.5, 0.4, 0.8, 1.0);
            }";
            GL.ShaderSource(fragmentShader, fragmentShaderStr);
            GL.CompileShader(fragmentShader);

            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return program;
        }
    }
}