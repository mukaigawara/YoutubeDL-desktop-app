using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using youtube.Properties;

namespace youtube
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string path = "";

        public void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "ファイルを開く";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "mp4ファイル(*.mp4)|*.mp4";


            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = ofd.FileName;
            }
            label5.Text = path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string savefileName = "";
            savefileName = textBox1.Text;

            if (textBox1.Text != "")
            {
                if (path != "")
                {
                    var inputFile = new MediaFile { Filename = @path };

                    
                    var outputFile = new MediaFile { Filename = @"C:\Users\user\Desktop\" + savefileName + ".mp3" };



                    using (var engine = new Engine())
                    {
                        engine.Convert(inputFile, outputFile);

                        MessageBox.Show(inputFile.Filename + "\n" + " を " + outputFile.Filename
                            + "\n" + "に変換しました");
                    }
                }
                else
                {
                    MessageBox.Show("変換したいMP4ファイルを開いてください");
                }
            }
            else
            {
                MessageBox.Show("保存する名前を入力してください");
            }

        }
    }
}
