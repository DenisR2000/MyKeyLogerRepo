using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPlagin
{
    public class ColorTransform : Interface.IPlugin
    {
        public string Name
        {
            get { return "Черно белый"; }
        }
        public string Author
        {
            get { return "Proger"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            
            for (int j = 0; j < bitmap.Height; j++)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    UInt32 pixel = (UInt32)(bitmap.GetPixel(i, j).ToArgb());
                    // получаем компоненты цветов пикселя
                    float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                    float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый 
                    float B = (float)(pixel & 0x000000FF); // синий
                                                           // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                    int rgb = Convert.ToInt32((R + G + B) / 3.0f);
                    // собираем новый пиксель по частям (по каналам)
                    bitmap.SetPixel(i, j, Color.FromArgb(rgb, rgb, rgb));
                }
            }

            app.Image = bitmap;
        }
    }
}
