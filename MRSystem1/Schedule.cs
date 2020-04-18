using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    public class Schedule
    {
        public int sid { get; set; }
        public int uid { get; set; }
        public string places { get; set; }
        public Boolean approved { get; set; }

        public DateTime from { get; set; }

        public DateTime to { get; set; }

        public Schedule(int uid, string places, Boolean approved, DateTime from , DateTime to)
        {
            this.sid = -1;
            this.uid = uid;
            this.places = places;
            this.approved = approved;
            this.from = from;
            this.to = to;
        }
        public Schedule()
        {

        }
        public override string ToString()
        {
            return this.sid.ToString() + " places " + this.places;
        }

    }
}
