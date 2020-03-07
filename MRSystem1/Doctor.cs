using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    public class Doctor
    {
        public Doctor(int docid,string name,string address,string city,string phonenumber,string degree)
        {
            this.docid = docid;
            this.name = name;
            this.address = address;
            this.city = city;
            this.phonenumber = phonenumber;
            this.degree = degree;


        }

        public Doctor()
        {
            
        }

        public int docid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string phonenumber { get; set; }
        public string degree { get; set; }
    }
}
