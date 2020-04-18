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
    public partial class ConfirmScheduleForm : Form
    {
        private readonly MRDB mrdb;
        private List<Schedule> list = null;
        public ConfirmScheduleForm(MRDB mrdb)
        {
            this.mrdb = mrdb;            
            Load += formLoad;
            InitializeComponent();      
        }
        private void formLoad(object sender, EventArgs e)
        {
            list = this.mrdb.GetSchedules();
            listView1.Items.Clear();
            foreach (var sc in list)
            {
                var row = new string[] { sc.sid.ToString(), sc.uid.ToString(), sc.places, sc.approved.ToString(), sc.from.ToString(), sc.to.ToString() };
                var lvi = new ListViewItem(row);
                lvi.Tag = sc;
                listView1.Items.Add(lvi);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selecteditem = (Schedule)listView1.SelectedItems[0].Tag;
                if (selecteditem!=null)
                {
                    //MessageBox.Show(selecteditem.ToString());
                  //  MessageBox.Show(selecteditem.ToString());
                    Form f1 = new ConfirmSchedule(mrdb, selecteditem.sid);
                    f1.Visible = true;
                    this.Visible = false;
                }                
            }catch
            {
                
            }
        }
    }
}

