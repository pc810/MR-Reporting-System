using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    public class Drug
    {
        public int did { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public Drug(int did,string name,string description,double price)
        {
            did = this.did;
            name = this.name;
            description = this.description;
            price = this.price;
        }

        public Drug()
        {
        }
    }
}
