using System;
using ImageSharp;

namespace SquareWorld.Frontend
{
    public class TextureLoader
    {
        public float[] Load(out int Width, out int Height)
        {
            using (Image<Rgba32> image = Image.Load("void.png"))
            {
                Width = image.Width;
                Height = image.Height;
                float[] rc = new float[Width*Height*sizeof(float)];

                for (int i = 0; i < Width*Height; i++)
                {
                    rc[i * sizeof(float) + 0] = image.Pixels[i].R/255.0f;
                    rc[i * sizeof(float) + 1] = image.Pixels[i].G/255.0f;
                    rc[i * sizeof(float) + 2] = image.Pixels[i].B/255.0f;
                    rc[i * sizeof(float) + 3] = image.Pixels[i].A/255.0f;
                }

                return rc;
            }
        }
    }
}