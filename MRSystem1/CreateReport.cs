using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRSystem1
{
    public partial class CreateReport : Form
    {
        private MRDB mrdb;
        public CreateReport(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
            List<string> cities = new List<string>()
            {
                "nadiad",
                "ahmedabad",
                "vadodara"
            };
            listBox1.DataSource = cities;
            label1.Text = "";

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string city = listBox1.SelectedItem as string;

            List<Doctor> doctors = mrdb.getDoctorByCity(city);

            List<string> doctornames = doctors.Select(doc => "Dr. "+doc.name + " , " + doc.degree).ToList();
            checkedListBox1.DataSource = doctornames;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.mrid = 123;
            report.reportDate = dateTimePicker1.Value;
            report.place = listBox1.SelectedItem as string;
            int rid = mrdb.createReport(report);

            List<int> docids  = new List<int>();
            string temp;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    temp=checkedListBox1.Items[i] as string;
                    temp=temp.Split()[1] + " " + temp.Split()[2];
                    docids.Add(mrdb.getDocIdByNameAndCity(temp,report.place));
                }
            }

            mrdb.addDoctorToReport(rid,docids);
            label1.Text = "Report of Date : " + report.reportDate.Day +"/"+report.reportDate.Month+"/"+report.reportDate.Year + " Successfully saved";


        }
    }
}
