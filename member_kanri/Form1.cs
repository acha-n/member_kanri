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

        //追加
        private void addition_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                //リストボックスの中の行の集まりの中の［i］行目(1人分) 
                //listBox1.Items[i];をhogeという変数に代入してる。ifで省略して書ける
                //object hoge = listBox1.Items[i];

                //idboxにはいってる値と同じ値をもつ行があるとき（リストボックスの中のiの情報のなかの[0]）
                if (id_box.Text == listBox1.Items[i].ToString().Split(' ')[0])
                {
                    //エラーメッセージを表示して処理を終了
                    MessageBox.Show("入力できません",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            listBox1.Items.Add(id_box.Text + " " + name_box.Text + " " + sex_box.Text + " " + age_box.Text + " " + affiliation_box.Text + " " + comment_box.Text);
        }
        //削除
        private void delete_button_Click(object sender, EventArgs e)
        {
            //もし選択されたら、選択されたものを消す
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
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
        //リストボックス
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ListBox listBox = sender as ListBox;
            //listbox.textをtextにした
            string text = listBox.Text;
            //↑のtextを.spritで配列にする
            string[] splitText = text.Split(' ');
            id_box.Text = splitText[0];
            name_box.Text = splitText[1];
            sex_box.Text = splitText[2];
            age_box.Text = splitText[3];
            affiliation_box.Text = splitText[4];
            comment_box.Text = splitText[5];
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

      
    }
}
