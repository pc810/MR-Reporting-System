using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    public class Report
    {
        public Report()
        {

        }
        public Report(int rid,int mrid,string place,DateTime reportDate,List<int> doctorVisited)
        {
            this.rid = rid;
            this.mrid = mrid;
            this.place = place;
            this.reportDate = reportDate;
            this.doctorVisited = doctorVisited;
            this.approved = "false";
        }

        public int rid { get; set; }
        public int mrid { get; set; }
        public string place { get; set; }
        public DateTime reportDate { get; set; }
        public List<int> doctorVisited { get; set; }
        public String approved { get; set; }


    }
}
