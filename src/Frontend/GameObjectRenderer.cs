using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace SquareWorld.Frontend
{
    public class GameObjectRenderer : IDisposable
    {
        enum AttribIds : int { vPosition = 0 };
        private const int NumDimensions = 2;
        private const int NumVertices = 6;

        private bool _initialized;

        private int _vao;
        private int _buffer;
        public GameObjectRenderer()
        {
            var vertices = new float[NumVertices * NumDimensions]
            {
                -0.90f, -0.90f, // Triangle 1
                +0.85f, -0.90f,
                -0.90f, +0.85f,
                +0.90f, -0.85f, // Triangle 2
                +0.90f, +0.90f,
                -0.85f, +0.90f,
            };

            _vao = GL.GenVertexArray();
            _buffer = GL.GenBuffer();

            GL.BindVertexArray(_vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
            GL.NamedBufferStorage(_buffer, NumVertices*NumDimensions*sizeof(float), vertices, flags: 0);

            GL.VertexArrayAttribBinding(_vao, (int)AttribIds.vPosition, 0);
            GL.EnableVertexArrayAttrib(_vao, (int)AttribIds.vPosition);
            GL.VertexArrayAttribFormat(
                _vao,
                (int)AttribIds.vPosition,
                size: NumDimensions,
                type: VertexAttribType.Float,
                normalized: false,
                relativeoffset: 0);

            GL.VertexArrayVertexBuffer(_vao, 0, _buffer, offset:IntPtr.Zero, stride:0);

            _initialized = true;
        }

        public void Render()
        {
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, first:0, count: NumVertices);
        }

        public void Draw(int type, int x, int y)
        {

        }
        
        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(_initialized)
                {
                    GL.DeleteVertexArray(_vao);
                    GL.DeleteBuffer(_buffer);
                    _initialized = false;
                }
            }
        }
    }
}