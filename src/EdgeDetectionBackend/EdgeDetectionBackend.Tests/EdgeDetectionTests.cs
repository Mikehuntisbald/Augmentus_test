using NUnit.Framework;
using System.Drawing;
using EdgeDetectionBackend.EdgeDetection;
using System.IO;

namespace EdgeDetectionBackend.Tests
{
    public class EdgeDetectionTests
    {
        private Bitmap mockImage;

        [SetUp]
        public void Setup()
        {
            // Create a 3x3 mock grayscale image for testing
            mockImage = new Bitmap(3, 3);
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    int intensity = x + y;
                    mockImage.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }
        }

        [Test]
        public void TestOperatorSelection_Sobel()
        {
            // Test if Sobel operator is correctly selected based on user input
            int userChoice = 1;

            Assert.DoesNotThrow(() =>
            {
                Bitmap output = userChoice switch
                {
                    1 => Sobel.ApplySobel(mockImage),
                    2 => Prewitt.ApplyPrewitt(mockImage),
                    _ => throw new InvalidDataException("Invalid operator choice")
                };
            });
        }

        [Test]
        public void TestOperatorSelection_Prewitt()
        {
            // Test if Prewitt operator is correctly selected based on user input
            int userChoice = 2;

            Assert.DoesNotThrow(() =>
            {
                Bitmap output = userChoice switch
                {
                    1 => Sobel.ApplySobel(mockImage),
                    2 => Prewitt.ApplyPrewitt(mockImage),
                    _ => throw new InvalidDataException("Invalid operator choice")
                };
            });
        }

        [Test]
        public void TestSobelEdgeDetection_OutputDimensions()
        {
            // Test if Sobel output dimensions match input dimensions
            Bitmap output = Sobel.ApplySobel(mockImage);
            Assert.That(output.Width, Is.EqualTo(mockImage.Width), "Width does not match");
            Assert.That(output.Height, Is.EqualTo(mockImage.Height), "Height does not match");
        }

        [Test]
        public void TestPrewittEdgeDetection_OutputDimensions()
        {
            // Test if Prewitt output dimensions match input dimensions
            Bitmap output = Prewitt.ApplyPrewitt(mockImage);
            Assert.That(output.Width, Is.EqualTo(mockImage.Width), "Width does not match");
            Assert.That(output.Height, Is.EqualTo(mockImage.Height), "Height does not match");
        }

        [Test]
        public void TestEdgeDetection_NoExceptions()
        {
            // Ensure no exceptions are thrown during Sobel and Prewitt operations
            Assert.DoesNotThrow(() => Sobel.ApplySobel(mockImage), "Sobel threw an exception");
            Assert.DoesNotThrow(() => Prewitt.ApplyPrewitt(mockImage), "Prewitt threw an exception");
        }

        [Test]
        public void TestSobelEdgeDetection_Correctness()
        {
            // Validate specific pixel intensity after applying Sobel operator
            Bitmap output = Sobel.ApplySobel(mockImage);

            // Example: Check the center pixel (1,1)
            Assert.That(output.GetPixel(1, 1).R, Is.GreaterThan(0), "Center pixel intensity is incorrect");
        }

        [Test]
        public void TestPrewittEdgeDetection_Correctness()
        {
            // Validate specific pixel intensity after applying Prewitt operator
            Bitmap output = Prewitt.ApplyPrewitt(mockImage);

            // Example: Check the center pixel (1,1)
            Assert.That(output.GetPixel(1, 1).R, Is.GreaterThan(0), "Center pixel intensity is incorrect");
        }
        
        [TearDown]
        public void TearDown()
        {
            // Dispose of the mock image after tests
            mockImage.Dispose();
        }
    }
}
