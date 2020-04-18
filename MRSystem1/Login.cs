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
    public partial class Login : Form
    {
        private MRDB mrdb;
        public Login()
        {
            this.mrdb = new MRDB();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string pass = textBox2.Text;
            Boolean ret = this.mrdb.logIn(email, pass);
            if (ret)
            {
                //MessageBox.Show("success : " + this.mrdb.uid);
                Form f = new Main(mrdb);
                f.Visible = true;
                this.Visible = false;
                //this.Visible = false;
            }
            else
                MessageBox.Show("fail");
        }

        private void button2_Click(object sender, EventArgs e)
        {/*
            Form f = new Register(this.mrdb);
            f.Visible = true;
            */
            Form f = new ManagerRegistration(this.mrdb);
            f.Visible = true;            
        }
    }
}
