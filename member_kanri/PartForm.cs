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
using MySql.Data.MySqlClient;


namespace member_kanri
{
    public partial class PartForm : Form
    {　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　
        public PartForm()
        {
            InitializeComponent();
        }

        //開いたときにリストボックスにDBのPARTINFOの情報を表示
        private void PartForm_Load(object sender, EventArgs e)
        {
            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);
            var loadConnection = new MySqlConnection(connectionString);

            //削除フラグが立ってないものだけ表示
            var partInfo = "SELECT ID,NAME FROM PARTINFO WHERE DELETE_FLG =0 ORDER BY CAST(ID AS SIGNED)";
            var partCommand = new MySqlCommand(partInfo, loadConnection);
            loadConnection.Open();
            var partReader = partCommand.ExecuteReader();
            {
                //実行（↑までは準備）
                while (partReader.Read())
                {   
                    //IDとNAMEを空白区切りで表示
                    listBox1.Items.Add(partReader["ID"].ToString() + " " + partReader["NAME"].ToString());
                }
                loadConnection.Close();
            }

            
        }

       /* //りすとぼっくす 選択されたらリストボックスへ表示
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int i=0; i < listBox1.SelectedItems.Count; i++)
            {
                PARTtextbox.Text = listBox1.SelectedItems[i].ToString();
            }
        }
*/

        //ついか
        private void addition_button_Click(object sender, EventArgs e)
        {

            //textboxの中身をリストボックスへ
            // listBox1.Items.Add((listBox1.Items.Count + 1) + " " + PARTtextbox.Text);


            //追加を押したらDBへ、IDは最後の番号＋１
            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);
            var loadConnection = new MySqlConnection(connectionString);
            
            var partInsert = "INSERT INTO PARTINFO(ID,NAME,DELETE_FLG) VALUES (@id,@name,@delete_flg) ";

            //PARTINFOのIDをカウントした結果＋１のカラムをAS ROWNUMで ROWNUMという名前にした
            var rownum = "SELECT COUNT(ID) + 1 AS ROWNUM FROM PARTINFO";

            var insertCommand= new MySqlCommand(partInsert, loadConnection);
            var rownumCommand= new MySqlCommand(rownum, loadConnection);

            //PARTINFOのdeleteフラグが０の最新1件をせれくと
            var partInsertView = "SELECT ID,NAME FROM PARTINFO  WHERE DELETE_FLG =0 ORDER BY CAST(ID AS SIGNED) DESC LIMIT 1";
            var partViewCommand = new MySqlCommand(partInsertView, loadConnection);


            loadConnection.Open();

            var NAME = PARTtextbox.Text;
            //追加したらdeleteフラグは０
            var DELETE_FLG = "0";

            var rownumReader = rownumCommand.ExecuteReader();

            //IDの宣言
            var ID = "";
            while (rownumReader.Read())
            {
                //IDにはせれくとしたときにROWNUMと名前を付けたものをいれる
                ID = rownumReader["ROWNUM"].ToString();
                break;
            }
            loadConnection.Close();

            loadConnection.Open();
            insertCommand.Parameters.AddWithValue("@id", ID);
            insertCommand.Parameters.AddWithValue("@name", NAME);
            insertCommand.Parameters.AddWithValue("@delete_flg", DELETE_FLG);

            insertCommand.ExecuteNonQuery();
            var partViewReader = partViewCommand.ExecuteReader();
            {
                //実行（↑までは準備）
                while (partViewReader.Read())
                {
                    //IDとNAMEを空白区切りで表示
                    listBox1.Items.Add(partViewReader["ID"].ToString() + " " + partViewReader["NAME"].ToString());
                }
            }
            loadConnection.Close();
            PARTtextbox.Text = "";
        }

        //とじる
        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //削除（リストビューからは消して、DBは消さずにdeleteフラグたてる）
        private void delete_button_Click(object sender, EventArgs e)
        {
            // MySQLへの接続
            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);
            var connection = new MySqlConnection(connectionString);
            //listviewのアイテムをDBへ更新

            //もし選択されたら、選択されたものを消す
            if (listBox1.SelectedItems.Count > 0)
            {
                //DBの削除フラグを上げる
                var userDelete = "UPDATE PARTINFO SET DELETE_FLG =1  WHERE @id=ID";
                var deleteCommand = new MySqlCommand(userDelete, connection);
                connection.Open();

                //空白区切りのリストボックスの中身を配列にする
                string text = listBox1.Text;
                string[] splittext = text.Split(' ');

                var DELETE_ID = splittext[0];
               
                deleteCommand.Parameters.AddWithValue("@id", DELETE_ID);

                deleteCommand.ExecuteNonQuery();
                connection.Close();

                //listboxから消す
                if (listBox1.SelectedIndex != -1)
                {
                    listBox1.Items.Remove(listBox1.Items[listBox1.SelectedIndex]);
                }

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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
