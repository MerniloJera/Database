using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HealthCare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (TxtUsername.Text == "Username" || TxtPassword.Text == "Password")
            {
                MessageBox.Show("Please enter your username and password to log in", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                string username = TxtUsername.Text;
                string password = TxtPassword.Text;

                string hashedPassword = HashPassword(password);

                string query = "SELECT * FROM login WHERE username = '" + username + "'";

                MySqlConnection connect = new MySqlConnection(ConnectionString.Connection);
                MySqlCommand command = new MySqlCommand(query, connect);
                command.CommandTimeout = 60;

                try
                {
                    connect.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string pass = reader.GetString(1);

                            if (pass == hashedPassword && username == reader.GetString(0))
                            {
                                DashBoardcs ds = new DashBoardcs();
                                this.Hide();
                                ds.Show();

                            }
                            else
                            {
                                MessageBox.Show("Account does not exist. Check the username and password and try again.", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account does not exist. Check the username and password and try again.", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception x)
                {
                    MessageBox.Show("Query error: " + x.Message);
                }
            }
        }
    }
}
