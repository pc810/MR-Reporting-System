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
    public partial class ConfirmSchedule : Form
    {
        private readonly MRDB mrdb;
        private Schedule sc;
        public ConfirmSchedule(MRDB mrdb, int sid)
        {
            InitializeComponent();
            this.mrdb = mrdb;            
            sc = mrdb.getSchedule(sid);
            if (sc != null)
            {
                label4.Text = sc.sid.ToString();
                label6.Text = sc.uid.ToString();
                textBox1.Text = sc.places;
                label8.Text = sc.from.ToString();
                label10.Text = sc.to.ToString();
                if (sc.approved)
                    listBox1.SelectedIndex = 1;
                else
                    listBox1.SelectedIndex = 0;
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sc.places = textBox1.Text;
            if (listBox1.SelectedIndex == 0)
                sc.approved = false;
            else
                sc.approved = true;
            this.mrdb.updateSchedule(sc.sid, sc.approved, sc.places);
            this.Visible = false;
        }
    }
}
