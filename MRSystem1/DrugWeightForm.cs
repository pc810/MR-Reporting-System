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
    public partial class DrugWeightForm : Form
    {
        List<Label> labels = new List<Label>();
        List<TextBox> textbox = new List<TextBox>();
        public static Dictionary<string,double> drug_values = new Dictionary<string, double>();
        public DrugWeightForm(List<string> s)
        {
            InitializeComponent();

            //List<string> d = (List<string>)s;
            
            int y = 20;
            foreach(string x in s)
            {
                Label l = new Label {Text = x, Location = new Point(20, y)};
                labels.Add(l);

                TextBox t = new TextBox {Location = new Point(200, y)};
                textbox.Add(t);
                y += 50;

            }

            foreach (Label l in labels)
            {
                groupBox1.Controls.Add(l);
            }

            foreach (TextBox t in textbox)
            {
                groupBox1.Controls.Add(t);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach(Label l in labels)
            {
                drug_values.Add(l.Text,double.Parse(textbox[i].Text));
                i++;
            }
        }
    }
}
