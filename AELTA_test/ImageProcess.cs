using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Util;

namespace ControlUI
{
    internal class ImageProcess
    {
        private VideoCapture cap = new VideoCapture(0);
        public ImageProcess()
        {
        }
        public void test()
        {
           while(true)
            {
                CvInvoke.Imshow("Test", cap.QueryFrame());
                CvInvoke.WaitKey(1);
            }
        }

    }
}
