using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CowboySet.Properties;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace CowboySet
{
    public partial class endGameForm : Form
    {

        MySqlConnection connection;
        string server;
        string database;
        string user;
        string password;
        string port;
        string connectionString;
        string sslM;
        int score;
        public endGameForm(int totalPoints)
        {
            InitializeComponent();
            label3.Text = "Your score was: " + totalPoints.ToString();
            score = totalPoints;
            this.BackgroundImage = Resources.backabout;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            button1.Visible = true;
            textBox1.Visible = true;
            label1.Visible = true;

            server = "localhost";
            database = "set";
            user = "root";
            password = "root";
            port = "8889";
            sslM = "none";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database
                + ";" + "user=" + user + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            int count = 1;

            string top10 = "SELECT score FROM game WHERE score > @totalPoints LIMIT 10";
            using (MySqlCommand findingTops = new MySqlCommand(top10, connection))
            {
                findingTops.Parameters.AddWithValue("@totalPoints", score);
               
                connection.Open();

                using (MySqlDataReader reader = findingTops.ExecuteReader())
                {
                    while (reader.Read() && count <= 10)
                    {
                        string boardScore = reader["score"].ToString();
                        count++;
                    }
                }

            }
              if (count>10)
                {
                
                label2.Text = "Sorry, but your score didn't make it into the top 10.\nTime to saddle up and give it another shot!";
                label2.Width = 50;
                button1.Visible = false;
                textBox1.Visible = false;
                label1.Visible = false;

            }
        }
    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                errorProvider1.SetError(textBox1, "Name is required");
                return;
            }
            else
            {
                errorProvider1.SetError(textBox1, string.Empty);
                addToLeaderBoard(name);
                goToOpenScreen();
            }
        }

        private void goToOpenScreen()
        {
            this.Close();
        }

        private void addToLeaderBoard(string name)
        {
            string sql = "INSERT INTO game (name, score) VALUES (@name, @totalPoints)";

            using (MySqlCommand insertIntoLeaderBoard = new MySqlCommand(sql, connection))
            {
                insertIntoLeaderBoard.Parameters.AddWithValue("@name", name);
                insertIntoLeaderBoard.Parameters.AddWithValue("@totalPoints", score);
                insertIntoLeaderBoard.ExecuteNonQuery(); // Execute the insert query.
            }



        }

        private void endGameForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}