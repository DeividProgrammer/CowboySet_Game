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
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace CowboySet
{
    public partial class GameForm : Form
    {
        MySqlConnection connection;
        string server;
        string database;
        string user;
        string password;
        string port;
        string connectionString;
        string sslM;
        SoundPlayer backgroundMusic;

        struct Card
        {
            public int color;
            public int shape;
            public int number;
            public string imgname;
        }

        int minute = 2;
        int secondsleft;
        int numsets, numCardSelected;
        SoundPlayer ticktock, buzzer;
        Card[] cards = new Card[27];
        Card[] cardsOnBoard = new Card[10];
        Random random = new Random(); //for shuffeling the deck

        PictureBox[] pictures = new PictureBox[10];
        PictureBox[] checkboxes = new PictureBox[10];


        List<Card> cardsSelected = new List<Card>(3); // Initialize a list with a capacity of 3


        int[] selectCardIndices = new int[27]; //carts shown in the screen
        int totalPoints = 0;
        

        public GameForm(bool isMutedMusic)
        {
            InitializeComponent();

            this.BackgroundImage = Properties.Resources.gameBackground;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.backgroundMusic = new SoundPlayer(CowboySet.Properties.Resources.gameMusic);

            if(!isMutedMusic) this.backgroundMusic.PlayLooping();

            for (int i = 0; i < 27; i++) selectCardIndices[i] = i;
            createCards();
            createPictures();
            newGame();
            label1.Text = totalPoints.ToString();

            server = "localhost";
            database = "set";
            user = "root";
            password = "root";
            port = "8889";
            sslM = "none";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database
                + ";" + "user=" + user + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                connection.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + connectionString);
            }

        }

        private void createCards()
        {
            int cardnum = 0;
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    for (int k = 1; k < 4; k++)
                    {
                        cards[cardnum].number = i;
                        cards[cardnum].shape = j;
                        cards[cardnum].color = k;
                        cards[cardnum].imgname = "_" + i + j + k + ".jpg";
                        cardnum++;

                    }
                }
            }
        }

        private void createPictures()
        {
            for (int i = 0; i < 10; i++)
            {
                pictures[i] = new PictureBox();
                pictures[i].Width = 150;
                pictures[i].Height = 150;
                pictures[i].Image = CowboySet.Properties.Resources._311; // temporary
                pictures[i].BackColor = Color.Transparent;
                pictures[i].SendToBack();
                checkboxes[i] = new PictureBox();
                checkboxes[i].Width = 50;
                checkboxes[i].Height = 50;
                checkboxes[i].Image = CowboySet.Properties.Resources.check2;
                checkboxes[i].BackColor = Color.Transparent;
                checkboxes[i].BringToFront();
                checkboxes[i].Visible = false;
                pictures[i].Visible = false;
                label1.BackColor = System.Drawing.Color.Transparent;


                this.Controls.Add(pictures[i]);
                this.Controls.Add(checkboxes[i]);

            }


            pictures[0].Location = new Point(420, 80);
            pictures[1].Location = new Point(725, 80);
            pictures[2].Location = new Point(1030, 80);
            pictures[3].Location = new Point(572, 160);
            pictures[4].Location = new Point(877, 160);
            pictures[5].Location = new Point(420, 300);
            pictures[6].Location = new Point(725, 300);
            pictures[7].Location = new Point(1030, 300);
            pictures[8].Location = new Point(572, 380);
            pictures[9].Location = new Point(877, 380);

            checkboxes[0].Location = new Point(420, 80);
            checkboxes[1].Location = new Point(725, 80);
            checkboxes[2].Location = new Point(1030, 80);
            checkboxes[2].Location = new Point(1030, 80);
            checkboxes[2].Location = new Point(1030, 80);
            checkboxes[3].Location = new Point(572, 160);
            checkboxes[4].Location = new Point(877, 160);
            checkboxes[5].Location = new Point(420, 300);
            checkboxes[6].Location = new Point(725, 300);
            checkboxes[7].Location = new Point(1030, 300);
            checkboxes[8].Location = new Point(572, 380);
            checkboxes[9].Location = new Point(877, 380);


            pictures[0].Click += picture_Click0;
            pictures[1].Click += picture_Click1;
            pictures[2].Click += picture_Click2;
            pictures[3].Click += picture_Click3;
            pictures[4].Click += picture_Click4;
            pictures[5].Click += picture_Click5;
            pictures[6].Click += picture_Click6;
            pictures[7].Click += picture_Click7;
            pictures[8].Click += picture_Click8;
            pictures[9].Click += picture_Click9;



        }

        private void manageClick(int i)
        {
            checkboxes[i].BringToFront();
            if (checkboxes[i].Visible)
            {
                checkboxes[i].Visible = false;
                numCardSelected--;
                cardsSelected.Remove(cards[selectCardIndices[i]]);
            }
            else
            {
                checkboxes[i].Visible = true;
                numCardSelected++;
                cardsSelected.Add(cards[selectCardIndices[i]]);
            }

            if (numCardSelected == 3)
            {
                bool sameColor = (cardsSelected[0].color == cardsSelected[1].color && cardsSelected[1].color == cardsSelected[2].color);
                bool differentColor = (cardsSelected[0].color != cardsSelected[1].color && cardsSelected[1].color != cardsSelected[2].color && cardsSelected[0].color != cardsSelected[2].color);
                bool sameShape = (cardsSelected[0].shape == cardsSelected[1].shape && cardsSelected[1].shape == cardsSelected[2].shape);
                bool differentShape = (cardsSelected[0].shape != cardsSelected[1].shape && cardsSelected[1].shape != cardsSelected[2].shape && cardsSelected[0].shape != cardsSelected[2].shape);
                bool sameNumber = (cardsSelected[0].number == cardsSelected[1].number && cardsSelected[1].number == cardsSelected[2].number);
                bool differentNumber = (cardsSelected[0].number != cardsSelected[1].number && cardsSelected[1].number != cardsSelected[2].number && cardsSelected[0].number != cardsSelected[2].number);

                bool isSet = (sameColor || differentColor) && (sameShape || differentShape) && (sameNumber || differentNumber);

                if (isSet)
                {
                    totalPoints++;
                    label1.Text = totalPoints.ToString();
                    cardsSelected.Clear();
                    pictureBox1.BackgroundImage = Resources.cowbowSetG;
                    deselectCheckBoxes();
                    shuffleAndDeal();
                    putOriginalImageAfter3Secs();

                }
                else
                {
                    deselectCheckBoxes();
                    pictureBox1.BackgroundImage = Resources.wrongSetCowboyG;
                    numCardSelected = 0;
                    putOriginalImageAfter3Secs();
                }
            }
        }
        private async void putOriginalImageAfter3Secs()
        {
            await Task.Delay(3000);
            pictureBox1.BackgroundImage = Resources.cowboyG;
        }

        private void deselectCheckBoxes()
        {
            for (int i = 0; i < checkboxes.Length; i++)
            {
                checkboxes[i].Visible = false;
            }
            numCardSelected = 0;
            cardsSelected.Clear();
        }




        private void picture_Click0(object sender, EventArgs e)
        {
            manageClick(0);
        }
        private void picture_Click1(object sender, EventArgs e)
        {
            manageClick(1);
        }
        private void picture_Click2(object sender, EventArgs e)
        {
            manageClick(2);
        }
        private void picture_Click3(object sender, EventArgs e)
        {
            manageClick(3);
        }
        private void picture_Click4(object sender, EventArgs e)
        {
            manageClick(4);
        }
        private void picture_Click5(object sender, EventArgs e)
        {
            manageClick(5);
        }
        private void picture_Click6(object sender, EventArgs e)
        {
            manageClick(6);
        }
        private void picture_Click7(object sender, EventArgs e)
        {
            manageClick(7);
        }
        private void picture_Click8(object sender, EventArgs e)
        {
            manageClick(8);
        }
        private void picture_Click9(object sender, EventArgs e)
        {
            manageClick(9);
        }

        private Bitmap GetImageResource(string filename)
        {
            string resourcename = filename.Substring(0, filename.IndexOf("."));
            return (Bitmap)Properties.Resources.ResourceManager.GetObject(resourcename);
        }

        public void newGame()
        {
            numsets = numCardSelected = 0;
            for (int i = 0; i < 10; i++)
            {
                pictures[i].Visible = true;
                checkboxes[i].Visible = false;

            }
            shuffleAndDeal();
            resetTimerLabel();

        }

        private void resetTimerLabel()
        {
            secondsleft = minute * 60;
            string labelTxt = minute.ToString();
            if (labelTxt.Length < 2) labelTxt = "0" + labelTxt;
            timerLabel.Text = labelTxt + ":00";
            timerLabel.BackColor = System.Drawing.Color.Transparent;
            timerLabel.Visible = true;
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void shuffleAndDeal()
        {
            for (int i = 0; i < 27; i++)
            {
                int r = random.Next(27);
                int temp = selectCardIndices[i];
                selectCardIndices[i] = selectCardIndices[r];
                selectCardIndices[r] = temp;
            }

            for (int i = 0; i < 10; i++)
            {
                string imagename = cards[selectCardIndices[i]].imgname;
                pictures[i].Image = GetImageResource(imagename);
                pictures[i].Refresh();
            }
        }

 
        private void goToOpenScreen()
        {
            this.Hide();
            Form1 game = new Form1();
            game.Closed += (s, args) => this.Close();
            game.Show();
        }

        private void notAbleToClickPictures()
        {
            for (int i = 0; i < pictures.Length; i++)
            {
                pictures[i].Enabled = false;
            }
        }

      
        private void Form2_Load(object sender, EventArgs e)
        {
             
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string m, s;
            if (secondsleft > 0)
            {
                if (secondsleft == 10)
                {
              
                    timerLabel.BackColor = System.Drawing.Color.Transparent;
                }
                secondsleft -= 1;
                m = (secondsleft / 60).ToString();
                if (m.Length < 2) m = "0" + m;
                s = (secondsleft % 60).ToString();
                if (s.Length < 2) s = "0" + s;
                timerLabel.Text = m + ":" + s;
            }
            else
            {
                timer1.Stop();
                timerLabel.Visible = false;
                notAbleToClickPictures();
          
                endGameForm();
            }
        }

        private void endGameForm()
        {
            
            
                endGameForm form = new endGameForm(totalPoints);
                this.Enabled = false;

                form.FormClosed += (sender, e) =>
                {

                    this.Enabled = true;

                    this.Hide();
                    Form1 game = new Form1();
                    game.Closed += (s, args) => this.Close();
                    game.Show();

                };

                form.ShowDialog();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timerLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
