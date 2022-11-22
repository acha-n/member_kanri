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

enum TableStatus
{
    NotFound = 0,
    Same = 1,
    Different = 2
}

namespace member_kanri
{
    //立ち上げたときに表示
    public partial class Form1 : Form
    {
        List<String> partManage;//= new List<string>();
                                // private bool insertReader;

        //Dictionary<string, string> partInfo = new Dictionary<string, string>(); 
        public Form1()
        {
            InitializeComponent();

            MySqlConnection connection = getConnection();

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
                partManage = new List<string>();

                //実行（↑までは準備）
                while (partReader.Read())
                {
                    partManage.Add(partReader["ID"].ToString());
                    affiliation_box.Items.Add(partReader["NAME"].ToString());
                }
                connection.Close();

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

        /// <summary>
        /// 追加、更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addition_button_Click(object sender, EventArgs e)
        {
            //PARTINFOのIDとNAMEをせれくと
            var partInfo = "SELECT ID, NAME FROM PARTINFO";
            //接続情報
            MySqlConnection connection = getConnection();
            var partInfoCommand = new MySqlCommand(partInfo, connection);
            /*  List<string> partid;
              connection.Open();
              var partInfoReader2 = partInfoCommand.ExecuteReader();
              partid = new List<string>();
              //↓のPARTNAMEとIDを追加
              while (partInfoReader2.Read())
              {
                  partid.Add(partInfoReader2["ID"].ToString());
                  affiliation_box.Items.Add(partInfoReader2["NAME"].ToString());
              }
              connection.Close();*/
            //更新したかどうかのフラグ（更新したらtrue、trueなら追加されない）
            bool isDone = false;

            //まず更新なのか追加なのかを確定する。
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                // 同じものがあったばあいはtrueにする（追加できない）
                if (listView1.Items[i].Text == id_box.Text)
                {
                    isDone = true;
                    break;
                }
            }

            //更新処理
            if (isDone)
            {
                //USERINFOの情報を持ってきて、同じIDを探す、値が同じなら普通に更新、違うのがあれば再読み込み
                var userEdit = "SELECT ID,NAME,AGE,SEX, (SELECT NAME FROM PARTINFO WHERE ID=KEKKA.PART ) " +
                               "AS PARTNAME ,COMMENT, PART FROM USERINFO KEKKA ORDER BY CAST(ID AS SIGNED)";
                MySqlConnection editConnection = getConnection();
                var editCommand = new MySqlCommand(userEdit, editConnection);
                editConnection.Open();
                var editReader = editCommand.ExecuteReader();
                int i = 0;
                string sex_num;
                if (listView1.Items[i].SubItems[3].Text == "男")
                {
                    sex_num = "1";
                }
                else
                {
                    sex_num = "2";
                }
                while (editReader.Read())

                {  //編集しようしたIDが既に消されてた時                 
                    if (listView1.Items[i].Text != editReader["ID"].ToString())
                    {
                        MessageBox.Show("削除されています", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else //if (listView1.Items[i].Text == editReader["ID"].ToString())
                    {
                        if (listView1.Items[i].SubItems[1].Text != editReader["NAME"].ToString() ||
                            listView1.Items[i].SubItems[2].Text != editReader["AGE"].ToString() ||
                             sex_num != editReader["SEX"].ToString() ||
                            listView1.Items[i].SubItems[5].Text != editReader["COMMENT"].ToString() ||
                            listView1.Items[i].SubItems[6].Text != editReader["PART"].ToString())
                        {

                            Console.WriteLine(editReader["NAME"].ToString());
                            Console.WriteLine(editReader["AGE"].ToString());
                            Console.WriteLine(editReader["SEX"].ToString());
                            Console.WriteLine(editReader["COMMENT"].ToString());
                            Console.WriteLine(editReader["PART"].ToString());

                            MessageBox.Show("再読み込みしてください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (MessageBox.Show("上書きしますか", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                            var PART_UPDATE = partManage[affiliation_box.SelectedIndex];
                            var COMMENT_UPDATE = listView1.Items[i].SubItems[5].Text;
                            //connection.Open();

                            //所属をIDでDBに入れる
                            //var partUpdate = new Dictionary<string, string>();
                            /*  var partInfoReader2 = partInfoCommand.ExecuteReader();
                              partid = new List<string>();
                              //↓のPARTNAMEとIDを追加
                              while (partInfoReader2.Read())
                              {
                                  partid.Add(partInfoReader2["ID"].ToString());
                                  affiliation_box.Items.Add(partInfoReader2["NAME"].ToString());
                              }*/
                            /*    foreach (KeyValuePair<string, string> kvp in partUpdate)
                                {
                                    if (affiliation_box.Text == kvp.Value)
                                    {
                                        PART_UPDATE = kvp.Key;
                                    }
                                }*/
                            //connection.Close();

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
                            return;
                        }
                        //isDone = true;
                        /*editConnection.Close();
                        break;*/
                        break;
                    }
                  
                    isDone = true;
                    editConnection.Close();
                    break;
                }
            }
            //追加処理（isDone=false）
            else
            {
                //追加の場合はまず既に存在するかをチェックする。存在した場合はワーニングしてリロードして終わっちゃう
                //0,0,0,0,0,は、IDは必要だけどそれ以外は何かしら入ってるでしょ見たいな感じ（ダミーで値入れてる）
                if (getRecordStatus(id_box.Text, "0", "0", "0", "0", "0") != TableStatus.NotFound)
                {
                    MessageBox.Show("すでに追加されています", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ReloadData();
                    return;
                }
                //DBにtextbox.text(追加したいID)があるかどうか（あるならtrue）

                // まず追加情報をリストビューに反映する。
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
                var PART_INSERT = partManage[affiliation_box.SelectedIndex];
                //所属をドロップダウンリストのインデックスをもとにlistにしてIDとPARTと一緒にする

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
            }

        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_button_Click(object sender, EventArgs e)
        {

            //もし選択されていない場合エラー
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("削除対象の行を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //削除対象のデータを取得し、チェック関数に投げる
            TableStatus status = getRecordStatus(
                listView1.SelectedItems[0].Text,
                listView1.SelectedItems[0].SubItems[1].Text,
                listView1.SelectedItems[0].SubItems[2].Text,
                listView1.SelectedItems[0].SubItems[3].Text,
                listView1.SelectedItems[0].SubItems[6].Text,
                listView1.SelectedItems[0].SubItems[5].Text);
            if (status == TableStatus.NotFound)
            {
                MessageBox.Show("指定されたデータは既に他のユーザによって削除されています。\nリロードします。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ReloadData();
                return;
            }
            if (status == TableStatus.Different)
            {
                MessageBox.Show("指定されたデータは既に他のユーザによって変更されています。\nリロードします。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ReloadData();
                return;
            }
            if (status==TableStatus.Same)
            {
                if(MessageBox.Show("本当に削除してもよろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MySqlConnection connection = getConnection();
                    var userdelete = "DELETE FROM USERINFO WHERE ID=@id";
                    var userDeleteCommand = new MySqlCommand(userdelete, connection);
                    connection.Open();
                    userDeleteCommand.Parameters.AddWithValue("@id", listView1.SelectedItems[0].Text);
                    userDeleteCommand.ExecuteNonQuery();
                    connection.Close();
                    listView1.Items.RemoveAt(listView1.SelectedItems[0].Index);
                }
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
            MySqlConnection connection = getConnection();

            //選択したらテキストボックスに表示↓
            ListView listView = (ListView)sender;
            //listview.textをtextという変数にした
            ListView.SelectedListViewItemCollection item = listView.SelectedItems;

            //listviewが選択されてるとき
            if (listView.SelectedItems.Count > 0)
            {
                id_box.Text = item[0].SubItems[0].Text;
                name_box.Text = item[0].SubItems[1].Text;
                age_box.Text = item[0].SubItems[2].Text;
                sex_box.Text = item[0].SubItems[3].Text;
                //affiliation_box.Text = item[0].SubItems[4].Text;
                //所属をIDで判別
                for (int i = 0; i < partManage.Count; i++)
                {
                    //subItems[6]のPARTAME
                    if (partManage[i] == item[0].SubItems[6].Text)
                    {
                        affiliation_box.SelectedIndex = i;
                        affiliation_box.Text = item[0].SubItems[4].Text;
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
        /*  private void Form1_FormClosed(object sender, FormClosedEventArgs e)
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
  */
        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form part = new PartForm();
            part.ShowDialog();
        }
        //再読み込み
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ReloadData()
        {
            listView1.Items.Clear();

            MySqlConnection connection = getConnection();

            // MySQLでやりたいSQL文
            var userInfo = "SELECT ID,NAME,AGE,SEX, (SELECT NAME FROM PARTINFO WHERE ID=KEKKA.PART ) " +
                           "AS PARTNAME ,COMMENT, PART FROM USERINFO  KEKKA ORDER BY CAST(ID AS SIGNED)";
            var partInfo = "SELECT * FROM PARTINFO WHERE DELETE_FLG = 0";


            // 指定したDBの情報とSQL文をmysqlCommandが実行してくれてる（のをuserinfocommandという変数にしてる）
            var userInfoCommand = new MySqlCommand(userInfo, connection);
            var partInfoCommand = new MySqlCommand(partInfo, connection);
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
        /// <summary>
        /// 指定されたデータがDBに存在するか？存在した場合一致するかチェックする関数
        /// </summary>
        /// <param name="sID"></param>
        /// <param name="sName"></param>
        /// <param name="sAge"></param>
        /// <param name="sSex"></param>
        /// <param name="sPart"></param>
        /// <param name="sComment"></param>
        /// <returns></returns>
        private TableStatus getRecordStatus(string sID, string sName, string sAge, string sSex, string sPart, string sComment)
        {
            MySqlConnection connection;
            //MYSQLの接続情報
            connection = new MySqlConnection("Server=118.27.38.218;Database=study;Uid=study;Pwd=kanoko20sai;Charset=utf8");
            // MySQLでやりたいSQL文
            String sql = "SELECT * FROM USERINFO  WHERE ID='" + sID + "'";
            MySqlCommand userInfoCommand = new MySqlCommand(sql, connection);
            connection.Open();

            // SELECT文の実行
            var userReader = userInfoCommand.ExecuteReader();
            // 1行ある（存在する場合）
            while (userReader.Read())
            {
                //1つでも差異があった場合
                if (userReader["ID"].ToString() != sID || 
                    userReader["NAME"].ToString() != sName || 
                    userReader["AGE"].ToString() != sAge || 
                    userReader["SEX"].ToString() != sSex ||
                    userReader["PART"].ToString() != sPart || 
                    userReader["COMMENT"].ToString() != sComment)
                {
                    connection.Close();
                    return TableStatus.Different;
                }
                //それ以外（全く一緒）
                else
                {
                    connection.Close();
                    return TableStatus.Same;
                }
            }

            //ここに来る場合は存在しないのが確定
            connection.Close();
            return TableStatus.NotFound;

        }

        /// <summary>
        /// DBコネクションの取得
        /// </summary>
        private MySqlConnection getConnection()
        {
            string server = "118.27.38.218";
            string database = "study";
            string user = "study";
            string pass = "kanoko20sai";
            string charset = "utf8";
            //MYSQLの接続情報
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);

            //↑の接続情報をつかって接続じゅんぴしてる
            return (new MySqlConnection(connectionString));

        }

    }
}
