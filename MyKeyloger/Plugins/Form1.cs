using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugins
{
    public partial class Form1 : Form, Interface.IMainApp
    {

        Hashtable plugins = new Hashtable();
        public Form1()
        {
            InitializeComponent();

            FindPlugins();

            CreatePluginsMenu();
        }


        public Bitmap Image {
            get { return (Bitmap)pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Блабла", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        void CreatePluginsMenu()
        {
           
            EventHandler handler = new EventHandler(OnPluginClick);

            foreach (string name in plugins.Keys)
            {
               
                menuItemPlugins.DropDownItems.Add(name,null,handler);
              
            }
        }


        private void OnPluginClick(object sender, EventArgs args)
        {
            Interface.IPlugin plugin = (Interface.IPlugin)plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform(this);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Графические файлы (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif;*.png";

            if (dlg.ShowDialog() == DialogResult.OK)
                try
                {
                    Bitmap bmp = new Bitmap(dlg.FileName);
                    pictureBox1.Image = bmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
        }


        void FindPlugins()
        {

            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            //string path = Directory.GetCurrentDirectory();

            string[] files = Directory.GetFiles(path, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("Interface.IPlugin");

                        if (iface != null)
                        {
                            Interface.IPlugin plugin = (Interface.IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }
    }
}
