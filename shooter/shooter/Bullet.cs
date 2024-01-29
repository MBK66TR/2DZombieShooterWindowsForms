using System;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO; 

namespace shooter
{
    internal class Bullet
    {
        public string Direction;
        public int BulletLeft;
        public int BulletTop;
        public int clienttop;
        public int clientbottom;
        public int speed = 20;
        string uygulamaDizini = AppDomain.CurrentDomain.BaseDirectory;

        private PictureBox PBullet = new PictureBox();
        private Timer BulletTimer = new Timer();
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;

        public void MakeBullet(Form form)
        {
            PBullet.SizeMode = PictureBoxSizeMode.AutoSize;
            PBullet.Tag = "bullet";
            PBullet.Left = BulletLeft;
            PBullet.Top = BulletTop;

            PBullet.BringToFront();
            form.Controls.Add(PBullet);

            
            if (Direction == "left" || Direction == "right")
            {
                waveOutDevice = new WaveOut();
                audioFileReader = new AudioFileReader(Path.Combine(uygulamaDizini, "Resources", "CalisSes.wav"));
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
            }

            if (Direction == "up" || Direction == "down")
            {
                waveOutDevice = new WaveOut();
                audioFileReader = new AudioFileReader(Path.Combine(uygulamaDizini, "Resources", "SetUpMine.wav"));
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
            }

            BulletTimer.Interval = speed;
            BulletTimer.Tick += new EventHandler(BulletTimerEvent);
            BulletTimer.Start();
        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {
            if (Direction == "left")
            {
                PBullet.Left -= speed;
                PBullet.Image = Properties.Resources.BulletLeft;
            }
            else if (Direction == "right")
            {
                PBullet.Left += speed;
                PBullet.Image = Properties.Resources.BulletRight;
            }
            else if (Direction == "up")
            {
                //PBullet.Top -= speed;
                PBullet.Image = Properties.Resources.Bullet33;
            }
            else if (Direction == "down")
            {
                //PBullet.Top += speed;
                PBullet.Image = Properties.Resources.Bullet33;
            }

            if (PBullet.Left < 10 || PBullet.Left > clientbottom || PBullet.Top < 10 || PBullet.Top > clienttop)
            {
                BulletTimer.Stop();
                BulletTimer.Dispose();
                PBullet.Dispose();
                BulletTimer = null;
                PBullet = null;

                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                audioFileReader.Dispose();
            }
        }
    }
}
