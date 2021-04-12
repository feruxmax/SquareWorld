using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace SquareWorld.Frontend
{
    public class TextureLoader
    {
        private const string ResourcesPath = "resources/";

        public float[] Load(string fileName, out int Width, out int Height)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(ResourcesPath + fileName))
            {
                Width = image.Width;
                Height = image.Height;
                float[] rc = new float[Width * Height * sizeof(float)];

                image.Mutate(x => x.Flip(FlipMode.Vertical));

                for (int i = 0; i < Height; i++)
                {
                    Span<Rgba32> span = image.GetPixelRowSpan(i);
                    for (int j = 0; j < Width * Height; j++)
                    {
                        var index = i * Height + j;
                        rc[index * sizeof(float) + 0] = span[j].R / 255.0f;
                        rc[index * sizeof(float) + 1] = span[i].G / 255.0f;
                        rc[index * sizeof(float) + 2] = span[i].B / 255.0f;
                        rc[index * sizeof(float) + 3] = span[i].A / 255.0f;
                    }
                }

                return rc;
            }
        }
    }
}