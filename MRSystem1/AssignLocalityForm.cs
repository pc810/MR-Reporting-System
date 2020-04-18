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
    public partial class AssignLocalityForm : Form
    {
        private readonly MRDB mrdb;
        private Dictionary<string, string[]> regionLocalityMap = new Dictionary<string, string[]>();

        public AssignLocalityForm(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
            List<KeyValuePair<int, string>> mrlist = this.mrdb.getMRList();
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.DataSource = new BindingSource(mrlist, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";



            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();

            string[] cg = { "Ahmedabad", "Vadodara", "Anand", "Chhota Udaipur", "Dahod", "Kheda", "Mahisagar", "Panchmahal" };
            string[] ng = { "Gandhinagar", "Aravalli", "Banaskantha", "Mehsana", "Patan", "Sabarkantha" };
            string[] sau = {"Rajkot","Amreli","Bhavnagar","Botad","Devbhoomi Dwarka",
              "Gir Somnath","Jamnagar","Junagadh","Morbi","Porbandar","Surendranagar" };
            string[] kut = { "kutch" };
            string[] sg = { "Surat", "Bharuch", "Dang", "Narmada", "Navsari", "Tapi", "Valsad" };
            //remainging from here

            String[] region_list = { "Central Gujarat", "North Gujarat", "Saurashtra", "Kutch", "South Gujarat" };

            regionLocalityMap.Add("Central Gujarat", cg);
            regionLocalityMap.Add("North Gujarat", ng);
            regionLocalityMap.Add("Saurashtra", sau);
            regionLocalityMap.Add("Kutch", kut);
            regionLocalityMap.Add("South Gujarat", sg);
            Array.Sort(region_list);
            foreach (string region in region_list)
            {
                data.Add(new KeyValuePair<string, string>(region, region));
            }
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DataSource = new BindingSource(data, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
            LocalityInput.Items.Clear();
            foreach (string locality in regionLocalityMap[region_list[0]])
            {
                LocalityInput.Items.Add(locality);
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var keys = regionLocalityMap.Keys.ToList();
            LocalityInput.Items.Clear();
            foreach (string region in keys)
            {
                if (comboBox2.SelectedValue.ToString().Equals(region))
                {
                    var localitylist = regionLocalityMap[region];
                    //List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
                    foreach (string locality in localitylist)
                        LocalityInput.Items.Add(locality);
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int uid = int.Parse(comboBox1.SelectedValue.ToString());
            MessageBox.Show("Email " + uid);
            string locality = "";
            int i;
            if (LocalityInput.SelectedItems.Count > 0) { 
                for (i = 0; i < LocalityInput.SelectedItems.Count; i++)
                {
                    var r = LocalityInput.SelectedItems[i];
                    if (LocalityInput.SelectedItems.Count - 1 == i)
                        locality += r;
                    else
                        locality += r + ",";
                }                
            }
            MessageBox.Show("locality " + locality);
            try
            {
                mrdb.UpdateLocality(uid, locality);
            }
            catch
            {
                MessageBox.Show("fail to update");
            }
            this.Visible = false;
        }
    }
}
