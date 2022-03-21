
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
            this.SuspendLayout();
            // 
            // Auth
            // 
            this.Auth.Location = new System.Drawing.Point(3, 289);
            this.Auth.Name = "Auth";
            this.Auth.Size = new System.Drawing.Size(92, 23);
            this.Auth.TabIndex = 0;
            this.Auth.Text = "авторизация";
            this.Auth.UseVisualStyleBackColor = true;
            this.Auth.Click += new System.EventHandler(this.Auth_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(297, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(245, 251);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // gettingMembers
            // 
            this.gettingMembers.Location = new System.Drawing.Point(101, 289);
            this.gettingMembers.Name = "gettingMembers";
            this.gettingMembers.Size = new System.Drawing.Size(181, 23);
            this.gettingMembers.TabIndex = 2;
            this.gettingMembers.Text = "Показать информацию о группе";
            this.gettingMembers.UseVisualStyleBackColor = true;
            this.gettingMembers.Click += new System.EventHandler(this.gettingMembers_Click);
            // 
            // idGroupBox
            // 
            this.idGroupBox.Location = new System.Drawing.Point(13, 22);
            this.idGroupBox.Name = "idGroupBox";
            this.idGroupBox.Size = new System.Drawing.Size(100, 20);
            this.idGroupBox.TabIndex = 3;
            // 
            // ShowMembers
            // 
            this.ShowMembers.Location = new System.Drawing.Point(288, 289);
            this.ShowMembers.Name = "ShowMembers";
            this.ShowMembers.Size = new System.Drawing.Size(124, 23);
            this.ShowMembers.TabIndex = 4;
            this.ShowMembers.Text = "Показать участников";
            this.ShowMembers.UseVisualStyleBackColor = true;
            this.ShowMembers.Click += new System.EventHandler(this.ShowMembers_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 319);
            this.Controls.Add(this.ShowMembers);
            this.Controls.Add(this.idGroupBox);
            this.Controls.Add(this.gettingMembers);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Auth);
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
    }
}

