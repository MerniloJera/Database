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
    public partial class CheckUp : Form
    {
        public CheckUp()
        {
            InitializeComponent();
        }

        int id = 1;

        public void load()
        {
            dgvData.Rows.Clear();

            dgvData.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dgvData.Font, System.Drawing.FontStyle.Bold);
            string[] columnNames = new string[] { "Record ID","First Name","Lastname","Middle Name","Birthdate","Sex","Civil Status", "Age", "Weight", "Height", "Check Up Details" };

            dgvData.ColumnCount =11;

            for (int a = 0; a < columnNames.Length; a++)
            {
                dgvData.Columns[a].Name = columnNames[a];
            }

            string query = "SELECT  healthrecords.recordId, patientrecords.firstname, patientrecords.lastname, patientrecords.middlename, patientrecords.birthdate, patientrecords.sex, patientrecords.civilStatus, healthrecords.age, healthrecords.weight, healthrecords.height, healthrecords.checkupDe FROM healthrecords INNER JOIN patientrecords on healthrecords.patientId = patientrecords.patientID  WHERE healthrecords.statusR = '1'";
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
                        DateTime birthdate = reader.GetDateTime(4);

                        // Extract year, month, and day
                        int year = birthdate.Year;
                        int month = birthdate.Month;
                        int day = birthdate.Day;

                        dgvData.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), month + "/" + day + "/" + year , reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                    }
                }

            }
            catch (Exception x)
            {
                MessageBox.Show("Query error: " + x.Message);
            }
        }

        private void CheckUp_Load(object sender, EventArgs e)
        {
            string query = "SELECT count(*) from healthrecords";
            MySqlConnection connect = new MySqlConnection(ConnectionString.Connection);
            MySqlCommand command = new MySqlCommand(query, connect);

            connect.Open();
            long rowCount = (long)command.ExecuteScalar();
            connect.Close();
            if (rowCount == 0)
            {
                txtRecordID.Text = id.ToString();
            }
            else
            {
                string query2 = "SELECT recordId from healthrecords order by recordId desc limit 1;";
                MySqlConnection connect2 = new MySqlConnection(ConnectionString.Connection);
                MySqlCommand command2 = new MySqlCommand(query2, connect2);
                connect2.Open();
                id = (int)command2.ExecuteScalar();
                id += 1;
                txtRecordID.Text = id.ToString();
                connect2.Close();
            }

            load();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM patientrecords WHERE patientID = @PatientID AND statusA = '1'";
            string insertQuery = "INSERT INTO healthrecords (recordId, patientId, age, weight, height, checkupDe, statusR) VALUES (@RecordID, @PatientID, @Age, @Weight, @Height, @CheckUp, '1')";

            using (MySqlConnection connect = new MySqlConnection(ConnectionString.Connection))
            {
                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connect))
                {
                    selectCommand.Parameters.AddWithValue("@PatientID", txtPatientID.Text);

                    try
                    {
                        connect.Open();

                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Patient ID exists, proceed with insertion
                                reader.Close(); // Close the first reader before executing the second query

                                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connect))
                                {
                                    insertCommand.Parameters.AddWithValue("@RecordID", txtRecordID.Text);
                                    insertCommand.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                                    insertCommand.Parameters.AddWithValue("@Age", txtAge.Text);
                                    insertCommand.Parameters.AddWithValue("@Weight", txtWeight.Text);
                                    insertCommand.Parameters.AddWithValue("@Height", txtHeight.Text);
                                    insertCommand.Parameters.AddWithValue("@CheckUp", txtCheckUp.Text);

                                    try
                                    {
                                        insertCommand.ExecuteNonQuery();
                                        MessageBox.Show("Successfully Added");
                                        load();
                                        txtAge.Clear();
                                        txtWeight.Clear();
                                        txtCheckUp.Clear();
                                        txtPatientID.Clear();
                                        txtHeight.Clear();
                                        id++;
                                        txtRecordID.Text = id.ToString();
                                    }
                                    catch (Exception x)
                                    {
                                        MessageBox.Show("Insert error: " + x.Message);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Patient ID does not exist", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show("Select error: " + x.Message);
                    }
                }
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
            AddForm show = new AddForm();
            this.Hide();
            show.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE healthrecords SET age = @Age, weight = @Weight, height = @Height, checkupDe = @CheckUp WHERE recordId = @PatientID";

            using (MySqlConnection connect = new MySqlConnection(ConnectionString.Connection))
            {
                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connect))
                {
                    updateCommand.Parameters.AddWithValue("@Age", txtAge.Text);
                    updateCommand.Parameters.AddWithValue("@Weight", txtWeight.Text);
                    updateCommand.Parameters.AddWithValue("@Height", txtHeight.Text);
                    updateCommand.Parameters.AddWithValue("@CheckUp", txtCheckUp.Text);
                    updateCommand.Parameters.AddWithValue("@PatientID", txtRecordID.Text);

                    try
                    {
                        connect.Open();
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Update successful");
                            load();
                        }
                        else
                        {
                            MessageBox.Show("No matching records found for the provided Patient ID", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update error: " + ex.Message);
                    }
                }
            }

        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedrow;
                selectedrow = e.RowIndex;
                DataGridViewRow row = dgvData.Rows[selectedrow];

                ConnectionString.IdContent = row.Cells[0].Value.ToString();


                string query = "SELECT * FROM healthrecords WHERE recordId = '" + ConnectionString.IdContent + "'";

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

                                        txtRecordID.Text = reader.GetString(0);
                                        txtPatientID.Text = reader.GetString(1);
                                        txtAge.Text = reader.GetString(2);
                                        txtWeight.Text = reader.GetString(3);
                                        txtHeight.Text = reader.GetString(4);
                                        txtCheckUp.Text = reader.GetString(5);

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
           /* dgvData.Rows.Clear();
            dgvData.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dgvData.Font, System.Drawing.FontStyle.Bold);
            string[] columnNames = new string[] { "Record ID","First Name","Lastname","Middle Name","Birthdate","Sex","Civil Status", "Age", "Weight", "Height", "Check Up Details" };

            dgvData.ColumnCount = 12;

            for (int a = 0; a < columnNames.Length; a++)
            {
                dgvData.Columns[a].Name = columnNames[a];
            }
            string query = "";

            if (int.TryParse(txtSearch.Text, out _))
            {
                query = "SELECT * FROM healthrecords WHERE patientId = " + txtSearch.Text + " OR recordId = " + txtSearch.Text +" OR age ="+ txtSearch.Text+ ";";
            }
            else if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                query = "SELECT * FROM healthrecords WHERE checkupDe = '" + txtSearch.Text + "' OR weight = '" + txtSearch.Text +"' OR height ='" + txtSearch.Text + "';";
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
                        dgvData.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    }
                }

            }
            catch (Exception x)
            {
                MessageBox.Show("Query error: " + x.Message);
            } */
        }
    }
}
