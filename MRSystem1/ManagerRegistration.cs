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
    public partial class ManagerRegistration : Form
    {
        private readonly MRDB mrdb;
        public ManagerRegistration(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            /*  String[] region_list = { "Ahmedabad", "Vadodara", "Anand", "Chhota Udaipur", "Dahod", "Kheda", "Mahisagar", "Panchmahal",
              "Gandhinagar","Aravalli","Banaskantha","Mehsana","Patan","Sabarkantha","Rajkot","Amreli","Bhavnagar","Botad","Devbhoomi Dwarka",
              "Gir Somnath","Jamnagar","Junagadh","Morbi","Porbandar","Surendranagar","Kachchh",
              "Surat","Bharuch","Dang","Narmada","Navsari","Tapi","Valsad"
              };*/
            String[] region_list = { "Central Gujarat", "North Gujarat", "Saurashtra", "Kutch", "South Gujarat" };
            Array.Sort(region_list);
            foreach (string region in region_list)
            {
                data.Add(new KeyValuePair<string, string>(region, region));
            }
            RegionInput.DataSource = null;
            RegionInput.Items.Clear();
            RegionInput.DataSource = new BindingSource(data, null);
            RegionInput.DisplayMember = "Value";
            RegionInput.ValueMember = "Key";
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string Name = NameInput.Text;
            string Email = EmailInput.Text;
            string Address = AddressInput.Text;
            string Region = RegionInput.Text;
            string PhoneNumber = PhoneNumberInput.Text;
            string Password = PasswordInput.Text;
            string ConfirmPassword = ConfirmPasswordInput.Text;
            //string Company = CompanyInput.Text;


       //     MessageBox.Show(Name + Email + Address + Region + PhoneNumber + Password + ConfirmPassword + Company);
            Manager manager = new Manager(Name, Password, Email, Address, PhoneNumber, new List<string> { Region });
            mrdb.RegisterManger(manager);
            this.Visible = false;
            //string joined = string.Join(",", manager.getRegion());
            //MessageBox.Show(joined);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
