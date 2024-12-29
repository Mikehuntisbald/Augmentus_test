using System;
using System.Drawing;

namespace EdgeDetectionBackend.EdgeDetection
{
    public class Sobel
    {
        public static Bitmap ApplySobel(Bitmap inputImage)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;

            Bitmap grayImage = ConvertToGrayscale(inputImage);
            Bitmap outputImage = new Bitmap(width, height);

            int[,] gx = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            int[,] grayscaleMatrix = new int[width, height];

            // Precompute grayscale values for performance
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = grayImage.GetPixel(x, y);
                    grayscaleMatrix[x, y] = pixelColor.R;
                }
            }

            // Perform Sobel operation
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int pixelX = 0;
                    int pixelY = 0;

                    for (int j = -1; j <= 1; j++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            int intensity = grayscaleMatrix[x + i, y + j];
                            pixelX += intensity * gx[i + 1, j + 1];
                            pixelY += intensity * gy[i + 1, j + 1];
                        }
                    }

                    int magnitude = (int)Math.Sqrt(pixelX * pixelX + pixelY * pixelY);
                    magnitude = Math.Min(255, magnitude);

                    outputImage.SetPixel(x, y, Color.FromArgb(magnitude, magnitude, magnitude));
                }
            }

            return outputImage;
        }

        private static Bitmap ConvertToGrayscale(Bitmap inputImage)
        {
            Bitmap grayImage = new Bitmap(inputImage.Width, inputImage.Height);

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    Color pixelColor = inputImage.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    grayImage.SetPixel(x, y, Color.FromArgb(grayValue, grayValue, grayValue));
                }
            }

            return grayImage;
        }
    }
}
