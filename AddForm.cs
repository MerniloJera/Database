using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCare
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
            dtBday.MaxDate = DateTime.Now;

        }
        bool number = false;


        public void testingP()
        {
            int desiredLength = 11;

            if (txtPhone.Text.Length != desiredLength)
            {
                number = true;
            }
            else
            {
                number = false;
            }
        }

        private void aDDPATIENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientInfo show = new PatientInfo();
            this.Hide();
            show.Show();
        }

        private void cHECKUPRECORDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUp show = new CheckUp();
            this.Hide();
            show.Show();
        }

        int id = 1;

        private void AddForm_Load(object sender, EventArgs e)
        {
            string query = "SELECT count(*) from patientrecords";
            MySqlConnection connect = new MySqlConnection(ConnectionString.Connection);
            MySqlCommand command = new MySqlCommand(query, connect);

            connect.Open();
            long rowCount = (long)command.ExecuteScalar();
            connect.Close();
            if (rowCount == 0)
            {
                TbxPatientId.Text = id.ToString();
            }
            else
            {
                string query2 = "SELECT patientID from patientrecords order by patientID desc limit 1;";
                MySqlConnection connect2 = new MySqlConnection(ConnectionString.Connection);
                MySqlCommand command2 = new MySqlCommand(query2, connect2);
                connect2.Open();
                id = (int)command2.ExecuteScalar();
                id += 1;
                TbxPatientId.Text = id.ToString();
                connect2.Close();
            }
        }

        bool checker = false;

        private void btnCreate_Click(object sender, EventArgs e)
        {

            testingP();

            if (number == true)
            {

                number = false;
                MessageBox.Show("Invalid Phone Number", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Clear();
            }
            else
            {
                DateTime dateTime = DateTime.Now;
                int year = dateTime.Year;
                int month = dateTime.Month;
                int day = dateTime.Day;
                string sex = "";

                if (rbMale.Checked)
                {
                    sex = "Male";
                }
                else
                {
                    sex = "Female";
                }


                string query1 = "SELECT * FROM patientrecords WHERE firstname = @FirstName AND middlename = @MiddleName AND lastname = @LastName AND birthdate = @BirthDate AND sex = @Sex";

                MySqlConnection connect1 = new MySqlConnection(ConnectionString.Connection);
                MySqlCommand command1 = new MySqlCommand(query1, connect1);
                command1.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                command1.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
                command1.Parameters.AddWithValue("@LastName", txtLastName.Text);
                command1.Parameters.AddWithValue("@BirthDate", year + "-" + month + "-" + day);
                command1.Parameters.AddWithValue("@Sex", cbStatus.Text);

                try
                {
                    connect1.Open();
                    MySqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.HasRows)
                    {
                        MessageBox.Show("This Person Already Exists");
                        checker = true;
                    }
                    // If the person does not exist, proceed with saving

                    connect1.Close(); // Close the first connection before using the second one


                    if (checker == false)
                    {
                        string query = "INSERT INTO patientrecords(patientID,lastname,firstname,middlename,birthdate,birthplace,sex,civilStatus,address,phonenum,statusA) VALUES (@PatientID, @LastName, @FirstName, @MiddleName, @BirthDate, @BirthPlace, @Sex, @CivilStatus, @Address, @Phone, '1')";

                        MySqlConnection connect = new MySqlConnection(ConnectionString.Connection);
                        MySqlCommand command = new MySqlCommand(query, connect);
                        command.Parameters.AddWithValue("@PatientID", TbxPatientId.Text);
                        command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        command.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
                        command.Parameters.AddWithValue("@BirthDate", year + "-" + month + "-" + day);
                        command.Parameters.AddWithValue("@BirthPlace", txtBirthPlace.Text);
                        command.Parameters.AddWithValue("@Sex", sex);
                        command.Parameters.AddWithValue("@CivilStatus", cbStatus.Text);
                        command.Parameters.AddWithValue("@Address", txtAddress.Text);
                        command.Parameters.AddWithValue("@Phone", txtPhone.Text);

                        try
                        {
                            connect.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully Saved");

                                // Clear the form or perform any other necessary actions
                                txtLastName.Clear();
                                txtFirstName.Clear();
                                txtMiddleName.Clear();
                                dtBday.ResetText();
                                txtBirthPlace.Clear();
                                rbMale.Checked = false;
                                rbFemale.Checked = false;
                                cbStatus.ResetText();
                                txtAddress.Clear();
                                txtPhone.Clear();
                                id++;
                                TbxPatientId.Text = id.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Error saving the record");
                            }
                        }
                        catch (Exception x)
                        {
                            MessageBox.Show("Query error: " + x.Message);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show("Query error: " + x.Message);
                }
                finally
                {
                    connect1.Close();
                }



            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DashBoardcs ds = new DashBoardcs();
            this.Hide();
            ds.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtBday_ValueChanged(object sender, EventArgs e)
        {
            if (dtBday.Value > DateTime.Now)
            {
                dtBday.Value = DateTime.Now;
            }
        }
    }
}
