using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MRSystem;

namespace MRSystem1
{
    public partial class AddMedicineForm : Form
    {
        private MRDB mrdb;
        public AddMedicineForm(MRDB mrdb)
        {
            this.mrdb = mrdb;
            /*List<Drug> alldrugs = new List<Drug>();
            alldrugs.Add(new Drug(1,"ampineat","sdfasfa asjdfkl",0.24));
            alldrugs.Add(new Drug(2, "paracetamol", "sdfasfa asjdfkl",0.10));
            alldrugs.Add(new Drug(3, "dsajfkla", "sdfasfa asjdfkl",1.1));
            */
            List<Drug> alldrugs = mrdb.getAllDrugs();
            
            List<string> drugs = alldrugs.Select(s => s.name).ToList();
            /*
            drugs.Add("ampineat");
            drugs.Add("paracetamol");
            drugs.Add("dsajfkla");
            */
            InitializeComponent();
            listBox1.DataSource = drugs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbstractMedicine m = new AbstractMedicine();
             
            m.name = textBox1.Text;
            m.description = textBox2.Text;
            m.type = textBox3.Text;
            m.state = textBox4.Text;
            m.price = float.Parse(textBox5.Text);

            

            List<string> selectedrugs = new List<string>();
            foreach(object li in listBox1.SelectedItems)
            {
                selectedrugs.Add(li as string);
            }
            DrugWeightForm dw = new DrugWeightForm(selectedrugs);
            DialogResult d = dw.ShowDialog();
            List<DrugWeight> ldw = new List<DrugWeight>();
            foreach(var pair in DrugWeightForm.drug_values)
            {
                
                int did = mrdb.getDrugIdByName(pair.Key);
                double weight = pair.Value;
                ldw.Add(new DrugWeight(did,weight));
            }


            int mid = mrdb.createMedince(m);
            mrdb.addDrugToMedicine(mid,ldw);
            //m.drugweight
            label6.Text = "New Medicine Successfully Added";

        }
    }
}
