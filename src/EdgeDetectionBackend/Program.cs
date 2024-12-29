using System;
using System.Drawing;
using System.IO;
using EdgeDetectionBackend.EdgeDetection;

namespace EdgeDetectionBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Edge Detection Backend");
            Console.WriteLine("-----------------------------------");

            try
            {
                // Get user input for image paths and operator selection
                Console.Write("Enter the path of the input image: ");
                string inputImagePath = Console.ReadLine();

                // Check if the input image exists
                if (!File.Exists(inputImagePath))
                {
                    Console.WriteLine("Error: The specified input image file does not exist. Please check the path.");
                    return;
                }

                Console.Write("Enter the path to save the output image (including file name): ");
                string outputImagePath = Console.ReadLine();

                // Check if the output directory exists
                string outputDirectory = Path.GetDirectoryName(outputImagePath);
                if (!Directory.Exists(outputDirectory))
                {
                    Console.WriteLine("Error: The specified output directory does not exist. Please check the path.");
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
                Bitmap inputImage;
                try
                {
                    inputImage = new Bitmap(inputImagePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Unable to load the input image. Details: {ex.Message}");
                    return;
                }

                // Perform edge detection based on user choice
                Bitmap outputImage;
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Applying Sobel operator...");
                            outputImage = Sobel.ApplySobel(inputImage);
                            break;
                        case 2:
                            Console.WriteLine("Applying Prewitt operator...");
                            outputImage = Prewitt.ApplyPrewitt(inputImage);
                            break;
                        default:
                            Console.WriteLine("Error: Invalid operator choice.");
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Failed to apply the edge detection algorithm. Details: {ex.Message}");
                    return;
                }

                // Save the output image
                try
                {
                    outputImage.Save(outputImagePath);
                    Console.WriteLine($"Output image saved to: {outputImagePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Unable to save the output image. Details: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("Process complete. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
