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
    public partial class UpdateMedicine : Form
    {
        private MRDB mrdb;
        private List<AbstractMedicine> medicines;
        private AbstractMedicine selectedAbstractMedicine;
        public UpdateMedicine(MRDB mrdb)
        {
            this.mrdb = mrdb;
            InitializeComponent();
            medicines = this.mrdb.GetAllMedicines();

            List<String> medList = medicines.Select(m => m.name).ToList();
            listBox1.DataSource = medList;
        }

        public void updateList()
        {
            medicines = this.mrdb.GetAllMedicines();

            List<String> medList = medicines.Select(m => m.name).ToList();
            listBox1.DataSource = medList;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            selectedAbstractMedicine.name = textBox1.Text;
            selectedAbstractMedicine.description = textBox2.Text;
            selectedAbstractMedicine.type = textBox3.Text;
            selectedAbstractMedicine.state = textBox4.Text;
            selectedAbstractMedicine.price = Int32.Parse(textBox5.Text);
            mrdb.updateMedicine(selectedAbstractMedicine);
            updateList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selected = listBox1.SelectedItem as string;

            selectedAbstractMedicine = medicines.Find(m => m.name == selected);
           
            textBox1.Text = selectedAbstractMedicine.name;
            textBox2.Text = selectedAbstractMedicine.description;
            textBox3.Text = selectedAbstractMedicine.type;
            textBox4.Text = selectedAbstractMedicine.state;
            textBox5.Text = selectedAbstractMedicine.price.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            mrdb.deleteMedicine(selectedAbstractMedicine.mid);
            updateList();
        }
    }
}
