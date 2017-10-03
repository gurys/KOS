namespace KosMail
{
    partial class Pop3set
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
            this.Add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Servers = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Show = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(183, 3);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 0;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Location = new System.Drawing.Point(3, 3);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 1;
            this.Delete.Text = "Удалить";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Servers
            // 
            this.Servers.CheckOnClick = true;
            this.Servers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Servers.FormattingEnabled = true;
            this.Servers.Location = new System.Drawing.Point(0, 0);
            this.Servers.Name = "Servers";
            this.Servers.Size = new System.Drawing.Size(435, 292);
            this.Servers.Sorted = true;
            this.Servers.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Show);
            this.panel1.Controls.Add(this.Ok);
            this.panel1.Controls.Add(this.Delete);
            this.panel1.Controls.Add(this.Add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 264);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 28);
            this.panel1.TabIndex = 3;
            // 
            // Show
            // 
            this.Show.Enabled = false;
            this.Show.Location = new System.Drawing.Point(84, 3);
            this.Show.Name = "Show";
            this.Show.Size = new System.Drawing.Size(93, 23);
            this.Show.TabIndex = 3;
            this.Show.Text = "Редактировать";
            this.Show.UseVisualStyleBackColor = true;
            this.Show.Click += new System.EventHandler(this.Show_Click);
            // 
            // Ok
            // 
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(264, 2);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 2;
            this.Ok.Text = "Сохранить";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // Pop3set
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 292);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Servers);
            this.Name = "Pop3set";
            this.Text = "POP3";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.CheckedListBox Servers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Show;
    }
}