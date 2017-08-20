using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SquareWorld.Engine;
using SquareWorld.Frontend.GameObjects;

namespace SquareWorld.Frontend
{
    class GL4Frontend : GameWindow, IFrontend
    {
        private static readonly string _title = "Square Game";
        private int _program;
        private readonly Game _game;
        private List<GameObjectRenderer> _gameObjectRenderers = new List<GameObjectRenderer>();
        private int _viewLoc;
        private int _scaleLoc;
        private Matrix4 _view;
        private Matrix4 _scale;

        public GameObjectsFactory GameObjectsFactory {get; private set;}
        public GL4Frontend(Game game)
            : base(
                512,
                512,
                GraphicsMode.Default,
                _title,
                GameWindowFlags.Default,
                DisplayDevice.Default,
                4,
                0,
                GraphicsContextFlags.ForwardCompatible)
        {
            _game = game;
            _program = CompileShaders();
            GameObjectsFactory = new GameObjectsFactory(_program);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.UseProgram(_program);

            _scaleLoc = GL.GetUniformLocation(_program, "scale");
            _viewLoc = GL.GetUniformLocation(_program, "view");
            _scale = Matrix4.CreateScale(1.0f/(_game.WorldSize/2), 1.0f/(_game.WorldSize/2), 1.0f);
            // Move (0, 0) to left bottom corner of camera view
            _view = Matrix4.CreateTranslation(-1, -1, 0.0f);

            _gameObjectRenderers.AddRange(GameObjectsFactory.Renderers);
            _gameObjectRenderers.ForEach(r => r.Load());

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
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
            UpdateTitleFps(e.Time);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UniformMatrix4(_viewLoc, transpose: false, matrix: ref _view);
            GL.UniformMatrix4(_scaleLoc, transpose: false, matrix: ref _scale);

            //_gameObjectRenderer.Render(0, 1, 1);
            //_gameObjectRenderer.Render(0, 0, 0);
            _game.Render();

            SwapBuffers();
        }

        private double summFps = 0;
        private int summ = 0;

        void UpdateTitleFps(double time)
        {
            if (summ++ == 60)
            {
                Title = $"{_title}: (Vsync: {VSync}) FPS: {summFps / 60:N1}";
                summ = 1;
                summFps = 1f / time;
            }
            else
            {
                summFps += 1f / time;
            }
        }

        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            var vertexShaderStr = 
            @"#version 420 core
            layout(location = 0) in vec2 vPosition;
            layout(location = 1) in vec2 textureCoordinate;

            out vec2 vs_textureCoordinate;

            uniform mat4 scale;
            uniform mat4 model;
            uniform mat4 view;

            void
            main()
            {
                vs_textureCoordinate = textureCoordinate;
                gl_Position = view * scale * model * vec4(vPosition, 0.0f, 1.0f);
            }";
            GL.ShaderSource(vertexShader, vertexShaderStr);
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            var fragmentShaderStr = 
            @"#version 420 core
            in vec2 vs_textureCoordinate;
            uniform sampler2D textureObject;
            out vec4 color;
            void
            main()
            {
                //color = vec4(1.0f, 0.0f, 0.0f, 1.0f);
                color = texture(textureObject, vs_textureCoordinate);
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