
namespace GPO
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Auth = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.gettingMembers = new System.Windows.Forms.Button();
            this.idGroupBox = new System.Windows.Forms.TextBox();
            this.ShowMembers = new System.Windows.Forms.Button();
            this.MongoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Auth
            // 
            this.Auth.Location = new System.Drawing.Point(20, 124);
            this.Auth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Auth.Name = "Auth";
            this.Auth.Size = new System.Drawing.Size(138, 35);
            this.Auth.TabIndex = 0;
            this.Auth.Text = "авторизация";
            this.Auth.UseVisualStyleBackColor = true;
            this.Auth.Click += new System.EventHandler(this.Auth_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(289, 34);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(513, 384);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // gettingMembers
            // 
            this.gettingMembers.Location = new System.Drawing.Point(20, 445);
            this.gettingMembers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gettingMembers.Name = "gettingMembers";
            this.gettingMembers.Size = new System.Drawing.Size(272, 35);
            this.gettingMembers.TabIndex = 2;
            this.gettingMembers.Text = "Показать информацию о группе";
            this.gettingMembers.UseVisualStyleBackColor = true;
            this.gettingMembers.Click += new System.EventHandler(this.gettingMembers_Click);
            // 
            // idGroupBox
            // 
            this.idGroupBox.Location = new System.Drawing.Point(20, 34);
            this.idGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.idGroupBox.Name = "idGroupBox";
            this.idGroupBox.Size = new System.Drawing.Size(148, 26);
            this.idGroupBox.TabIndex = 3;
            this.idGroupBox.TextChanged += new System.EventHandler(this.idGroupBox_TextChanged);
            // 
            // ShowMembers
            // 
            this.ShowMembers.Location = new System.Drawing.Point(352, 445);
            this.ShowMembers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ShowMembers.Name = "ShowMembers";
            this.ShowMembers.Size = new System.Drawing.Size(194, 35);
            this.ShowMembers.TabIndex = 4;
            this.ShowMembers.Text = "Показать участников";
            this.ShowMembers.UseVisualStyleBackColor = true;
            this.ShowMembers.Click += new System.EventHandler(this.ShowMembers_Click);
            // 
            // MongoButton
            // 
            this.MongoButton.Location = new System.Drawing.Point(597, 448);
            this.MongoButton.Name = "MongoButton";
            this.MongoButton.Size = new System.Drawing.Size(189, 29);
            this.MongoButton.TabIndex = 5;
            this.MongoButton.Text = "Mongo";
            this.MongoButton.UseVisualStyleBackColor = true;
            this.MongoButton.Click += new System.EventHandler(this.MongoButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 514);
            this.Controls.Add(this.MongoButton);
            this.Controls.Add(this.ShowMembers);
            this.Controls.Add(this.idGroupBox);
            this.Controls.Add(this.gettingMembers);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Auth);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Auth;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button gettingMembers;
        private System.Windows.Forms.TextBox idGroupBox;
        private System.Windows.Forms.Button ShowMembers;
        private System.Windows.Forms.Button MongoButton;
    }
}

