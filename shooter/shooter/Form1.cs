using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace shooter
{
    public partial class Form1 : Form
    {
        
        string uygulamaDizini = AppDomain.CurrentDomain.BaseDirectory;

        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private IWavePlayer outdevice;
        private AudioFileReader AmmoPickUp;

        bool AimUp,AimDown,GoLeft, GoRight, GoTop, GoBottom, HKitexist, GameOver, isPaused;
        string facing = "up";
        int PlayerHealth = 100;
        
        int speed = 5;
        int Ammo = 10;
        int ZSpeed = 2;
        int Score = 0;
        int zombielevel = 3;
        int Wave = 1;
        private SoundPlayer backgroundMusic;
        int musicCounter=0;
      

        Random RandomNum = new Random();

        
        

        List<PictureBox> zombielist = new List<PictureBox>();

        public Form1()
        {

            backgroundMusic = new SoundPlayer(Path.Combine(uygulamaDizini, "Resources", "8bit-music-for-game-68698.wav"));
           

            //backgroundMusic = new SoundPlayer("C:\\Users\\HP\\source\\repos\\shooter\\shooter\\Resources\\8bit-music-for-game-68698.wav"); // Müzik dosyasının yolunu belirtin

            InitializeComponent();
            RestartGame();


            outdevice = new WaveOut();
            waveOutDevice = new WaveOut(); // Yeni bir ses çıkış cihazı oluştur
            audioFileReader = new AudioFileReader(Path.Combine(uygulamaDizini, "Resources", "HitSound.wav"));
            AmmoPickUp = new AudioFileReader(Path.Combine(uygulamaDizini, "Resources", "AmmoPickUp.wav"));

            /* audioFileReader = new AudioFileReader("C:\\Users\\HP\\source\\repos\\shooter\\shooter\\Resources\\HitSound.wav"); // Ses dosyasının yolunu belirtin
            AmmoPickUp = new AudioFileReader("C:\\Users\\HP\\source\\repos\\shooter\\shooter\\Resources\\AmmoPickUp.wav");
            */
            waveOutDevice.Init(audioFileReader); // Ses dosyasını çıkış cihazına tanıt
            outdevice.Init(AmmoPickUp);


            
            


          
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            if (isPaused)
            {
                return;
            }


            musicCounter++;       
            if(musicCounter == 1400)
            {
                backgroundMusic.Play();
                musicCounter = 0;

            }

             
           

            if (PlayerHealth > 1)
            {
                if(PlayerHealth > 100)
                {
                    PlayerHealth = 100;
                }
                HealthBar.Value = PlayerHealth;
            }
            else
            {
                GameOver = true;
                Player.Image = Properties.Resources.deadd;
                GameTimer.Stop();
            }

            TxtAmmo.Text ="Ammo: "+ Ammo;
            TxtScore.Text = "Score: " + Score;

            if (GoLeft == true && Player.Left > 0)
            {
                Player.Left -= speed;
            }

            if (GoRight == true && Player.Left + Player.Width < this.ClientSize.Width)
            {
                Player.Left += speed;
            }

            if (GoTop == true && Player.Top > 45)
            {
                Player.Top -= speed*3/4;
            }

            if(GoBottom == true && Player.Top +Player.Height < this.ClientSize.Height)
            {
                Player.Top += speed*3/4; 
            }
            if (PlayerHealth < 0)
            {
                PlayerHealth = 0;
            }

            HealthBar.Value = PlayerHealth;
            HealthBar.Left = Player.Left;
            HealthBar.Top = Player.Top - HealthBar.Height - 5;
            //Weapon.Left = Player.Left-Weapon.Width;
            //Weapon.Top = Player.Top- Weapon.Height;
            



            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo") 
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        PlayAmmoPickUp();
                        if (Score < 5)
                        {
                            Ammo += 5;
                        }
                        if(Score >= 5)
                        {
                            Ammo += 10;
                        }
                        if(Score >= 50)
                        {
                            Ammo += 10;
                        }
                        if(Score >= 100)
                        {
                            Ammo += 10;
                        }
                        if (Score >= 200)
                        {
                            Ammo += 10;
                        }
                    }
                }

                

                if (x is PictureBox && (string)x.Tag=="zombie")
                {

                    if(Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        PlayerHealth -= 1;
                        PlayHealthDecreasedSound();
                    }


                    if (x.Left > Player.Left)
                    {
                        x.Left -= ZSpeed;
                        ((PictureBox)x).Image = Properties.Resources.Azombieleft;
                    }
                    if (x.Left < Player.Left)
                    {
                        x.Left += ZSpeed;
                        ((PictureBox)x).Image = Properties.Resources.Azombieright;
                    }
                    if (x.Top > Player.Top)
                    {
                        x.Top -= ZSpeed/2;
                        //(PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (x.Top < Player.Top)
                    {
                        x.Top += ZSpeed/2;
                        //((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }


                foreach(Control j in this.Controls)
                {
                    if(j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag =="zombie")
                    {
                        if(x.Bounds.IntersectsWith(j.Bounds))
                        {
                            Score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombielist.Remove(((PictureBox)x));
                            if (zombielist.Count == 0)
                            {
                                zombielevel++;
                                speed += 1;
                                Wave++;
                                label1.Text="Wave:"+ Wave.ToString();
                                
                                if (zombielevel % 2 == 0)
                                {
                                    ZSpeed += 1;
                                }
                                for (int i = 0;zombielevel>i;i++)
                                {

                                    
                                    CreateZombies();
                                    
                                    
                                }
                            }
                        }
                    }
                }
                

            }

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && (string)y.Tag == "health")
                {
                    if (Player.Bounds.IntersectsWith(y.Bounds))
                    {
                        this.Controls.Remove(y);
                        ((PictureBox)y).Dispose();

                        if(Score < 5)
                        { 
                        PlayerHealth += 35;
                        }
                        if(Score >= 5)
                        {
                            PlayerHealth += 60;
                        }

                        HKitexist = false;
 
                        }
                }
            }





        }

      

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (GameOver == true)
            {
                return;
            }


            if (e.KeyCode == Keys.P)
            {
                isPaused = !isPaused;

                if (isPaused)
                {

                    backgroundMusic.Stop();
                    GameTimer.Stop(); 
                }
                else
                {
                    
                    backgroundMusic.Play(); 
                    GameTimer.Start(); 
                }
            }


            if (e.KeyCode == Keys.A)
            {
                GoLeft = true;
                facing = "left";
                Player.Image = Properties.Resources.karakter111left;
            }

            if (e.KeyCode == Keys.D)
            {
                GoRight = true;
                facing = "right";
                Player.Image = Properties.Resources.karakter111right;
            }

            if (e.KeyCode == Keys.W)
            {
                GoTop = true;
                facing = "up";
                Player.Image = Properties.Resources.karakter111;
            }

            if (e.KeyCode == Keys.S)
            {
                GoBottom = true;
                facing = "down";
                Player.Image = Properties.Resources.karakter111;
            }
            if(e.KeyCode == Keys.Up)
            {
                AimUp = true;
                //silah aim yukarı yazılacak 
            }
            if(e.KeyCode == Keys.Down)
            {
                AimDown = true;
            }

        }

        private void Player_Click(object sender, EventArgs e)
        {

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            


            if(e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.A)
            {
                GoLeft = false;
            }

            if (e.KeyCode == Keys.D)
            {
                GoRight = false;
            }

            if (e.KeyCode == Keys.W)
            {
                GoTop = false;
            }

            if (e.KeyCode == Keys.S)
            {
                GoBottom = false;
            }
            if (e.KeyCode == Keys.Up) 
            {
                AimUp = false; 
            }
            if( e.KeyCode == Keys.Down)
            {
                AimDown=false;
            }

            if(e.KeyCode == Keys.Space && Ammo > 0 && GameOver == false)
            {
             
                --Ammo;
                
                ShootBullet(facing);


                if (Ammo <1)
                {
                    DropAmmo();
                }

            }

            if (!HKitexist)
            {

            
                if (PlayerHealth < 30)
                {
                    HKitexist=true;
                    DropKit();
                }
            }

            if(e.KeyCode== Keys.Enter  && GameOver == true)
            {
                RestartGame();
            }


        }

        private void PlayAmmoPickUp()
        {
            outdevice.Stop(); // Önceki sesi durdur
            AmmoPickUp.Position = 0; // Ses dosyasını başa sıfırla
            outdevice.Play(); // Ses dosyasını çal
        }


        private void PlayHealthDecreasedSound()
        {
            waveOutDevice.Stop(); // Önceki sesi durdur
            audioFileReader.Position = 0; // Ses dosyasını başa sıfırla
            waveOutDevice.Play(); // Ses dosyasını çal
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void ShootBullet(string direction)
        {
            if(facing == "up" || facing == "down" )
            { 
            for (int i = 0; i < 2; i++)
            {
                Bullet ShootBullet = new Bullet();
                ShootBullet.clienttop = this.ClientSize.Height;
                ShootBullet.clientbottom = this.ClientSize.Width;
                ShootBullet.Direction = direction;
                ShootBullet.BulletLeft = Player.Left + (Player.Width / 2);
                ShootBullet.BulletTop = Player.Top + (Player.Height / 2);
                ShootBullet.MakeBullet(this);
                if (Score > 50)
                {
                    ShootBullet.speed = 30;
                }
                if (Score > 110)
                {
                    ShootBullet.speed = 40;
                }
                
            }
            }
            if(facing =="left" ||  facing == "right")
            {
                Bullet ShootBullet = new Bullet();
                ShootBullet.clienttop = this.ClientSize.Height;
                ShootBullet.clientbottom = this.ClientSize.Width;
                ShootBullet.Direction = direction;
                ShootBullet.BulletLeft = Player.Left + (Player.Width / 2);
                ShootBullet.BulletTop = Player.Top + (Player.Height / 2);
                ShootBullet.MakeBullet(this);
                if (Score > 50)
                {
                    ShootBullet.speed = 30;
                }
                if (Score > 110)
                {
                    ShootBullet.speed = 40;
                }
            }

        }


        //buradan sonraki 2 blok düşünülüp kaldırılabilir resim dönderip geri çizme üzerine kurulu
        private void RotateImage(PictureBox pictureBox, float angle)
        {
            Image originalImage = pictureBox.Image;

            if (originalImage != null)
            {
                Bitmap rotatedImage = RotateImage(originalImage, angle);
                pictureBox.Image = rotatedImage;
            }
        }

        private Bitmap RotateImage(Image image, float angle)
        {
            Bitmap rotatedImage = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(image.Width / 2, image.Height / 2); // Dönme merkezini belirler
                g.RotateTransform(angle); // Resmi döndürür
                g.TranslateTransform(-image.Width / 2, -image.Height / 2); // Dönme merkezini geri ayarlar
                g.DrawImage(image, new Point(0, 0)); // Döndürülmüş resmi çizer
            }

            return rotatedImage;
        }

        // Kullanım örneği:
        // RotateImage(Crossair, 45); // 45 dereceye kadar döndürür



        private void CreateZombies()
        {

   

            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.Azombieleft;
            zombie.Top = RandomNum.Next(0, 800);
            zombie.Left = RandomNum.Next(0, 900);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombielist.Add(zombie);
            this.Controls.Add(zombie);
            Player.BringToFront();

        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            //if ile daha fazla mermi verecek kutunun resmi yapılacak
            ammo.Image = Properties.Resources.AmmoBox111;
            ammo.Left = RandomNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = RandomNum.Next(50, this.ClientSize.Height - ammo.Height);
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
            Player.BringToFront();
           
        }
        private void DropKit()
        {
            PictureBox healt = new PictureBox();
            //if ile daha fazla can verecek kutunun resmi ayarlanacak
            healt.Image = Properties.Resources.HealthBox111;
            healt.Left = RandomNum.Next(10,this.ClientSize.Width - healt.Width);
            healt.Top = RandomNum.Next(10, this.ClientSize.Height - healt.Height);
            healt.SizeMode = PictureBoxSizeMode.AutoSize;
            healt.Tag = "health";
            this.Controls.Add(healt);
            healt.BringToFront();
            Player.BringToFront();
        }
        private void RestartGame()
        {

            foreach (PictureBox i in zombielist)
            {
                this.Controls.Remove(i);
            }
            zombielist.Clear();

            for (int i = 0; i<3;i++)
            {
                CreateZombies();
            }

            GoBottom = false; 
            GoTop=false; 
            GoLeft = false;
            GoRight = false;

            PlayerHealth = 100;
            Score = 0;
            Ammo = 10;
            GameOver = false;
            musicCounter = 0;
                backgroundMusic.Play();
            
            
             
             speed = 5;            
             ZSpeed = 2;
             Score = 0;                  
             zombielevel = 3;
             Wave = 1;



            GameTimer.Start();
        }
    }
}
