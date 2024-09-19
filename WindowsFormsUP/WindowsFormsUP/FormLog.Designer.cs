namespace WindowsFormsUP
{
    partial class FormLog
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.textBoxCap = new System.Windows.Forms.TextBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.labelMistake = new System.Windows.Forms.Label();
            this.buttonEYE = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonChange = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLockoutTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Авторизация";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.textBoxLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBoxLogin.Location = new System.Drawing.Point(314, 85);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(258, 57);
            this.textBoxLogin.TabIndex = 2;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.textBoxPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBoxPass.Location = new System.Drawing.Point(314, 148);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(258, 57);
            this.textBoxPass.TabIndex = 3;
            // 
            // textBoxCap
            // 
            this.textBoxCap.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.textBoxCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBoxCap.Location = new System.Drawing.Point(314, 360);
            this.textBoxCap.Name = "textBoxCap";
            this.textBoxCap.Size = new System.Drawing.Size(258, 57);
            this.textBoxCap.TabIndex = 6;
            this.textBoxCap.Visible = false;
            // 
            // buttonEnter
            // 
            this.buttonEnter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonEnter.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.buttonEnter.ForeColor = System.Drawing.Color.Blue;
            this.buttonEnter.Location = new System.Drawing.Point(314, 473);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(258, 76);
            this.buttonEnter.TabIndex = 5;
            this.buttonEnter.Text = "Войти";
            this.buttonEnter.UseVisualStyleBackColor = false;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // labelMistake
            // 
            this.labelMistake.AutoSize = true;
            this.labelMistake.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.labelMistake.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelMistake.Location = new System.Drawing.Point(12, 418);
            this.labelMistake.Name = "labelMistake";
            this.labelMistake.Size = new System.Drawing.Size(0, 50);
            this.labelMistake.TabIndex = 5;
            this.labelMistake.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonEYE
            // 
            this.buttonEYE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonEYE.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.buttonEYE.ForeColor = System.Drawing.Color.Blue;
            this.buttonEYE.Location = new System.Drawing.Point(578, 148);
            this.buttonEYE.Name = "buttonEYE";
            this.buttonEYE.Size = new System.Drawing.Size(54, 57);
            this.buttonEYE.TabIndex = 4;
            this.buttonEYE.Text = "🙈";
            this.buttonEYE.UseVisualStyleBackColor = false;
            this.buttonEYE.Click += new System.EventHandler(this.buttonEYE_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(314, 238);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(258, 116);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // buttonChange
            // 
            this.buttonChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonChange.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.buttonChange.ForeColor = System.Drawing.Color.Blue;
            this.buttonChange.Location = new System.Drawing.Point(578, 297);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(54, 57);
            this.buttonChange.TabIndex = 7;
            this.buttonChange.Text = "🔄";
            this.buttonChange.UseVisualStyleBackColor = false;
            this.buttonChange.Visible = false;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkOrange;
            this.label2.Location = new System.Drawing.Point(163, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 50);
            this.label2.TabIndex = 9;
            this.label2.Text = "Логин";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkOrange;
            this.label3.Location = new System.Drawing.Point(130, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 50);
            this.label3.TabIndex = 10;
            this.label3.Text = "Пароль";
            // 
            // labelLockoutTime
            // 
            this.labelLockoutTime.AutoSize = true;
            this.labelLockoutTime.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.labelLockoutTime.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLockoutTime.Location = new System.Drawing.Point(578, 486);
            this.labelLockoutTime.Name = "labelLockoutTime";
            this.labelLockoutTime.Size = new System.Drawing.Size(0, 50);
            this.labelLockoutTime.TabIndex = 12;
            this.labelLockoutTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.labelLockoutTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonEYE);
            this.Controls.Add(this.labelMistake);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.textBoxCap);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "FormLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.TextBox textBoxCap;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Label labelMistake;
        private System.Windows.Forms.Button buttonEYE;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLockoutTime;
    }
}

