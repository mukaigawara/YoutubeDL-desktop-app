using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtube
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public bool searchForm = false;
        private void Form5_Load(object sender, EventArgs e)
        {
            //EMESSAGE();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //search form1
            if (searchForm == false)
            {
                var form1 = new Form1();
                form1.Show(this);

                searchForm = !searchForm;
            }
            else
            {
                MessageBox.Show("Error:　すでにブラウザが起動しています。");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //converter form2
            var form2 = new Form2();
            form2.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //Download form3
            var form3 = new Form3();
            form3.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //エラー情報を表示
            EMESSAGE();
          
        }

        private void EMESSAGE()
        {
              MessageBox.Show(" DOWNCON        version 1.0\n\n"
                  +"現在確認されているエラーはありません。\n\n\n"
                +  "-　改修予定　- \n"
                +"ブラウザのダウンロード・変換機能を非同期処理に変更。\n"
                +"すべての変換機能に、任意で変換元mp4ファイルを消せるように変更。");
        }
    }
}
