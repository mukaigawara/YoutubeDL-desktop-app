using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace youtube
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string url = "";
        private void button1_Click(object sender, EventArgs e)
        {
            //現在のリンクにある動画をダウンロード
            //ダウンロードできるURLかどうか、判別する処理を追加
            try
            {
                url = textBox1.Text;
                
                if (String.IsNullOrEmpty(url))
                {
                    MessageBox.Show("urlを入力してください");
                    return;
                }
                WebRequest.DefaultWebProxy = null;
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(url);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = video.FullName;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(sfd.FileName, video.GetBytes());
                    MessageBox.Show("ダウンロードが成功しました。");
                }
            }
            catch
            {
                MessageBox.Show("正しいURLを入力してください。");
                textBox1.Text = "";
            }

        }
    }
}
