using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CowboySet
{
    public partial class leaderBoard : Form
    {

        string server;
        string database;
        string user;
        string password;
        string port;
        string connectionString;
        string sslM;
        public leaderBoard()
        {
            InitializeComponent();

            MySqlConnection connection;
            server = "localhost";
            database = "set";
            user = "root";
            password = "root";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database
                + ";" + "user=" + user + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            try
            {
                String person, score;
                listView1.View = View.Details;
                listView1.Columns.Add("Name", -2, HorizontalAlignment.Left);
                listView1.Columns.Add("Score", -2, HorizontalAlignment.Right);
                int count = 1;
                String sql = "SELECT distinct name,score from game order by score desc";
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read() && count <= 10)
                    {
                        person = reader["name"].ToString();
                        score = reader["score"].ToString();
                        ListViewItem item = new ListViewItem(person);
                        item.SubItems.Add(score);
                        listView1.Items.Add(item);
                        count++;
                    }
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + connectionString);
            }
        }

        private void leaderBoard_Load(object sender, EventArgs e)
        {

        }
    }
}
