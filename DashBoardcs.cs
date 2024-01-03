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
    public partial class DashBoardcs : Form
    {
        public DashBoardcs()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddForm show = new AddForm();
            this.Hide();
            show.Show();
        }

        private void BtnPInfo_Click(object sender, EventArgs e)
        {
            PatientInfo show = new PatientInfo();
            this.Hide();
            show.Show();
        }

        private void BtnCheckup_Click(object sender, EventArgs e)
        {
            CheckUp show = new CheckUp();
            this.Hide();
            show.Show();
        }
    }
}
