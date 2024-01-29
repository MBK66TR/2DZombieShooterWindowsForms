namespace shooter
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TxtAmmo = new System.Windows.Forms.Label();
            this.TxtScore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HealthBar = new System.Windows.Forms.ProgressBar();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.Player = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtAmmo
            // 
            this.TxtAmmo.AutoSize = true;
            this.TxtAmmo.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TxtAmmo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TxtAmmo.Location = new System.Drawing.Point(13, 13);
            this.TxtAmmo.Name = "TxtAmmo";
            this.TxtAmmo.Size = new System.Drawing.Size(97, 28);
            this.TxtAmmo.TabIndex = 0;
            this.TxtAmmo.Text = "Ammo : 0";
            // 
            // TxtScore
            // 
            this.TxtScore.AutoSize = true;
            this.TxtScore.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TxtScore.ForeColor = System.Drawing.Color.Firebrick;
            this.TxtScore.Location = new System.Drawing.Point(366, 13);
            this.TxtScore.Name = "TxtScore";
            this.TxtScore.Size = new System.Drawing.Size(78, 28);
            this.TxtScore.TabIndex = 0;
            this.TxtScore.Text = "Kills : 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(529, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wave :";
            // 
            // HealthBar
            // 
            this.HealthBar.Location = new System.Drawing.Point(416, 512);
            this.HealthBar.Name = "HealthBar";
            this.HealthBar.Size = new System.Drawing.Size(55, 23);
            this.HealthBar.TabIndex = 1;
            this.HealthBar.Value = 100;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.MainTimerEvent);
            // 
            // Player
            // 
            this.Player.Image = global::shooter.Properties.Resources.karakter111right;
            this.Player.ImageLocation = "";
            this.Player.Location = new System.Drawing.Point(416, 541);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(48, 53);
            this.Player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Player.TabIndex = 2;
            this.Player.TabStop = false;
            this.Player.Click += new System.EventHandler(this.Player_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.LimeGreen;
            this.label2.Location = new System.Drawing.Point(670, -4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Exit to press ESC";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(922, 653);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.HealthBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtScore);
            this.Controls.Add(this.TxtAmmo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Student Shooter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TxtAmmo;
        private System.Windows.Forms.Label TxtScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar HealthBar;
        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label label2;
    }
}

