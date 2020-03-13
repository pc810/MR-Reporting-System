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
            this.mrdb = new MRDB();
            InitializeComponent();
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
    }
}
