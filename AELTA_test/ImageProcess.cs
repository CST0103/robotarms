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
using DocumentFormat.OpenXml.Wordprocessing;
using Emgu.CV.CvEnum;
using System.Windows.Forms;
using Intel.RealSense;
using Format = Intel.RealSense.Format;
                                     

namespace ControlUI
{
    
    public class ImageProcess
    {
       
        public ImageProcess()
        {
            var cfg = new Config();

            using (var ctx = new Context())
            {
                var devices = ctx.QueryDevices();
                var dev = devices[0];

                Console.WriteLine("\nUsing device 0, an {0}", dev.Info[CameraInfo.Name]);
                Console.WriteLine("  Serial number: {0}", dev.Info[CameraInfo.SerialNumber]);
                Console.WriteLine("  Firmware version: {0}", dev.Info[CameraInfo.FirmwareVersion]);

                var sensors = dev.QuerySensors();
                var depthSensor = sensors[0];
                var colorSensor = sensors[1];

                var depthProfile = depthSensor.StreamProfiles.Where(p => p.Stream == Intel.RealSense.Stream.Depth).OrderBy(p => p.Framerate).Select(p => p.As<VideoStreamProfile>()).First();

                var colorProfile = colorSensor.StreamProfiles.Where(p => p.Stream == Intel.RealSense.Stream.Color).OrderBy(p => p.Framerate).Select(p => p.As<VideoStreamProfile>()).First();

                //設置相機的寬高,深度,幀數
                cfg.EnableStream(Intel.RealSense.Stream.Depth, 640, 480, Format.Z16, 30);
                cfg.EnableStream(Intel.RealSense.Stream.Color, 640, 480, Format.Bgr8, 30);
            }
            



        }
        public double[] ImageRecognition(int shape)
        {
           

            var cfg = new Config();

            using (var ctx = new Context())
            {
                var devices = ctx.QueryDevices();
                var dev = devices[0];

                Console.WriteLine("\nUsing device 0, an {0}", dev.Info[CameraInfo.Name]);
                Console.WriteLine("  Serial number: {0}", dev.Info[CameraInfo.SerialNumber]);
                Console.WriteLine("  Firmware version: {0}", dev.Info[CameraInfo.FirmwareVersion]);

                var sensors = dev.QuerySensors();
                var depthSensor = sensors[0];
                var colorSensor = sensors[1];

                var depthProfile = depthSensor.StreamProfiles.Where(p => p.Stream == Intel.RealSense.Stream.Depth).OrderBy(p => p.Framerate).Select(p => p.As<VideoStreamProfile>()).First();

                var colorProfile = colorSensor.StreamProfiles.Where(p => p.Stream == Intel.RealSense.Stream.Color).OrderBy(p => p.Framerate).Select(p => p.As<VideoStreamProfile>()).First();

                //設置相機的寬高,深度,幀數
                cfg.EnableStream(Intel.RealSense.Stream.Depth, 640, 480, Format.Z16, 30);
                cfg.EnableStream(Intel.RealSense.Stream.Color, 640, 480, Format.Bgr8, 30);
            }

            // 480,640
            var pipe = new Pipeline();
            //啟動相機並應用配置
            pipe.Start(cfg);
            Colorizer color_map = new Colorizer();
            Image<Bgr, byte> imageDep;
            Image<Bgr, byte> imagecol;
            using (var frames = pipe.WaitForFrames())
            {
                Align align = new Align(Intel.RealSense.Stream.Color).DisposeWith(frames);
                Intel.RealSense.Frame aligned = align.Process(frames).DisposeWith(frames);
                FrameSet alignedframeset = aligned.As<FrameSet>().DisposeWith(frames);

                var colorFrame = alignedframeset.ColorFrame.DisposeWith(alignedframeset);
                var depthFrame = alignedframeset.DepthFrame.DisposeWith(alignedframeset);

                var colorizedDepth = color_map.Process<VideoFrame>(depthFrame).DisposeWith(alignedframeset);

                //獲取幀數並轉為bitmap
                System.Drawing.Bitmap DepthImg = new System.Drawing.Bitmap(colorFrame.Width, colorFrame.Height, colorFrame.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, colorizedDepth.Data);

                System.Drawing.Bitmap ColorImg = new System.Drawing.Bitmap(colorFrame.Width, colorFrame.Height, colorFrame.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, frames.ColorFrame.Data);

                //將圖像顯示至picturebox

                imageDep = DepthImg.ToImage<Bgr, byte>();
                imagecol = ColorImg.ToImage<Bgr, byte>();

                ColorImg.Save("color.jpg");
                DepthImg.Save("depth.jpg");

            }
            int[] CameraSize = new int[] { 640, 480 };
            Point reccenter = new Point(0, 0);
            Point circenter = new Point(0, 0);
            Point squcenter = new Point(0, 0);
            Point screen = new Point(320, 240);
            double Areacir = 0;
            double Arearec = 0;
            double Areasqu = 0;
            //Mat src = new Mat(@"color.jpg", ImreadModes.AnyColor);
            Mat src = imagecol.Mat;

            double[] result = null;

            switch (shape)
            {
                case (int)Eshape.Circle:
                    //////////////////////////////圓形//////////////////////////////////////////////////
                    Mat HSVcircle = new Mat();
                    Mat H2B = new Mat();
                    Mat gray = new Mat();

                    var cirImg = src.ToImage<Bgr, byte>();


                    CvInvoke.CvtColor(cirImg, HSVcircle, ColorConversion.Bgr2Hsv);


                    //Yellow color


                    ScalarArray cirlow = new ScalarArray(new MCvScalar(17, 120, 100));
                    ScalarArray cirhigh = new ScalarArray(new MCvScalar(25, 255, 255));
                    Mat maskcir = new Mat();

                    CvInvoke.InRange(HSVcircle, cirlow, cirhigh, maskcir);




                    Mat Graycir = new Mat();
                    Mat Cannycir = new Mat();
                    Mat Thretholdcir = new Mat();

                    var cirmaskimg = maskcir.ToImage<Bgr, byte>();



                    CvInvoke.CvtColor(cirmaskimg, Graycir, ColorConversion.Bgr2Gray);
                    CvInvoke.Threshold(Graycir, Thretholdcir, 200, 255, ThresholdType.Binary);
                    CvInvoke.Canny(Graycir, Cannycir, 60, 200, 3, false);

                    var Circle = CvInvoke.HoughCircles(Graycir, HoughModes.Gradient, 1, 300, 100, 10, 50, 100);
                    int X_Centercir = 0, Y_Centercir = 0;
                    VectorOfVectorOfPoint contourscir = new VectorOfVectorOfPoint();

                    CvInvoke.FindContours(Graycir, contourscir, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                    //畫圓

                    for (int i = 0; i < Circle.Length; i++)
                    {

                        int x = (int)Circle[i].Center.X;
                        int y = (int)Circle[i].Center.Y;
                        int a = (int)Circle[i].Radius;
                        circenter = new Point(x, y);
                        CvInvoke.Circle(cirmaskimg, circenter, a, new MCvScalar(255, 0, 255), 2, LineType.Filled);
                        CvInvoke.Circle(cirmaskimg, circenter, 2, new MCvScalar(0, 0, 255), 2, LineType.Filled);
                        if (i < Circle.Length)
                        {
                            Rectangle BoundingBox = CvInvoke.BoundingRectangle(contourscir[i]);

                            double dist_Center_Old = Math.Sqrt((Math.Pow(circenter.Y - CameraSize[1] / 2, 2) + Math.Pow(circenter.X - CameraSize[0] / 2, 2)));
                            double dist_Center_New = Math.Sqrt((Math.Pow(Y_Centercir - CameraSize[1] / 2, 2) + Math.Pow(X_Centercir - CameraSize[0] / 2, 2)));

                            if (X_Centercir == 0 && Y_Centercir == 0)
                            {
                                Areacir = CvInvoke.ContourArea(contourscir[i]);
                                X_Centercir = circenter.X;
                                Y_Centercir = circenter.Y;
                                CvInvoke.Rectangle(cirmaskimg, BoundingBox, new MCvScalar(0));
                                CvInvoke.Circle(cirmaskimg, circenter, 2, new MCvScalar(255, 255), 5);
                                CvInvoke.Line(cirmaskimg, circenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);

                            }
                            if (
                                Math.Sqrt((Math.Pow(circenter.Y - CameraSize[1] / 2, 2) + Math.Pow(circenter.X - CameraSize[0] / 2, 2))) <
                                Math.Sqrt((Math.Pow(Y_Centercir - CameraSize[1] / 2, 2) + Math.Pow(X_Centercir - CameraSize[0] / 2, 2))))
                            {
                                CvInvoke.Rectangle(cirmaskimg, BoundingBox, new MCvScalar(0));

                                CvInvoke.Circle(cirmaskimg, circenter, 2, new MCvScalar(255, 255), 5);
                                X_Centercir = circenter.X;
                                Y_Centercir = circenter.Y;

                                Areacir = CvInvoke.ContourArea(contourscir[i]);
                                CvInvoke.Line(cirmaskimg, circenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);

                            }
                            else
                            {
                                circenter.X = X_Centercir;
                                circenter.Y = Y_Centercir;
                                CvInvoke.Line(cirmaskimg, circenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);

                            }
                        }
                    }
                    result = new double[3] { (circenter.Y - CameraSize[1] / 2), (circenter.X - CameraSize[0] / 2), Areacir };
                    CvInvoke.Circle(cirmaskimg, screen, 3, new Bgr(System.Drawing.Color.GreenYellow).MCvScalar, -1);
                    //CvInvoke.Imshow("cir", cirmaskimg);
                    return result;

                case (int)Eshape.Rectangle:
                    ///////////////////////HSV長方形//////////////////////////////////
                    
                    Mat HSVrec = new Mat();

                    var recImg = src.ToImage<Bgr, byte>();

                    CvInvoke.CvtColor(recImg, HSVrec, ColorConversion.Bgr2Hsv);

                    //RED color



                    ScalarArray low = new ScalarArray(new MCvScalar(170, 120, 100));
                    ScalarArray high = new ScalarArray(new MCvScalar(180, 255, 255));
                    ScalarArray low1 = new ScalarArray(new MCvScalar(0, 120, 100));
                    ScalarArray high1 = new ScalarArray(new MCvScalar(10, 255, 255));



                    // 建立兩個遮罩，分別對應兩個紅色區間
                    Mat mask1 = new Mat();
                    Mat mask2 = new Mat();

                    // 進行遮罩操作，將紅色區域設為白色(255)，其他區域設為黑色(0)
                    CvInvoke.InRange(HSVrec, low, high, mask1);
                    CvInvoke.InRange(HSVrec, low1, high1, mask2);

                    // 將兩個遮罩合併
                    Mat maskrec = new Mat();
                    CvInvoke.BitwiseOr(mask1, mask2, maskrec);


                    Mat Grayrec = new Mat();
                    Mat Cannyrec = new Mat();
                    Mat Thretholdrec = new Mat();

                    var recmaskImg = maskrec.ToImage<Bgr, byte>().Erode(1).Dilate(1);

                    CvInvoke.CvtColor(recmaskImg, Grayrec, ColorConversion.Bgr2Gray);
                    CvInvoke.Threshold(Grayrec, Thretholdrec, 200, 255, ThresholdType.Binary);
                    CvInvoke.Canny(Grayrec, Cannyrec, 60, 200, 3, false);
                    using (VectorOfVectorOfPoint contoursrec = new VectorOfVectorOfPoint())
                    {
                        CvInvoke.FindContours(Cannyrec, contoursrec, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                        int X_Centerrec = 0, Y_Centerrec = 0;
                        int countrec = contoursrec.Size;
                        for (int i = 0; i < countrec; i++)
                        {

                            using (VectorOfPoint contourrec = contoursrec[i])
                            {
                                // MinAreaRect 是此版本找尋最小面積矩形的方法。
                                RotatedRect BoundingBox = CvInvoke.MinAreaRect(contourrec);
                                //if (BoundingBox.Size.Width <)
                                CvInvoke.Polylines(recmaskImg, Array.ConvertAll(BoundingBox.GetVertices(), Point.Round), true, new Bgr(System.Drawing.Color.DeepPink).MCvScalar, 3);
                            }
                        }
                        List<Rectangle> rectanglesrec = new List<Rectangle>();
                        for (int i = 0; i < contoursrec.Size; i++)
                        {
                            using (VectorOfPoint contourrec = contoursrec[i])
                            using (VectorOfPoint approxContourrec = new VectorOfPoint())
                            {
                                // 使用多邊形逼近函式檢測矩形輪廓
                                CvInvoke.ApproxPolyDP(contourrec, approxContourrec, CvInvoke.ArcLength(contourrec, true) * 0.05, true);
                                if (CvInvoke.ContourArea(approxContourrec, false) > 1000) // 設定矩形面積閾值
                                {
                                    // 擷取矩形輪廓的外框矩形
                                    Rectangle rect = CvInvoke.BoundingRectangle(approxContourrec);
                                    rectanglesrec.Add(rect);
                                    reccenter = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                                    CvInvoke.Circle(recmaskImg, reccenter, 3, new Bgr(System.Drawing.Color.Red).MCvScalar, -1);
                                    double dist_Center_Old = Math.Sqrt((Math.Pow(reccenter.Y - CameraSize[1] / 2, 2) + Math.Pow(reccenter.X - CameraSize[0] / 2, 2)));
                                    double dist_Center_New = Math.Sqrt((Math.Pow(Y_Centerrec - CameraSize[1] / 2, 2) + Math.Pow(X_Centerrec - CameraSize[0] / 2, 2)));

                                    if (X_Centerrec == 0 && Y_Centerrec == 0)
                                    {
                                        Arearec = CvInvoke.ContourArea(contoursrec[i]);
                                        X_Centerrec = reccenter.X;
                                        Y_Centerrec = reccenter.Y;
                                        CvInvoke.Line(recmaskImg, reccenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);
                                    }
                                    if (
                                        Math.Sqrt((Math.Pow(reccenter.Y - CameraSize[1] / 2, 2) + Math.Pow(reccenter.X - CameraSize[0] / 2, 2))) <
                                        Math.Sqrt((Math.Pow(Y_Centerrec - CameraSize[1] / 2, 2) + Math.Pow(X_Centerrec - CameraSize[0] / 2, 2))))
                                    {
                                        RotatedRect BoundingBox = CvInvoke.MinAreaRect(contourrec);
                                        CvInvoke.Polylines(recmaskImg, Array.ConvertAll(BoundingBox.GetVertices(), Point.Round), true, new Bgr(System.Drawing.Color.DeepPink).MCvScalar, 3);

                                        CvInvoke.Circle(recmaskImg, reccenter, 3, new Bgr(System.Drawing.Color.Red).MCvScalar, -1);
                                        X_Centerrec = reccenter.X;
                                        Y_Centerrec = reccenter.Y;

                                        Arearec = CvInvoke.ContourArea(contoursrec[i]);
                                        CvInvoke.Line(recmaskImg, reccenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);

                                    }
                                    else
                                    {
                                        reccenter.X = X_Centerrec;
                                        reccenter.Y = Y_Centerrec;
                                        CvInvoke.Line(recmaskImg, reccenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);

                                    }
                                }
                            }

                        }
                    }
                    result = new double[3] { (reccenter.Y - CameraSize[1] / 2), (reccenter.X - CameraSize[0] / 2), Arearec };
                    CvInvoke.Circle(recmaskImg, screen, 3, new Bgr(System.Drawing.Color.GreenYellow).MCvScalar, -1);
                    //CvInvoke.Imshow("rec", recmaskImg);
                    return result;

                case (int)Eshape.Square:
                    //////////////////////////正方形///////////////////////////

                    Mat HSVsqu = new Mat();

                    var squImg = src.ToImage<Bgr, byte>();

                    CvInvoke.CvtColor(squImg, HSVsqu, ColorConversion.Bgr2Hsv);

                    //RED color



                    ScalarArray lows = new ScalarArray(new MCvScalar(170, 120, 100));
                    ScalarArray highs = new ScalarArray(new MCvScalar(180, 255, 255));
                    ScalarArray low1s = new ScalarArray(new MCvScalar(0, 120, 100));
                    ScalarArray high1s = new ScalarArray(new MCvScalar(10, 255, 255));



                    // 建立兩個遮罩，分別對應兩個紅色區間
                    Mat mask1s = new Mat();
                    Mat mask2s = new Mat();

                    // 進行遮罩操作，將紅色區域設為白色(255)，其他區域設為黑色(0)
                    CvInvoke.InRange(HSVsqu, lows, highs, mask1s);
                    CvInvoke.InRange(HSVsqu, low1s, high1s, mask2s);

                    // 將兩個遮罩合併
                    Mat masksqu = new Mat();
                    CvInvoke.BitwiseOr(mask1s, mask2s, masksqu);


                    Mat Graysqu = new Mat();
                    Mat Cannysqu = new Mat();
                    Mat Thretholdsqu = new Mat();

                    var squmaskImg = masksqu.ToImage<Bgr, byte>().Erode(1).Dilate(1);

                    CvInvoke.CvtColor(squmaskImg, Graysqu, ColorConversion.Bgr2Gray);
                    CvInvoke.Threshold(Graysqu, Thretholdsqu, 200, 255, ThresholdType.Binary);
                    CvInvoke.Canny(Graysqu, Cannysqu, 60, 200, 3, false);
                    using (VectorOfVectorOfPoint contourssqu = new VectorOfVectorOfPoint())
                    {
                        CvInvoke.FindContours(Cannysqu, contourssqu, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                        int X_Centersqu = 0, Y_Centersqu = 0;
                        int countsqu = contourssqu.Size;
                        for (int i = 0; i < countsqu; i++)
                        {

                            using (VectorOfPoint contoursqu = contourssqu[i])
                            {
                                // MinAreaRect 是此版本找尋最小面積矩形的方法。
                                RotatedRect BoundingBox = CvInvoke.MinAreaRect(contoursqu);
                                //if (BoundingBox.Size.Width <)
                                CvInvoke.Polylines(squmaskImg, Array.ConvertAll(BoundingBox.GetVertices(), Point.Round), true, new Bgr(System.Drawing.Color.DeepPink).MCvScalar, 3);
                            }
                        }
                        List<Rectangle> rectanglessqu = new List<Rectangle>();
                        for (int i = 0; i < contourssqu.Size; i++)
                        {
                            using (VectorOfPoint contoursqu = contourssqu[i])
                            using (VectorOfPoint approxContoursqu = new VectorOfPoint())
                            {
                                // 使用多邊形逼近函式檢測矩形輪廓
                                CvInvoke.ApproxPolyDP(contoursqu, approxContoursqu, CvInvoke.ArcLength(contoursqu, true) * 0.05, true);
                                if (CvInvoke.ContourArea(approxContoursqu, false) > 1000) // 設定矩形面積閾值
                                {
                                    // 擷取矩形輪廓的外框矩形
                                    Rectangle rectsqu = CvInvoke.BoundingRectangle(approxContoursqu);
                                    rectanglessqu.Add(rectsqu);
                                    squcenter = new Point(rectsqu.X + rectsqu.Width / 2, rectsqu.Y + rectsqu.Height / 2);
                                    CvInvoke.Circle(squmaskImg, squcenter, 3, new Bgr(System.Drawing.Color.Red).MCvScalar, -1);
                                    double dist_Center_Old = Math.Sqrt((Math.Pow(squcenter.Y - CameraSize[1] / 2, 2) + Math.Pow(squcenter.X - CameraSize[0] / 2, 2)));
                                    double dist_Center_New = Math.Sqrt((Math.Pow(Y_Centersqu - CameraSize[1] / 2, 2) + Math.Pow(X_Centersqu - CameraSize[0] / 2, 2)));

                                    if (X_Centersqu == 0 && Y_Centersqu == 0)
                                    {
                                        Areasqu = CvInvoke.ContourArea(contourssqu[i]);
                                        X_Centersqu = squcenter.X;
                                        Y_Centersqu = squcenter.Y;
                                        CvInvoke.Line(squmaskImg, squcenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);
                                    }
                                    if (
                                        Math.Sqrt((Math.Pow(squcenter.Y - CameraSize[1] / 2, 2) + Math.Pow(squcenter.X - CameraSize[0] / 2, 2))) <
                                        Math.Sqrt((Math.Pow(Y_Centersqu - CameraSize[1] / 2, 2) + Math.Pow(X_Centersqu - CameraSize[0] / 2, 2))))
                                    {
                                        RotatedRect BoundingBox = CvInvoke.MinAreaRect(contoursqu);
                                        CvInvoke.Polylines(squmaskImg, Array.ConvertAll(BoundingBox.GetVertices(), Point.Round), true, new Bgr(System.Drawing.Color.DeepPink).MCvScalar, 3);

                                        CvInvoke.Circle(squmaskImg, squcenter, 3, new Bgr(System.Drawing.Color.Red).MCvScalar, -1);
                                        X_Centersqu = squcenter.X;
                                        Y_Centersqu = squcenter.Y;

                                        Areasqu = CvInvoke.ContourArea(contourssqu[i]);
                                        CvInvoke.Line(squmaskImg, squcenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);

                                    }
                                    else
                                    {
                                        squcenter.X = X_Centersqu;
                                        squcenter.Y = Y_Centersqu;
                                        CvInvoke.Line(squmaskImg, squcenter, new Point(CameraSize[0] / 2, CameraSize[1] / 2), new MCvScalar(0, 0, 255), 2);

                                    }
                                }
                            }

                        }
                    }

                    result = new double[3] { (squcenter.Y - CameraSize[1] / 2), (squcenter.X - CameraSize[0] / 2), Areasqu };
                    CvInvoke.Circle(squmaskImg, screen, 3, new Bgr(System.Drawing.Color.GreenYellow).MCvScalar, -1);
                    //CvInvoke.Imshow("squ", squmaskImg);

                    return result;

                    

            }


            CvInvoke.Imshow("src", src);

            return result;  
            


        }

        
    }
}
