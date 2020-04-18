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
    public partial class MRRegistration : Form
    {
        private readonly MRDB mrdb;
        public MRRegistration(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string Name = NameInput.Text;
            string Email = EmailInput.Text;
            string Address = AddressInput.Text;
            string PhoneNumber = PhoneNumberInput.Text;
            string Password = PasswordInput.Text;
            string ConfirmPassword = ConfirmPasswordInput.Text;
       //     MessageBox.Show(Name + Email + Address + Region + PhoneNumber + Password + ConfirmPassword);
            MedicalRepresentative mr = new MedicalRepresentative(Name, Password, Email, Address, PhoneNumber, null);
            mrdb.RegisterMR(mr);
            this.Visible = false;
        }
    }
}
