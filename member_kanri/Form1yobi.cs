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
    public partial class Form1 : Form
    {
        List<String> partManage = new List<string>();
        //Dictionary<string, string> partInfo = new Dictionary<string, string>(); 
        public Form1()
        {
            InitializeComponent();

            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            //MYSQLの接続情報
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);

            //↑の接続情報をつかって接続じゅんぴしてる
            var connection = new MySqlConnection(connectionString);

            // MySQLでやりたいSQL文
            //()のなかはPARTINFOのIDとUSERINFOのPARTがおなじものを　AS PARTNAMEでPARTNAMEにかくのう
            var userInfo = "SELECT ID,NAME,AGE,SEX, (SELECT NAME FROM PARTINFO WHERE ID=KEKKA.PART ) " +
                           "AS PARTNAME ,COMMENT, PART FROM USERINFO KEKKA ORDER BY CAST(ID AS SIGNED)";
            var partInfo = "SELECT * FROM PARTINFO WHERE DELETE_FLG = 0";

            // 指定したDBの情報とSQL文をmysqlCommandが実行してくれてる（のをuserinfocommandという変数にしてる）
            var userInfoCommand = new MySqlCommand(userInfo, connection);
            var partInfoCommand = new MySqlCommand(partInfo, connection);
            {
                // 接続開始
                connection.Open();

                //所属のみもってきて所属ボックスへ
                var partReader = partInfoCommand.ExecuteReader();
                {
                    //実行（↑までは準備）
                    while (partReader.Read())
                    {
                        affiliation_box.Items.Add(partReader["NAME"].ToString());
                        partManage.Add(partReader["ID"].ToString());
                    }
                    connection.Close();
                }
                connection.Open();
                // SELECT文の実行
                var userReader = userInfoCommand.ExecuteReader();
                // 1行ずつ読み取ってlistviewに表示
                while (userReader.Read())
                {
                    //性別　1なら男2なら女
                    ListViewItem lvi = listView1.Items.Add(userReader["ID"].ToString());
                    lvi.SubItems.Add(userReader["NAME"].ToString());
                    lvi.SubItems.Add(userReader["AGE"].ToString());
                    lvi.SubItems.Add((userReader["SEX"].ToString() == "1") ? "男" : "女");
                    lvi.SubItems.Add(userReader["PARTNAME"].ToString());
                    lvi.SubItems.Add(userReader["COMMENT"].ToString());

                    //コメントの後ろにPARTのIDがはいってる（見えない）
                    lvi.SubItems.Add(userReader["PART"].ToString());                  
                }
                connection.Close();
            }
            //deleteフラグが１のとき
            var deleteItem = "SELECT NAME FROM PARTINFO WHERE DELETE_FLG = 1";
            var deleteItemCommand = new MySqlCommand(deleteItem, connection);

            connection.Open();
            var deleteItemReader = deleteItemCommand.ExecuteReader();
            //iの宣言
            var i = 0;
            //deleteフラグが1ならPARTINFOのNAMEを読み込んで赤にする
            while (deleteItemReader.Read())
            {
                if (listView1.Items[i].SubItems[4].Text == deleteItemReader["NAME"].ToString())
                {
                    listView1.Items[i].UseItemStyleForSubItems = false;
                    listView1.Items[i].SubItems[4].ForeColor = Color.Red;
                }　
                else
                {                 
                }
                //whileが1回回るごとに1を追加（forと違ってiの値は増えない）
                i++;
            }
            connection.Close();
        }

        //追加、更新
        private void addition_button_Click(object sender, EventArgs e)
        {
            // MySQLへの接続
            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);

            //PARTINFOのIDとNAMEをせれくと
            var partInfo = "SELECT ID, NAME FROM PARTINFO";
            var connection = new MySqlConnection(connectionString);
            var partInfoCommand = new MySqlCommand(partInfo, connection);

            for (var i = 0; i < listView1.Items.Count; i++)
            {
                //リストボックスの中の行の集まりの中の［i］行目(1人分) 
                //string listId = listView1.Items[i].Text;
                //idboxにはいってる値と同じ値をもつ行があるとき（リストボックスの中のiの情報のなかの[0]）
                if (id_box.Text == listView1.Items[i].Text)
                {
                    //USERINFOの情報を持ってきて、同じIDを探す、値が同じなら普通に更新、違うのがあれば再読み込み
                    var userEdit = "SELECT ID,NAME,AGE,SEX, (SELECT NAME FROM PARTINFO WHERE ID=KEKKA.PART ) " +
                                   "AS PARTNAME ,COMMENT, PART FROM USERINFO KEKKA ORDER BY CAST(ID AS SIGNED)";
                    var editConnection = new MySqlConnection(connectionString);
                    var editCommand = new MySqlCommand(userEdit, editConnection);
                    editConnection.Open();
                    var editReader = editCommand.ExecuteReader();

                    while (editReader.Read())
                    {
                        if ((listView1.Items[i].SubItems[1].Text != editReader["NAME"].ToString()) |
                            (listView1.Items[i].SubItems[2].Text != editReader["AGE"].ToString()) | (listView1.Items[i].SubItems[3].Text != editReader["SEX"].ToString()) |
                            (listView1.Items[i].SubItems[4].Text != editReader["PARTNAME"].ToString()) | (listView1.Items[i].SubItems[5].Text != editReader["COMMENT"].ToString()))
                        {
                            MessageBox.Show("再読み込みしてください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if ((listView1.Items[i].SubItems[1].Text == editReader["NAME"].ToString()) &
                            (listView1.Items[i].SubItems[2].Text == editReader["AGE"].ToString()) & (listView1.Items[i].SubItems[3].Text == editReader["SEX"].ToString()) &
                            (listView1.Items[i].SubItems[4].Text == editReader["PARTNAME"].ToString()) & (listView1.Items[i].SubItems[4].Text == editReader["COMMENT"].ToString()))
                        {
                            if (MessageBox.Show("上書きしますか", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                //UPDATE
                                ListViewItem target = listView1.Items[i];
                                target.SubItems[1].Text = name_box.Text;
                                target.SubItems[2].Text = age_box.Text;
                                target.SubItems[3].Text = sex_box.Text;
                                target.SubItems[4].Text = affiliation_box.Text;
                                target.SubItems[5].Text = comment_box.Text;

                                var userUpdate = "UPDATE USERINFO SET ID=@id,NAME=@name,AGE=@age,SEX=@sex,PART=@part,COMMENT=@comment WHERE ID=@id";
                                var ID_UPDATE = listView1.Items[i].Text;
                                var NAME_UPDATE = listView1.Items[i].SubItems[1].Text;
                                var AGE_UPDATE = listView1.Items[i].SubItems[2].Text;
                                var SEX_UPDATE = listView1.Items[i].SubItems[3].Text;
                                //性別を戻す
                                if (sex_box.Text == "男")
                                {
                                    SEX_UPDATE = "1";
                                }
                                else
                                {
                                    SEX_UPDATE = "2";
                                }
                                var PART_UPDATE = listView1.Items[i].SubItems[4].Text;
                                connection.Open();
                                //所属をIDでDBに入れる
                                var partUpdate = new Dictionary<string, string>();
                                var partInfoReader2 = partInfoCommand.ExecuteReader();
                                /* //↓のPARTNAMEとIDを追加
                               while (partInfoReader2.Read())
                               {
                                   partUpdate.Add(partInfoReader2["ID"].ToString(), partInfoReader2["NAME"].ToString());
                                }*/
                                foreach (KeyValuePair<string, string> kvp in partUpdate)
                                {
                                    if (affiliation_box.Text == kvp.Value)
                                    {
                                        PART_UPDATE = kvp.Key;
                                    }
                                }
                                connection.Close();

                                //deleteフラグが１で赤の所属を変更したとき
                                var deleteItem = "SELECT NAME FROM PARTINFO WHERE DELETE_FLG = 1";
                                var deleteItemCommand = new MySqlCommand(deleteItem, connection);
                                connection.Open();
                                var deleteItemReader = deleteItemCommand.ExecuteReader();
                                //iの宣言
                                //var i = 0;
                                //deleteフラグが1でないならPARTINFOのNAMEを読み込んで黒にする
                                while (deleteItemReader.Read())
                                {
                                    if (listView1.Items[i].SubItems[4].Text != deleteItemReader["NAME"].ToString())
                                    {
                                        listView1.Items[i].UseItemStyleForSubItems = false;
                                        listView1.Items[i].SubItems[4].ForeColor = Color.Black;
                                    }
                                    else
                                    {
                                    }
                                    //whileが1回回るごとに1を追加（forと違ってiの値は増えない）
                                    i++;
                                }
                                connection.Close();
                                var COMMENT_UPDATE = listView1.Items[i].SubItems[5].Text;
                                var upDateCommand = new MySqlCommand(userUpdate, connection);
                                //listviewのアイテムをDBへ更新
                                connection.Open();
                                upDateCommand.Parameters.AddWithValue("@id", ID_UPDATE);
                                upDateCommand.Parameters.AddWithValue("@name", NAME_UPDATE);
                                upDateCommand.Parameters.AddWithValue("@age", AGE_UPDATE);
                                upDateCommand.Parameters.AddWithValue("@sex", SEX_UPDATE);
                                upDateCommand.Parameters.AddWithValue("@part", PART_UPDATE);
                                upDateCommand.Parameters.AddWithValue("@comment", COMMENT_UPDATE);
                                upDateCommand.ExecuteNonQuery();
                                connection.Close();
                                /*//更新と追加のSQL文準備
                                var userupdate =
                                "UPDATE USERINFO SET " +
                                "ID='" + listView1.Items[i].Text + "'," +
                                "NAME='" + listView1.Items[i].SubItems[1].Text + "'," +
                                "AGE='" + listView1.Items[i].SubItems[2].Text + "'," +
                                "SEX='" + listView1.Items[i].SubItems[3].Text + "'," +
                                "PART='" + listView1.Items[i].SubItems[4].Text + "'," +
                                "COMMENT='" + listView1.Items[i].SubItems[5].Text + "'" +
                                "WHERE ID ='" + listView1.Items[i].Text + "'";*/
                            }
                        }                    
                    }
                    editConnection.Close();
                }
            }

           
                // //idboxにはいってる値と同じ値をもつ行がないとき（リストボックスの中のiの情報のなかの[0]）(INSERT)
                //↑じゃない場合、テキストボックスの中身を空白いれてリストボックスに追加
                // listView1.Items.Add(id_box.Text).SubItems.Add(name_box.Text);
                ListViewItem lvi = listView1.Items.Add(id_box.Text);
                lvi.SubItems.Add(name_box.Text);
                lvi.SubItems.Add(age_box.Text);
                lvi.SubItems.Add(sex_box.Text);
                lvi.SubItems.Add(affiliation_box.Text);
                lvi.SubItems.Add(comment_box.Text);


                var userInsert =
                 "INSERT INTO USERINFO (ID, NAME, AGE, SEX, PART, COMMENT) VALUES " +
                 "(@id,@name,@age,@sex,@part,@comment)";

                var ID_INSERT = id_box.Text;
                var NAME_INSERT = name_box.Text;
                var AGE_INSERT = age_box.Text;
                var SEX_INSERT = sex_box.Text;
                //性別を戻す
                if (sex_box.Text == "男")
                {
                    SEX_INSERT = "1";
                }
                else
                {
                    SEX_INSERT = "2";
                }
                var PART_INSERT = affiliation_box.Text;
                /*1こづついふかく
                 * if (affiliation_box.Text == "阪神タイガース" ) {
                    PART = "1";
                }
    */

                //dictionarｙつかう
                var partInsert = new Dictionary<string, string>();
                connection.Open();
                var partInfoReader = partInfoCommand.ExecuteReader();

                //UPDATEと同じ理由
                while (partInfoReader.Read())
                {
                    partInsert.Add(partInfoReader["ID"].ToString(), partInfoReader["NAME"].ToString());
                }

                foreach (KeyValuePair<string, string> kvp in partInsert)
                {
                    if (affiliation_box.Text == kvp.Value)
                    {
                        PART_INSERT = kvp.Key;
                    }

                }
                connection.Close();
                var COMMENT_INSERT = comment_box.Text;

                var insertCommand = new MySqlCommand(userInsert, connection);
                connection.Open();

                insertCommand.Parameters.AddWithValue("@id", ID_INSERT);
                insertCommand.Parameters.AddWithValue("@name", NAME_INSERT);
                insertCommand.Parameters.AddWithValue("@age", AGE_INSERT);
                insertCommand.Parameters.AddWithValue("@sex", SEX_INSERT);
                insertCommand.Parameters.AddWithValue("@part", PART_INSERT);
                insertCommand.Parameters.AddWithValue("@comment", COMMENT_INSERT);

                insertCommand.ExecuteNonQuery();

                connection.Close();
                /*" +
                "ID= '" + id_box.Text + "'," +
                "NAME= '" + name_box.Text + "'," +
                "AGE= '" + age_box.Text + "'," +
                "SEX= '" + sex_box.Text + "'," +
                "PART= '" + affiliation_box.Text + "'," +
                "COMMENT= '" + comment_box.Text + "')";*/
            
        }
       
        //削除
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
            if (listView1.SelectedItems.Count > 0)
            {
                var userdelete = "DELETE FROM USERINFO WHERE ID=@id" ;
                var deleteCommand = new MySqlCommand(userdelete, connection);
                connection.Open();
                deleteCommand.Parameters.AddWithValue("@id", listView1.SelectedItems[0].Text);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
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
            connection.Close();
        }

        //とじるぼたん
        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //りすとびゅー
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MySQLへの接続
            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);
            var connection = new MySqlConnection(connectionString);

            //選択したらテキストボックスに表示↓
            ListView listView = (ListView)sender;
            //listview.textをtextという変数にした
            ListView.SelectedListViewItemCollection item = listView.SelectedItems;

            //listviewのかうんとが０のとき
            if (listView.SelectedItems.Count > 0)
            {
                id_box.Text = item[0].SubItems[0].Text;
                name_box.Text = item[0].SubItems[1].Text;
                age_box.Text = item[0].SubItems[2].Text;
                sex_box.Text = item[0].SubItems[3].Text;
                //affiliation_box.Text = item[0].SubItems[4].Text;

                //所属をIDで判別
                for(int i = 0; i < partManage.Count; i++)
                {
                    if (partManage[i] == item[0].SubItems[6].Text)
                    {
                        affiliation_box.SelectedIndex= i;
                    }
                }
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

        //とりこみ（起動時にDBから表示）
        /* private void Form1_Load(object sender, EventArgs e)
         {

             //ファイルパスの指定
             string path = ConfigurationManager.AppSettings["CSVfilePath"];
             //streamReaderの定義（ファイル開けるようにしてる）
             StreamReader streamReader = new StreamReader(path);
            //ファイルに文字があるだけループする
             while (streamReader.Peek() != -1)
             {
                 //よみこむ
                 string result = streamReader.ReadLine();
                 //読み込んだものをスプリットで配列にする
                 string[] listviewresults = result.Split(',');
                 //配列にしたものをリストビューに入れる
                 *//* ListViewItem lvi = listView1.Items.Add(listviewresults[0].Replace("#commma#", ","));
                 lvi.SubItems.Add(listviewresults[1].Replace("#commma#", ","));
                 lvi.SubItems.Add(listviewresults[2].Replace("#commma#", ","));
                 lvi.SubItems.Add(listviewresults[3].Replace("#commma#", ","));
                 lvi.SubItems.Add(listviewresults[4].Replace("#commma#", ","));
                 lvi.SubItems.Add(listviewresults[5].Replace("#commma#", ","));*//*
             }
             //ファイルを閉じる
             streamReader.Close();
         }*/

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



        //編集
        private void button1_Click(object sender, EventArgs e)
        {
            Form　part = new PartForm();
            part.ShowDialog();
        }

        //再読み込み
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            //MYSQLの接続情報
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);

            //↑の接続情報をつかって接続じゅんぴしてる
            var connection = new MySqlConnection(connectionString);

            // MySQLでやりたいSQL文
            var userInfo = "SELECT ID,NAME,AGE,SEX, (SELECT NAME FROM PARTINFO WHERE ID=KEKKA.PART ) " +
                           "AS PARTNAME ,COMMENT, PART FROM USERINFO  KEKKA ORDER BY CAST(ID AS SIGNED)";
            var partInfo = "SELECT * FROM PARTINFO WHERE DELETE_FLG = 0";


            // 指定したDBの情報とSQL文をmysqlCommandが実行してくれてる（のをuserinfocommandという変数にしてる）
            var userInfoCommand = new MySqlCommand(userInfo, connection);
            var partInfoCommand = new MySqlCommand(partInfo, connection);
            {
                // 接続開始
                connection.Open();

                // SELECT文の実行
                var userReader = userInfoCommand.ExecuteReader();
                // 1行ずつ読み取ってlistviewに表示
                while (userReader.Read())
                {
                    //性別　1なら男2なら女
                    ListViewItem lvi = listView1.Items.Add(userReader["ID"].ToString());
                    lvi.SubItems.Add(userReader["NAME"].ToString());
                    lvi.SubItems.Add(userReader["AGE"].ToString());
                    lvi.SubItems.Add((userReader["SEX"].ToString() == "1") ? "男" : "女");
                    lvi.SubItems.Add(userReader["PARTNAME"].ToString());
                    lvi.SubItems.Add(userReader["COMMENT"].ToString());
                    lvi.SubItems.Add(userReader["PART"].ToString());

                }
                connection.Close();


                //deleteフラグが１のとき
                var deleteItem = "SELECT NAME FROM PARTINFO WHERE DELETE_FLG = 1";
                var deleteItemCommand = new MySqlCommand(deleteItem, connection);

                connection.Open();
                var deleteItemReader = deleteItemCommand.ExecuteReader();
                //iの宣言
                var i = 0;
                //deleteフラグが1ならPARTINFOのNAMEを読み込んで赤にする
                while (deleteItemReader.Read())
                {
                    if (listView1.Items[i].SubItems[4].Text == deleteItemReader["NAME"].ToString())
                    {
                        listView1.Items[i].UseItemStyleForSubItems = false;
                        listView1.Items[i].SubItems[4].ForeColor = Color.Red;
                    }
                    else
                    {
                        listView1.Items[i].UseItemStyleForSubItems = false;
                        listView1.Items[i].SubItems[4].ForeColor = Color.Black;
                    }
                    //whileが1回回るごとに1を追加（forと違ってiの値は増えない）
                    i++;
                }
                connection.Close();
            }
        }

    }
}
    

