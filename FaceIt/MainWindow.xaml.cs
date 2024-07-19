using FaceRecognitionDotNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using App;
using System.Drawing;
using Windows.ApplicationModel.Activation; // 确保已经引入了这个命名空间

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FaceIt
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Pass.Password == "123")
            {

            }
            else
            {
                statusText.Text = string.Empty;
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PR programInstance = new PR();
            programInstance.CV(new string[] { }); // 调用CV方法
            FaceDetector detector = new FaceDetector();
            bool isMatch = detector.DetectFacesFromImageFile("E:/Temp/1721298109971.jpg", "E:/Temp/1721298109971.jpg"); // Replace these paths with your actual image paths.
            if (isMatch == true)
            {
                this.Close();
            }
            Console.WriteLine($"Faces match: {isMatch}");
        }


    }
}


