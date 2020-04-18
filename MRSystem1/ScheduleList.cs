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
    public partial class ScheduleList : Form
    {
        private readonly MRDB mrdb;
        private List<Schedule> list = null;
        public ScheduleList(MRDB mrdb)
        {
            this.mrdb = mrdb;
            Load += formLoad;
            InitializeComponent();
        }
        private void formLoad(object sender, EventArgs e)
        {
            list = this.mrdb.GetSchedulesForCurrent();
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
           
        }
    }
}
