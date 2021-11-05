using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;
using youtube.Properties;


namespace youtube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string url = "";
        string homeUrl = "https://www.youtube.com";
        private void button4_Click(object sender, EventArgs e)
        {
            //キャッシュを無効で再読み込み、リロード、リフレッシュ
            cefBrowser.Reload();
        }

        ChromiumWebBrowser cefBrowser;
        public void Form1_Load(object sender, EventArgs e)
        {
            //CefSettings settings = new CefSettings();
            //Cef.Initialize(settings);

            cefBrowser = new ChromiumWebBrowser(/*"chrome://version"*/"https://www.youtube.com/");

            //コントロールを追加する
            splitContainer1.Panel2.Controls.Add(cefBrowser);
            cefBrowser.Dock = DockStyle.Fill;

            //event
            this.cefBrowser.AddressChanged += cefBrowser_AddressChanged;
        }

        private void cefBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            url = e.Address;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(this.EnterUrl));
            }
            else
            {
                this.textBox1.Text = url;
            }
        }
        private void EnterUrl()
        {
            this.textBox1.Text = url;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cefBrowser.Load(homeUrl);
        }

        private void 変換ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //GO BACK
            cefBrowser.Back();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.Owner as Form5).searchForm = false;

            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //go forward
            cefBrowser.Forward();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            cefBrowser.Load("https://www.google.com/search?q="+textBox1.Text);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            //初期化
            progressBar1.Value = 0;
            label1.Text = "ダウンロード中";
            //visible,enableの設定
            splitContainer1.Panel2.Enabled = false;
            label1.Visible = true;
            progressBar1.Visible = true;
            button6.Enabled = false;

            //ダウンロード,video に格納
            progressBar1.Minimum = 0 ;
            progressBar1.Maximum = 5 ;
            try
            {
                
                ProgressBar_Plus();

                 url = textBox1.Text;

                if (string.IsNullOrEmpty(url))
                {
                    MessageBox.Show("URLを入力してください。");
                    return;
                }
                WebRequest.DefaultWebProxy = null;
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(url);

                ProgressBar_Plus();

                var sfd = new SaveFileDialog();

                sfd.FileName = video.FullName;

                //sfdで入力されたタイトルを格納しておく
                var videoName = sfd.FileName;

                sfd.Filter = "mp4ファイル(*.mp4)|*.mp4";


                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(sfd.FileName, video.GetBytes());
                    //MessageBox.Show("ダウンロードが成功しました。");

                    ProgressBar_Plus();

                    label1.Text = "変換中";

                    var inputFile = new MediaFile { Filename = @sfd.FileName };
                    var outputFile = new MediaFile { Filename = @sfd.FileName+".mp3" };

                    ProgressBar_Plus();

               

                    using (var engine = new Engine())
                    {
                        engine.Convert(inputFile, outputFile);


                        ProgressBar_Plus();

                        //MessageBox.Show(inputFile.Filename + "\nを\n" + outputFile.Filename
                        //    + "\nに変換しました");
                        label1.Text = "変換完了";
                    }

                    try
                    {
                        //ｍｐ４のファイルを削除
                        FileInfo file = new FileInfo(@inputFile.Filename);
                        file.Delete();

                        //File.Move(file.FullName,videoName+"mp3");
                    }
                    catch { label1.Text = "error"; }
                }

            }
            catch
            {
                button6.Enabled = true;
                label1.Visible = false;
                progressBar1.Visible = false;
                MessageBox.Show("エラーが発生しました。\nURLなどを確認してください。");
            }

            //変換
            /*string path = "";
            string saveName = "";

            var inputFile = new MediaFile { Filename = @path };
            var outputFile = new MediaFile { Filename = path };*/

            //非同期 遅延
            await Task.Delay(600);
            label1.Text = "正常に終了しました";
            await Task.Delay(6000);



            //visible,enableの設定
            button6.Enabled = true;
            label1.Visible = false;
            progressBar1.Visible = false;
            splitContainer1.Panel2.Enabled = true;

        }

        private void ProgressBar_Plus()
        {
            progressBar1.Value += 1;
        }
    }
}
