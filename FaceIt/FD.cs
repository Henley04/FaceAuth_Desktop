using Emgu.CV.Structure;
using Emgu.CV;
using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App;

namespace App
{
    public class FD
    {

    
        public void CV(string[] args)
        {
            VideoCapture capture = new VideoCapture();

            try
            {
                // 注意：QueryFrame()可能返回null，特别是如果摄像头没有准备好  
                Image<Bgr, byte> image = capture.QueryFrame().ToImage<Bgr, byte>();
                if (image != null)
                {
                    string imagePath = @"E:/Temp/OH.jpg";
                    image.Save(imagePath);
                    Console.WriteLine($"Image saved to {imagePath}");
                }
                else
                {
                    Console.WriteLine("Failed to capture an image from the camera.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                capture?.Dispose();
            }

            Console.ReadKey();
        }
    }
}
public class FaceFinder
    {
        private FaceRecognition FaceRecognition;
        private Model ModelType;
        
        public FaceFinder()
        {
            var modelsPath = Path.Combine(Directory.GetCurrentDirectory(), "models");
            FaceRecognition = FaceRecognition.Create(modelsPath);

            if (Enum.TryParse("hog", true, out Model model))
            {
                ModelType = model;
            }
            else
            {
                ModelType = Model.Cnn;
            }

            Console.WriteLine($"使用模型: {ModelType}");
        }
    }

    public class FaceDetector
    {
        public bool DetectFacesFromImageFile(string imageFilePath, string targeti, Mode mode = Mode.Rgb)
        {
            var modelParameter = new ModelParameter
            {
                PosePredictor68FaceLandmarksModel = File.ReadAllBytes("E://FD//models//shape_predictor_68_face_landmarks.dat"),
                PosePredictor5FaceLandmarksModel = File.ReadAllBytes("E://FD//models//shape_predictor_5_face_landmarks.dat"),
                FaceRecognitionModel = File.ReadAllBytes("E://FD//models//dlib_face_recognition_resnet_model_v1.dat"),
                CnnFaceDetectorModel = File.ReadAllBytes("E://FD//models//mmod_human_face_detector.dat")
            };

            using (FaceRecognition fr = FaceRecognition.Create(modelParameter))
            {
                using (Image imageA = FaceRecognition.LoadImageFile(imageFilePath))
                using (Image imageB = FaceRecognition.LoadImageFile(targeti))
                {
                    IEnumerable<Location> locationsA = fr.FaceLocations(imageA);
                    IEnumerable<Location> locationsB = fr.FaceLocations(imageB);

                    // Check if any faces were detected.
                    if (!locationsA.Any() || !locationsB.Any())
                    {
                        Console.WriteLine("No faces detected in one or both images.");
                        return false; // Return false if no faces are detected.
                    }

                    IEnumerable<FaceEncoding> encodingA = fr.FaceEncodings(imageA, locationsA);
                    IEnumerable<FaceEncoding> encodingB = fr.FaceEncodings(imageB, locationsB);
                    const double tolerance = 0.6d;
                    bool match = FaceRecognition.CompareFace(encodingA.First(), encodingB.First(), tolerance);

                    return match; // Return the match result.
                }
            }
        }
    }
