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
        private  GameObjectRenderer _gameObjectRenderer;
        private readonly Game  _game;

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

            var program = CompileShaders();
            GL.UseProgram(program);

            _gameObjectRenderer = new GameObjectRenderer();

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

            _gameObjectRenderer.Render();

            SwapBuffers();
        }

        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            var vertexShaderStr = 
            @"#version 420 core
            layout(location = 0) in vec4 vPosition;
            void
            main()
            {
                gl_Position = vPosition;
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