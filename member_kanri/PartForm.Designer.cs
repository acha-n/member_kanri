namespace member_kanri
{
    partial class PartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addition_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.PARTtextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addition_button
            // 
            this.addition_button.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addition_button.ForeColor = System.Drawing.Color.DarkCyan;
            this.addition_button.Location = new System.Drawing.Point(231, 517);
            this.addition_button.Name = "addition_button";
            this.addition_button.Size = new System.Drawing.Size(118, 54);
            this.addition_button.TabIndex = 0;
            this.addition_button.Text = "追加";
            this.addition_button.UseVisualStyleBackColor = true;
            this.addition_button.Click += new System.EventHandler(this.addition_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete_button.ForeColor = System.Drawing.Color.DarkCyan;
            this.delete_button.Location = new System.Drawing.Point(372, 517);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(118, 55);
            this.delete_button.TabIndex = 1;
            this.delete_button.Text = "削除";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // close_button
            // 
            this.close_button.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_button.ForeColor = System.Drawing.Color.DarkCyan;
            this.close_button.Location = new System.Drawing.Point(520, 517);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(118, 55);
            this.close_button.TabIndex = 2;
            this.close_button.Text = "とじる";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(151, 210);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(559, 244);
            this.listBox1.TabIndex = 3;
            //this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // PARTtextbox
            // 
            this.PARTtextbox.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PARTtextbox.Location = new System.Drawing.Point(151, 112);
            this.PARTtextbox.Name = "PARTtextbox";
            this.PARTtextbox.Size = new System.Drawing.Size(559, 47);
            this.PARTtextbox.TabIndex = 4;
            // 
            // PartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(864, 625);
            this.Controls.Add(this.PARTtextbox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.addition_button);
            this.Name = "PartForm";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.PartForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addition_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox PARTtextbox;
    }
}