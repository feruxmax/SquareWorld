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

        private int _vao;
        private int _buffer;
        private bool _initialized;

        //
        private int _modelMatrixLocation;
        private Matrix4 _modelMatrix;

        public GameObjectRenderer(int modelMatrixLocation, Matrix4 modelMatrix)
        {
            _modelMatrixLocation = modelMatrixLocation;
            _modelMatrix = modelMatrix;

            var vertices = new float[NumVertices * NumDimensions]
            {
                1.0f, 1.0f, // Triangle 1
                1.0f, 0.0f,
                0.0f, 1.0f,
                1.0f, 0.0f, // Triangle 2
                0.0f, 0.0f,
                0.0f, 1.0f,
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

        public void Render(int type, int x, int y)
        {
            var model = Matrix4.CreateTranslation(x, y, 0) * _modelMatrix;
            GL.UniformMatrix4(_modelMatrixLocation, transpose: false, matrix: ref model);

            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, first:0, count: NumVertices);
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