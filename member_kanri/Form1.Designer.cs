namespace member_kanri
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.name_box = new System.Windows.Forms.TextBox();
            this.comment_box = new System.Windows.Forms.TextBox();
            this.close_button = new System.Windows.Forms.Button();
            this.addition_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lable = new System.Windows.Forms.Label();
            this.id_box = new System.Windows.Forms.TextBox();
            this.sex_box = new System.Windows.Forms.ComboBox();
            this.age_box = new System.Windows.Forms.NumericUpDown();
            this.affiliation_box = new System.Windows.Forms.ComboBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.age_box)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(121, 250);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "所属";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(121, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "年齢";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(123, 197);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 30);
            this.label4.TabIndex = 3;
            this.label4.Text = "性別";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(90, 298);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 30);
            this.label5.TabIndex = 4;
            this.label5.Text = "コメント";
            // 
            // name_box
            // 
            this.name_box.AcceptsReturn = true;
            this.name_box.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.name_box.Location = new System.Drawing.Point(205, 94);
            this.name_box.Margin = new System.Windows.Forms.Padding(5);
            this.name_box.Multiline = true;
            this.name_box.Name = "name_box";
            this.name_box.Size = new System.Drawing.Size(395, 39);
            this.name_box.TabIndex = 1;
            // 
            // comment_box
            // 
            this.comment_box.AcceptsReturn = true;
            this.comment_box.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comment_box.Location = new System.Drawing.Point(204, 298);
            this.comment_box.Margin = new System.Windows.Forms.Padding(4);
            this.comment_box.Multiline = true;
            this.comment_box.Name = "comment_box";
            this.comment_box.Size = new System.Drawing.Size(395, 209);
            this.comment_box.TabIndex = 5;
            // 
            // close_button
            // 
            this.close_button.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.close_button.ForeColor = System.Drawing.Color.Teal;
            this.close_button.Location = new System.Drawing.Point(482, 738);
            this.close_button.Margin = new System.Windows.Forms.Padding(4);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(118, 39);
            this.close_button.TabIndex = 11;
            this.close_button.Text = "とじる";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // addition_button
            // 
            this.addition_button.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.addition_button.ForeColor = System.Drawing.Color.Teal;
            this.addition_button.Location = new System.Drawing.Point(202, 738);
            this.addition_button.Margin = new System.Windows.Forms.Padding(4);
            this.addition_button.Name = "addition_button";
            this.addition_button.Size = new System.Drawing.Size(118, 39);
            this.addition_button.TabIndex = 7;
            this.addition_button.Text = "追加,更新";
            this.addition_button.UseVisualStyleBackColor = true;
            this.addition_button.Click += new System.EventHandler(this.addition_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.delete_button.ForeColor = System.Drawing.Color.Teal;
            this.delete_button.Location = new System.Drawing.Point(338, 738);
            this.delete_button.Margin = new System.Windows.Forms.Padding(4);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(118, 39);
            this.delete_button.TabIndex = 10;
            this.delete_button.Text = "削除";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(121, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 30);
            this.label1.TabIndex = 14;
            this.label1.Text = "名前";
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable.Location = new System.Drawing.Point(95, 48);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(103, 30);
            this.lable.TabIndex = 17;
            this.lable.Text = "社員ID";
            // 
            // id_box
            // 
            this.id_box.AcceptsReturn = true;
            this.id_box.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.id_box.Location = new System.Drawing.Point(204, 45);
            this.id_box.Multiline = true;
            this.id_box.Name = "id_box";
            this.id_box.Size = new System.Drawing.Size(395, 39);
            this.id_box.TabIndex = 0;
            // 
            // sex_box
            // 
            this.sex_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sex_box.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sex_box.FormattingEnabled = true;
            this.sex_box.Items.AddRange(new object[] {
            "男",
            "女"});
            this.sex_box.Location = new System.Drawing.Point(205, 194);
            this.sex_box.Name = "sex_box";
            this.sex_box.Size = new System.Drawing.Size(131, 38);
            this.sex_box.TabIndex = 19;
            // 
            // age_box
            // 
            this.age_box.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.age_box.Location = new System.Drawing.Point(203, 145);
            this.age_box.Name = "age_box";
            this.age_box.Size = new System.Drawing.Size(133, 37);
            this.age_box.TabIndex = 20;
            // 
            // affiliation_box
            // 
            this.affiliation_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.affiliation_box.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.affiliation_box.FormattingEnabled = true;
            this.affiliation_box.Items.AddRange(new object[] {
            "営業部",
            "人事部",
            "開発部"});
            this.affiliation_box.Location = new System.Drawing.Point(202, 247);
            this.affiliation_box.Name = "affiliation_box";
            this.affiliation_box.Size = new System.Drawing.Size(134, 38);
            this.affiliation_box.TabIndex = 21;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(100, 512);
            this.listView1.Margin = new System.Windows.Forms.Padding(1);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(567, 217);
            this.listView1.TabIndex = 22;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ID";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 61;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "名前";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 127;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "年齢";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 56;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "性別";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 67;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "所属";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader12.Width = 62;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "コメント";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader13.Width = 130;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(744, 790);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.affiliation_box);
            this.Controls.Add(this.age_box);
            this.Controls.Add(this.sex_box);
            this.Controls.Add(this.id_box);
            this.Controls.Add(this.lable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.addition_button);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.comment_box);
            this.Controls.Add(this.name_box);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.MintCream;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.age_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox name_box;
        private System.Windows.Forms.TextBox comment_box;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button addition_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lable;
        private System.Windows.Forms.TextBox id_box;
        private System.Windows.Forms.ComboBox sex_box;
        private System.Windows.Forms.NumericUpDown age_box;
        private System.Windows.Forms.ComboBox affiliation_box;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
    }
}

