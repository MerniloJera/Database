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
    public partial class PatientInfo : Form
    {
        public PatientInfo()
        {
            InitializeComponent();
        }


        public void load()
        {
            dgvData.Rows.Clear();

            dgvData.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dgvData.Font, System.Drawing.FontStyle.Bold);
            string[] columnNames = new string[] { "ID", "Lastname.", "Firstname", "Middlename", "Birthdate", "Birthplace", "Sex", "Civil Status", "Address", "Phone Number" };

            dgvData.ColumnCount = 10;

            for (int a = 0; a < columnNames.Length; a++)
            {
                dgvData.Columns[a].Name = columnNames[a];
            }

            string query = "SELECT * FROM patientrecords WHERE statusA = '1'";
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
                        // Assuming the birthdate is stored in the 7th column (index 6)
                        DateTime birthdate = reader.GetDateTime(4);

                        // Extract year, month, and day
                        int year = birthdate.Year;
                        int month = birthdate.Month;
                        int day = birthdate.Day;

                        dgvData.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), month + "/" + day + "/" + year, reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                    }
                }

            }
            catch (Exception x)
            {
                MessageBox.Show("Query error: " + x.Message);
            }
        }

        private void PatientInfo_Load(object sender, EventArgs e)
        {
            load();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedrow;
                selectedrow = e.RowIndex;
                DataGridViewRow row = dgvData.Rows[selectedrow];

                ConnectionString.IdContent = row.Cells[0].Value.ToString();


                string query = "SELECT * FROM patientrecords WHERE patientID = '" + ConnectionString.IdContent + "'";

                using (MySqlConnection connect = new MySqlConnection(ConnectionString.Connection))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connect))
                    {
                        command.CommandTimeout = 60;

                        try
                        {
                            connect.Open();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {

                                        txtPatientID.Text = reader.GetString(0);
                                        txtLastName.Text = reader.GetString(1);
                                        txtFirstName.Text = reader.GetString(2);
                                        txtMiddleName.Text = reader.GetString(3);
                                        dtBday.Text = reader.GetString(4);
                                        txtBirthPlace.Text = reader.GetString(5);
                                        string sex = reader.GetString(6);

                                        if(sex == "Male")
                                        {
                                            rbMale.Checked = true;
                                        }
                                        else
                                        {
                                            rbFemale.Checked = true;

                                        }

                                        cbStatus.Text = reader.GetString(7);
                                        txtAddress.Text = reader.GetString(8);
                                        txtPhone.Text = reader.GetString(9);


                                    }
                                }
                            }
                        }
                        catch (Exception x)
                        {
                            MessageBox.Show("Query error: " + x.Message);
                        }
                    }
                }


            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }

        private void aDDPATIENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddForm show = new AddForm();
            this.Hide();
            show.Show();
        }

        private void cHECKUPRECORDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUp show = new CheckUp();
            this.Hide();
            show.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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


            string query = "UPDATE patientrecords SET lastname = '" + txtLastName.Text + "', firstname = '" + txtFirstName.Text + "', middlename = '" + txtMiddleName.Text + "', birthdate ='" + year + "-" + month + "-" + day + "', birthplace = '" + txtBirthPlace.Text + "', sex = '" + sex + "', civilStatus = '" + cbStatus.Text + "', address = '" + txtAddress.Text + "', phonenum = " + txtPhone.Text + " WHERE patientID = '" + txtPatientID.Text + "'";
            
            
            MySqlConnection connect = new MySqlConnection(ConnectionString.Connection);
            MySqlCommand command = new MySqlCommand(query, connect);
            command.CommandTimeout = 60;


            try
            {
                connect.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                }
                else
                {
                    MessageBox.Show("Successfully Updated");

                    txtLastName.Clear();
                    txtFirstName.Clear();
                    txtMiddleName.Clear();
                    dtBday.ResetText();
                    txtBirthPlace.Clear();

                    if (sex == "Male")
                    {
                        rbMale.Checked = false;
                    }
                    else
                    {
                        rbFemale.Checked = false;
                    }
                    cbStatus.ResetText();
                    txtAddress.Clear();
                    txtPhone.Clear();

                    load();

                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Query error: " + x.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sex = "";

            if (rbMale.Checked)
            {
                sex = "Male";
            }
            else
            {
                sex = "Female";
            }

            string query = "UPDATE patientrecords SET statusA = '2' WHERE patientID = '" + ConnectionString.IdContent + "'";
            MySqlConnection connect = new MySqlConnection(ConnectionString.Connection);
            MySqlCommand command = new MySqlCommand(query, connect);
            command.CommandTimeout = 60;


            try
            {
                connect.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                }
                else
                {
                    MessageBox.Show("Successfully Deleted");

                    txtLastName.Clear();
                    txtFirstName.Clear();
                    txtMiddleName.Clear();
                    dtBday.ResetText();
                    txtBirthPlace.Clear();

                    if (sex == "Male")
                    {
                        rbMale.Checked = false;
                    }
                    else
                    {
                        rbFemale.Checked = false;
                    }
                    cbStatus.ResetText();
                    txtAddress.Clear();
                    txtPhone.Clear();
                    txtPatientID.Text = "id";

                    load();

                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Query error: " + x.Message);
            }
            load();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            dgvData.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dgvData.Font, System.Drawing.FontStyle.Bold);
            string[] columnNames = new string[] { "ID", "Lastname.", "Firstname", "Middlename", "Birthdate", "Birthplace", "Sex", "Civil Status", "Address", "Phone Number" };

            dgvData.ColumnCount = 10;

            for (int a = 0; a < columnNames.Length; a++)
            {
                dgvData.Columns[a].Name = columnNames[a];
            }
            string query = "";

            if(int.TryParse(txtSearch.Text,out _))
            {
                query = "SELECT * FROM patientrecords WHERE patientID = " + txtSearch.Text + "AND statusA = '1' "+";";
            }else if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                query = "SELECT * FROM patientrecords WHERE lastname = '" + txtSearch.Text + "' OR  firstname = '"+ txtSearch.Text +"' OR middlename = '" + txtSearch.Text +"' OR sex = '" + txtSearch.Text + "' OR civilStatus ='" + txtSearch.Text + "AND statusA = '1'" +"';";
            }

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
                        // Assuming the birthdate is stored in the 7th column (index 6)
                        DateTime birthdate = reader.GetDateTime(4);

                        // Extract year, month, and day
                        int year = birthdate.Year;
                        int month = birthdate.Month;
                        int day = birthdate.Day;

                        dgvData.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), month + "/" + day + "/" + year, reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                    }
                }

            }
            catch (Exception x)
            {
                MessageBox.Show("Query error: " + x.Message);
            }


        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            load();
            txtSearch.Clear();
        }
    }
}
