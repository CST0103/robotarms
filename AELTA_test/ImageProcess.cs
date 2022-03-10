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

namespace ControlUI
{
    public class ImageProcess
    {
        private VideoCapture cap;
        public ImageProcess()
        {

            Environment.SetEnvironmentVariable("OPENCV_VIDEOIO_MSMF_ENABLE_HW_TRANSFORMS", "0");
            cap = new VideoCapture();
            cap.Set(Emgu.CV.CvEnum.CapProp.Autofocus, 0);
            cap.Set(Emgu.CV.CvEnum.CapProp.Focus, 40);
            cap.FlipVertical = true;
        }
        public double[] ImageRecognition()
        {
            while (true)
            {
                // 480,640
                Image<Bgr, byte> img = cap.QueryFrame().ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Vertical);
                img = img.SmoothGaussian(3);
                int[] CameraSize = new int[] { cap.Width, cap.Height };
                Image<Hsv, byte> img_hsv = new Image<Hsv, byte>(img.Width, img.Height);
                CvInvoke.CvtColor(img, img_hsv, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);

                Image<Gray, byte>[] channels = img_hsv.Split();
                Image<Gray, byte> threshold = img_hsv.InRange(new Hsv(0, 0, 0), new Hsv(360, 255, 50));
                Point Center = new Point(0, 0);
                threshold = threshold.Erode(5);
                threshold = threshold.Dilate(5);
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(threshold, contours, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                for (int i = 0; i < contours.Size; i++)
                {
                    if (CvInvoke.ContourArea(contours[i]) > 5000)
                    {
                        Rectangle BoundingBox = CvInvoke.BoundingRectangle(contours[i]);
                        CvInvoke.Rectangle(img, BoundingBox, new MCvScalar(0));
                        Center = new Point(BoundingBox.X + BoundingBox.Width / 2, BoundingBox.Y + BoundingBox.Height / 2);

//                        CvInvoke.Circle(img, new Point(320, 240), 2, new MCvScalar(255), 5);
                        CvInvoke.Circle(img, Center, 2, new MCvScalar(255, 255), 5);
                        CvInvoke.PutText(img, Center.ToString(), new Point(50, 50), Emgu.CV.CvEnum.FontFace.HersheyScriptSimplex, 2, new MCvScalar(0, 0, 0));
                    }
                }
                CvInvoke.Imshow("ori", img);
                CvInvoke.Imshow("Test", threshold);
                CvInvoke.WaitKey(1);
                double[] vs = new double[2] { (Center.Y - CameraSize[1] / 2) / 6, (Center.X - CameraSize[0] / 2) / 4.5 };
                //return vs;
            }

        }

    }
}
