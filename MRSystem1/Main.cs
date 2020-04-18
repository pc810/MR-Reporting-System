using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRSystem1
{
    public partial class Main : Form
    {
        private MRDB mrdb;
        public Main(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
            if (mrdb.isMR)
            {
                groupBox1.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
            }
            else
            {
                groupBox2.Visible = false;
                button3.Visible = false;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new AddMedicineForm(mrdb);
            f.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f = new UpdateMedicine(mrdb);
            f.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f = new CreateReport(mrdb);
            f.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form f = new viewReport(mrdb);
            f.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form f = new MRRegistration(this.mrdb);
            f.Visible = true;
            //this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form f = new AssignLocalityForm(mrdb);
            f.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form f = new AddSchedule(this.mrdb);
            f.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form f = new ConfirmScheduleForm(this.mrdb);
            f.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form f = new ScheduleList(this.mrdb);
            f.Visible = true;
        }
    }
}
