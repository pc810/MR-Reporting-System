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
    public partial class Register : Form
    {
        private readonly MRDB mrdb;

        public Register(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f = new ManagerRegistration(this.mrdb);
            f.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new MRRegistration(this.mrdb);
            f.Visible = true;
            this.Visible = false;
        }
    }
}
