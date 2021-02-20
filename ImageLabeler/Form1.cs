using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageLabeler
{
    public partial class Form1 : Form
    {
        List<string> ImagePaths;
        List<Attribute> AttributeList;
        int index = 0;
        string targetDirectory = "./";
        public Form1()
        {
            InitializeComponent();

            
            System.IO.FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = targetDirectory,
                Filter = "*.jpg | .bmp"
            };
            // Add event handlers for all events you want to handle
            watcher.Created += new FileSystemEventHandler(OnChanged);
            // Activate the watcher
            watcher.EnableRaisingEvents = true;

            ImagePaths = new List<string>();
            
            string[] fileEntries = System.IO.Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                string filenameExt = System.IO.Path.GetExtension(fileName);
                if (filenameExt.Equals(".jpg") ||
                    filenameExt.Equals(".jpeg") ||
                    filenameExt.Equals(".bmp") ||
                    filenameExt.Equals(".png")
                    )
                {
                    ImagePaths.Add(fileName);
                }
            }
            MainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MainPictureBox.Image = Image.FromFile(ImagePaths[0]);

            AttributeList = new List<Attribute>();
            
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            //MessageBox.Show($"File: {e.FullPath} {e.ChangeType}");
            ImagePaths.Clear();
            string[] fileEntries = System.IO.Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                string filenameExt = System.IO.Path.GetExtension(fileName);
                if (filenameExt.Equals(".jpg") ||
                    filenameExt.Equals(".jpeg") ||
                    filenameExt.Equals(".bmp") ||
                    filenameExt.Equals(".png")
                    )
                {
                    ImagePaths.Add(fileName);
                }
            }
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            index--;
            if (index <= 0)
            {
                index = 0;
            }
            MainPictureBox.Image = Image.FromFile(ImagePaths[index]);
            GC.Collect();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NextAction();
        }

        private void NextAction()
        {
            index++;
            if (index >= ImagePaths.Count)
            {
                index = ImagePaths.Count - 1;
            }
            MainPictureBox.Image = Image.FromFile(ImagePaths[index]);
            GC.Collect();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Attribute attribute = new Attribute();
            
            if (radioButton1.Checked)
            {
                attribute.attributeEnum = Attribute.AttributeEnum.Mountain;
            }
            if (radioButton2.Checked)
            {
                attribute.attributeEnum = Attribute.AttributeEnum.Cat;
            }
            MessageBox.Show(attribute.ToString());
            AttributeList.Add(attribute);
            AttributeListToTxt();
            NextAction();
        }

        private void AttributeListToTxt()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.AttributeList.Count; i++)
            {
                sb.Append(this.ImagePaths[i] + "\t" + this.AttributeList[i].ToString() + Environment.NewLine);
            }
            File.WriteAllText("LabelResult.txt", sb.ToString());
        }
    }
}
