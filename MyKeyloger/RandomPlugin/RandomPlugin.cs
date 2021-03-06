using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPlugin
{
    public class RandomTransform : Interface.IPlugin
    {
        public string Name {
            get { return "Случайная трансформация"; }
        }

        public string Version {
            get { return "1.0"; }
        }

        public string Author {
            get { return "Proger"; }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;

            Random rand = new Random(DateTime.Now.Millisecond);
            int pixels = (int)(0.1 / bitmap.Width / bitmap.Height);

            for (int i = 0; i < pixels; ++i)
                bitmap.SetPixel(rand.Next(bitmap.Width -1), rand.Next(bitmap.Height),
                    Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));

            app.Image = bitmap;
        }
    }
}
