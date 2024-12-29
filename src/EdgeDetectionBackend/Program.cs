using System;
using System.Drawing;
using System.Windows.Forms;

namespace EdgeDetectionBackend
{
    class Program
    {
        [STAThread] // MUST add
        static void Main(string[] args)
        {
            Console.WriteLine("Edge Detection Backend with Interactive File Selection");
            Console.WriteLine("-----------------------------------");

            try
            {
                string inputImagePath = SelectInputFile();
                if (string.IsNullOrEmpty(inputImagePath))
                {
                    Console.WriteLine("No input file selected. Exiting...");
                    return;
                }

                string outputImagePath = SelectSaveFile();
                if (string.IsNullOrEmpty(outputImagePath))
                {
                    Console.WriteLine("No output file path selected. Exiting...");
                    return;
                }

                Console.WriteLine("Select Edge Detection Operator:");
                Console.WriteLine("1. Sobel");
                Console.WriteLine("2. Prewitt");
                Console.Write("Enter your choice (1 or 2): ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("Error: Invalid choice. Please select 1 or 2.");
                    return;
                }

                // Load input image
                Bitmap inputImage = new Bitmap(inputImagePath);

                // Choose operator and apply edge detection
                Bitmap outputImage;
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Applying Sobel operator...");
                        outputImage = EdgeDetection.Sobel.ApplySobel(inputImage);
                        break;
                    case 2:
                        Console.WriteLine("Applying Prewitt operator...");
                        outputImage = EdgeDetection.Prewitt.ApplyPrewitt(inputImage);
                        break;
                    default:
                        Console.WriteLine("Error: Invalid operator choice.");
                        return;
                }

                // Save output image
                outputImage.Save(outputImagePath);
                Console.WriteLine($"Output image saved to: {outputImagePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("Process complete. Press any key to exit...");
            Console.ReadKey();
        }

        /// Open a window to select the input image
        private static string SelectInputFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Input Image";
                openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }

        /// Open a window to select the save location

        private static string SelectSaveFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Select Save Location";
                saveFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
            }
            return null;
        }
    }
}
