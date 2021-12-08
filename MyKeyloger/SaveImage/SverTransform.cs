using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SaveImage
{
    public class SverTransform : Interface.IPlugin
    {
        public string Name
        {
            get { return "Сохранить"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public string Author
        {
            get { return "Proger"; }
        }
        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                try
                {
                    dlg.Title = "Save as...";
                    dlg.OverwritePrompt = true;//отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            bitmap.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
