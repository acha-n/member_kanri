using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace member_kanri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        //追加、更新
        private void addition_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //リストボックスの中の行の集まりの中の［i］行目(1人分) 
                //listView1.Items[i];をhogeという変数に代入してる。ifで省略して書ける
                string listId = listView1.Items[i].Text;

                //idboxにはいってる値と同じ値をもつ行があるとき（リストボックスの中のiの情報のなかの[0]）
                if (id_box.Text == listId)
                {
                    //メッセージボックスを出す
                    //yesを押したとき（テキストボックスに入ってるテキストをリストボックス内の同じIDに上書き）
                    if (MessageBox.Show("上書きしますか", "確認",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ListViewItem target = listView1.Items[i];
                        target.SubItems[1].Text = name_box.Text;
                        target.SubItems[2].Text = age_box.Text;
                        target.SubItems[3].Text = sex_box.Text;
                        target.SubItems[4].Text = affiliation_box.Text;
                        target.SubItems[5].Text = comment_box.Text;
                     }
                    return; 
                }
            }
            //↑じゃない場合、テキストボックスの中身を空白いれてリストボックスに追加
           // listView1.Items.Add(id_box.Text).SubItems.Add(name_box.Text);
            ListViewItem lvi = listView1.Items.Add(id_box.Text);
            lvi.SubItems.Add(name_box.Text);
            lvi.SubItems.Add(age_box.Text);
            lvi.SubItems.Add(sex_box.Text);
            lvi.SubItems.Add(affiliation_box.Text);
            lvi.SubItems.Add(comment_box.Text);
        }
        //削除
        private void delete_button_Click(object sender, EventArgs e)
        {
            //もし選択されたら、選択されたものを消す
            if (listView1.SelectedItems.Count >0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);

            }
            //選択されなかったらメッセージボックスを出す
            else
            {
                MessageBox.Show("選択してください",
                 "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        //とじるぼたん
        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //りすとびゅー
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //選択したらテキストボックスに表示↓
            ListView listView = (ListView)sender;
            //listview.textをtextという変数にした
            ListView.SelectedListViewItemCollection item = listView.SelectedItems;

            //listviewのかうんとが０のとき
            if(listView.SelectedItems.Count > 0)
            { 
            id_box.Text = item[0].SubItems[0].Text;
            name_box.Text = item[0].SubItems[1].Text;
            age_box.Text = item[0].SubItems[2].Text;
            sex_box.Text = item[0].SubItems[3].Text;
            affiliation_box.Text = item[0].SubItems[4].Text;
            comment_box.Text = item[0].SubItems[5].Text;
            }
         
            //↑以外の時（追加、削除したとき）
            else
            {
                id_box.Text = "";
                name_box.Text = "";
                sex_box.Text = "";
                age_box.Text = "";
                affiliation_box.Text = "";
                comment_box.Text = "";
            }
            
        }

        //とりこみ（起動時にCSVから表示）
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //ファイルパスの指定
            string path = ConfigurationManager.AppSettings["CSVfilePath"];
            //streamReaderの定義（ファイル開けるようにしてる）
            StreamReader streamReader = new StreamReader(path);
            Console.WriteLine(path);
           //ファイルに文字があるだけループする
            while (streamReader.Peek() != -1)
            {
                //よみこむ
                string result = streamReader.ReadLine();
                //読み込んだものをスプリットで配列にする
                string[] listviewresults = result.Split(',');
                //配列にしたものをリストビューに入れる
                ListViewItem lvi = listView1.Items.Add(listviewresults[0].Replace("#commma#", ","));
                lvi.SubItems.Add(listviewresults[1].Replace("#commma#", ","));
                lvi.SubItems.Add(listviewresults[2].Replace("#commma#", ","));
                lvi.SubItems.Add(listviewresults[3].Replace("#commma#", ","));
                lvi.SubItems.Add(listviewresults[4].Replace("#commma#", ","));
                lvi.SubItems.Add(listviewresults[5].Replace("#commma#", ","));
            }
            //ファイルを閉じる
            streamReader.Close();
        }

       //閉じたときにCSVに保存
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ファイルパスの指定
            string path = ConfigurationManager.AppSettings["CSVfilePath"];
            //steamwriterという型のswという変数に、()のなかでファイルの場所と名前を指定したものを代入
            //（）のなかはダイアログで指定したファイルのこと↓
            StreamWriter sw = new StreamWriter(path);

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //リストビューのアイテムをカンマ区切りに並べたものをＳに代入
                //文字列の、は#comma#に置き換え
                string s =
                     (listView1.Items[i].SubItems[0].Text).Replace(",", "#commma#") +
                     "," +
                     (listView1.Items[i].SubItems[1].Text).Replace(",", "#commma#") +
                     "," +
                     (listView1.Items[i].SubItems[2].Text).Replace(",", "#commma#") +
                     "," +
                     (listView1.Items[i].SubItems[3].Text).Replace(",", "#commma#") +
                     "," +
                     (listView1.Items[i].SubItems[4].Text).Replace(",", "#commma#") +
                     "," +
                     (listView1.Items[i].SubItems[5].Text).Replace(",", "#commma#");
                //swのなかにｓを１行ずつ書き込んで保存する
                sw.WriteLine(s);
            }
            //ファイルをとじる
            sw.Close();
        }
    }
}

