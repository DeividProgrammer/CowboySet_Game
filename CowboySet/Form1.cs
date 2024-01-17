using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CowboySet.Properties;

namespace CowboySet
{
    public partial class Form1 : Form
    {

        SoundPlayer backgroundMusic;
        SoundPlayer clickSound;
        Boolean isMusicMuted = false;
        public Form1()
        {

            PictureBox pictureBox1 = new PictureBox();
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.BGFinal;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            clickSound = new SoundPlayer(CowboySet.Properties.Resources.button);
            backgroundMusic = new SoundPlayer(CowboySet.Properties.Resources.menuMusic_1);
            backgroundMusic.PlayLooping();

            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button2.BackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button3.BackColor = Color.Transparent;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button4.BackColor = Color.Transparent;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button5.BackColor = Color.Transparent;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button5.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button1.MouseEnter += Button_MouseEnter;
            button1.MouseLeave += Button_MouseLeave;

            button2.MouseEnter += Button_MouseEnter;
            button2.MouseLeave += Button_MouseLeave;

            button3.MouseEnter += Button_MouseEnter;
            button3.MouseLeave += Button_MouseLeave;

            button4.MouseEnter += Button_MouseEnter;
            button4.MouseLeave += Button_MouseLeave;

            button5.MouseEnter += Button_MouseEnter;
            button5.MouseLeave += Button_MouseLeave;

            button6.BackgroundImage = isMusicMuted ? Resources.muteButton : Resources.unmuteButton;
            button6.BackgroundImageLayout = ImageLayout.Stretch;

            button6.FlatAppearance.BorderSize = 0;
            button6.BackColor = Color.Transparent;
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button5.FlatAppearance.MouseDownBackColor = Color.Transparent;

        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar la imagen del botón cuando el cursor entra en el botón
            Button button = (Button)sender;
            if (button == button1)
                button.BackgroundImage = Properties.Resources.playButtonHover;
            else if (button == button2)
                button.BackgroundImage = Properties.Resources.quitButtonHover;
            else if (button == button3)
                button.BackgroundImage = Properties.Resources.leaderboardButtonHover;
            else if (button == button4)
                button.BackgroundImage = Properties.Resources.HowToPlayButtonHover;
            else if (button == button5)
                button.BackgroundImage = Properties.Resources.aboutButtonHover;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            // Cambiar la imagen del botón de nuevo cuando el cursor sale del botón
            Button button = (Button)sender;
            if (button == button1)
                button.BackgroundImage = Properties.Resources.playButtonBlack;
            else if (button == button2)
                button.BackgroundImage = Properties.Resources.quitButton;
            else if (button == button3)
                button.BackgroundImage = Properties.Resources.leaderboardButton;
            else if (button == button4)
                button.BackgroundImage = Properties.Resources.HowToPlayButton;
            else if (button == button5)
                button.BackgroundImage = Properties.Resources.aboutButton;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            backgroundMusic.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameForm game = new GameForm(isMusicMuted);
            game.Closed += (s, args) => this.Close();
            game.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.backgroundMusic.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            leaderBoard form = new leaderBoard();
            form.Refresh();
            form.ShowDialog();
            form.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HowToPlay form = new HowToPlay();
            form.Refresh();
            form.ShowDialog();
            form.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Refresh();
            form.ShowDialog();
            form.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(isMusicMuted)
            {
                isMusicMuted = false;
                backgroundMusic.Play();
                button6.BackgroundImage = Properties.Resources.unmuteButton;
            } else
            {
                isMusicMuted= true;
                backgroundMusic.Stop();
                button6.BackgroundImage = Properties.Resources.muteButton;
            }
        }
    }
    
}
