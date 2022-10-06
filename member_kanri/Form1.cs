using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                //object hoge = listView1.Items[i];

                //idboxにはいってる値と同じ値をもつ行があるとき（リストボックスの中のiの情報のなかの[0]）
                if (id_box.Text == listView1.Items[i].ToString().Split(' ')[0])
                {
                    //メッセージボックスを出す
                    MessageBox.Show("上書きしますか", "確認",
                        MessageBoxButtons.YesNo);

                    //yesを押したとき（テキストボックスに入ってるテキストをリストボックス内の同じIDに上書き）
                    if (MessageBox.Show("上書きしますか", "確認",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //listView1.Items[i] = id_box.Text + " " + name_box.Text + " " + sex_box.Text + " " + age_box.Text + " " + affiliation_box.Text + " " + comment_box.Text;
                        listView1.Items.Add(id_box.Text + " " + name_box.Text  + " " + age_box.Text + " " + sex_box.Text + " " + affiliation_box.Text + " " + comment_box.Text);
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
    }
}