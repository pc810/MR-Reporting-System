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
    public partial class viewReport : Form
    {
        private MRDB mrdb;
        private int reportid = -1;
        public viewReport(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
            List<String> listofmrsList = new List<string>() {"0","123"};
            listBox1.DataSource = listofmrsList;
        }

        void displayStatus(Report report)
        {
            if (report != null)
            {
                reportid = report.rid;
                label5.Text = report.place;
                label7.Text = report.approved;
                List<Doctor> doctors = mrdb.getReportDoctorList(report.rid);
                listBox2.DataSource = doctors.Select(d => d.name + " " + d.degree).ToList();
            }
            else
            {
                label5.Text = "";
                listBox2.DataSource = null;
                reportid = -1;

            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Report report=mrdb.getReport(Int32.Parse(listBox1.SelectedItem as string),dateTimePicker1.Value);
            displayStatus(report);

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Report report = mrdb.getReport(Int32.Parse(listBox1.SelectedItem as string), dateTimePicker1.Value);
            displayStatus(report);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (reportid != -1)
            {
                mrdb.updateReportStatus(reportid);
                label7.Text = "true";
            }
        }
    }
}
