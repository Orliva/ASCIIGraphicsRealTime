using System;
using Emgu.CV;
using Emgu.CV.Structure;
using ASCIIGraphics;

namespace ASCIIGraphicsRealTime
{
    class Program
    {
        private static VideoCapture capture = new VideoCapture(0);
        private static Image<Bgr, byte> im;

        [STAThread]
        static void Main(string[] args)
        {
            capture = new VideoCapture(); //Получаем доступ к камере по умолчанию
            capture.Grab();

            im = capture.QueryFrame().ToImage<Bgr, byte>();
            capture.ImageGrabbed += Capture_ImageGrabbed; //Подписываемся на событие
            capture.Start();

            Console.Read();
        }

        private static void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            capture.Retrieve(im);

            DrawPictureInConsole.Draw(im.ToBitmap());
            Console.SetCursorPosition(0, 0);
        }
    }
}
