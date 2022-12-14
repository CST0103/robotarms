using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;

namespace ControlUI
{
    public class ImageProcess
    {
        private VideoCapture Arm_cap;
        public ImageProcess()
        {

            Environment.SetEnvironmentVariable("OPENCV_VIDEOIO_MSMF_ENABLE_HW_TRANSFORMS", "0");
            Arm_cap = new VideoCapture(0);
            Arm_cap.Set(Emgu.CV.CvEnum.CapProp.Autofocus, 0);
            Arm_cap.Set(Emgu.CV.CvEnum.CapProp.Focus, 40);
            Arm_cap.Set(Emgu.CV.CvEnum.CapProp.AutoExposure, 0);
            Arm_cap.Set(Emgu.CV.CvEnum.CapProp.Exposure, -5);
            //Arm_cap.Set(Emgu.CV.CvEnum.CapProp.FrameWidth, 1080);
            //Arm_cap.Set(Emgu.CV.CvEnum.CapProp.FrameWidth, 720);
            Arm_cap.FlipVertical = true;

        }
        public double[] ImageRecognition()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int X_Center = 0, Y_Center = 0;
            // 480,640
            Image<Bgr, byte> img = Arm_cap.QueryFrame().ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Vertical);

            img = img.SmoothGaussian(1);
            //img = img.SmoothBilateral(8, 8, 8);

            int[] CameraSize = new int[] { Arm_cap.Width, Arm_cap.Height };
            Image<Hsv, byte> img_hsv = new Image<Hsv, byte>(img.Width, img.Height);
            CvInvoke.CvtColor(img, img_hsv, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);


            Image<Gray, byte>[] channels = img_hsv.Split();
            Image<Gray, byte> threshold = img_hsv.InRange(new Hsv(0, 0, 0), new Hsv(180, 255, 46));
            
            Point Center = new Point(0, 0);

            threshold = threshold.Erode(3);
            threshold = threshold.Dilate(3);
           
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

            CvInvoke.FindContours(threshold, contours, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            double Area = 0;
            for (int i = 0; i < contours.Size; i++)
            {
                if (CvInvoke.ContourArea(contours[i]) > 1000)
                {
                    Rectangle BoundingBox = CvInvoke.BoundingRectangle(contours[i]);
                    Center = new Point(BoundingBox.X + BoundingBox.Width / 2, BoundingBox.Y + BoundingBox.Height / 2);
                    double dist_Center_Old = Math.Sqrt((Math.Pow(Center.Y - CameraSize[1] / 2, 2) + Math.Pow(Center.X - CameraSize[0] / 2, 2)));
                    double dist_Center_New = Math.Sqrt((Math.Pow(Y_Center - CameraSize[1] / 2, 2) + Math.Pow(X_Center - CameraSize[0] / 2, 2)));

                    if (X_Center == 0 && Y_Center == 0)
                    {
                        Area = CvInvoke.ContourArea(contours[i]);
                        X_Center = Center.X;
                        Y_Center = Center.Y;
                        CvInvoke.Rectangle(img, BoundingBox, new MCvScalar(0));

                        CvInvoke.Circle(img, Center, 2, new MCvScalar(255, 255), 5);
                    }
                    if (
                        Math.Sqrt((Math.Pow(Center.Y - CameraSize[1] / 2, 2) + Math.Pow(Center.X - CameraSize[0] / 2, 2))) <
                        Math.Sqrt((Math.Pow(Y_Center - CameraSize[1] / 2, 2) + Math.Pow(X_Center - CameraSize[0] / 2, 2))))
                    {
                        CvInvoke.Rectangle(img, BoundingBox, new MCvScalar(0));

                        CvInvoke.Circle(img, Center, 2, new MCvScalar(255, 255), 5);
                        X_Center = Center.X;
                        Y_Center = Center.Y;

                        Area = CvInvoke.ContourArea(contours[i]);
                    }
                    else
                    {
                        Center.X = X_Center;
                        Center.Y = Y_Center;
                    }
                }
            }
            CvInvoke.Line(img, Center, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);
            CvInvoke.PutText(img, X_Center.ToString() + " " + Y_Center.ToString(), new Point(50, 50), Emgu.CV.CvEnum.FontFace.HersheyScriptSimplex, 2, new MCvScalar(0, 0, 0));

            CvInvoke.Imshow("ori", img);
            CvInvoke.Imshow("Test", threshold);
            CvInvoke.WaitKey(1);
            stopwatch.Stop();
            double time = stopwatch.ElapsedMilliseconds;
            double[] vs = new double[4] { (Center.Y - CameraSize[1] / 2), (Center.X - CameraSize[0] / 2), Area, time};
            return vs;

        }
    }
}
