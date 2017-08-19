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
                1.0f,  1.0f, // Triangle 1
                1.0f, -1.0f,
               -1.0f,  1.0f,
                1.0f, -1.0f, // Triangle 2
               -1.0f, -1.0f,
               -1.0f,  1.0f,
            };

            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            _buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
            GL.NamedBufferStorage(_buffer, NumVertices*NumDimensions*sizeof(float), vertices, flags: 0);

            GL.VertexAttribPointer((int)AttribIds.vPosition, size:NumDimensions, 
                type: VertexAttribPointerType.Float, normalized: false, stride: 0, offset: 0);
            GL.EnableVertexAttribArray((int)AttribIds.vPosition);

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