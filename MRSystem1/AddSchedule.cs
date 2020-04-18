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
    public partial class AddSchedule : Form
    {
        private readonly MRDB mrdb;
        public AddSchedule(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        //    MessageBox.Show(FromdateTimePicker.Text + " " + TodateTimePicker.Text + " : " + textBox1.Text);
            
            DateTime from = new DateTime(FromdateTimePicker.Value.Ticks);
            DateTime to = new DateTime(TodateTimePicker.Value.Ticks);
            Schedule schedule = new Schedule(mrdb.uid, textBox1.Text, false, from, to);
            mrdb.addSchedule(schedule);
            this.Visible = false;
        }
    }
}
