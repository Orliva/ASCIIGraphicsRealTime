using System;
using System.Linq;
using System.Drawing;
using System.IO;

namespace ASCIIGraphics
{
    public class DrawPictureInConsole
    {
        //Коэф-ты для корретного отображения картинки
        private const double WIDTH_OFFSET = 2;
        private const int MAX_WIDTH = 300;

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > maxHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)maxHeight));
            return bitmap;
        }

        private static Bitmap GetBitmap(Image image)
        {
            Bitmap bm = new Bitmap(image);
            bm = ResizeBitmap(bm); //Получаем новый Bitmap измененного размера (с учетом коэф-ов)
            bm.ToGray(); //Красим пиксели Bitmap'a в градиент серого

            return bm;
        }

        /// <summary>
        /// При некорректном отображении картинки, изменить настройки консоли
        /// Размер шрифта, тип шрифта, размер консоли
        /// </summary>
        /// <param name="image"></param>
        static public void Draw(Image image)
        {
            Bitmap bm = GetBitmap(image);

            var converter = new BitmapToAsciiConverter(bm);
            var rows = converter.Convert(); //Получаем массив символов ASCII для вывода в консоль
            foreach (var row in rows)
                Console.WriteLine(row);
        }

        static public void DrawInFile(Image image, string destFileName)
        {
            Bitmap bm = GetBitmap(image);

            var converter = new BitmapToAsciiConverter(bm);
            var rowsNegative = converter.ConvertNegative(); //Получаем картинку в "негативе" для вывода на белом (дефолтном) цвете .txt файла
            File.WriteAllLines(destFileName, rowsNegative.Select(r => new string(r)));
        }
    }
}
