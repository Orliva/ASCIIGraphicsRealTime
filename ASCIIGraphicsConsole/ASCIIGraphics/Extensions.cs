﻿using System.Drawing;

namespace ASCIIGraphics
{
    internal static class Extensions
    {
        public static void ToGray(this Bitmap bm)
        {
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    var pixel = bm.GetPixel(x, y);
                    int sr = (pixel.R + pixel.G + pixel.B) / 3;
                    bm.SetPixel(x, y, Color.FromArgb(pixel.A, sr, sr, sr));
                }
            }
        }
    }
}
