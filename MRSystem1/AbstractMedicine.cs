using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem
{
    public class DrugWeight
    {
        public int did { get; set; }
        public double weight { get; set; }

        public DrugWeight(int d,double w)
        {
            this.did = d;
            this.weight = w;
        }
    }
    public class AbstractMedicine
    {
        public AbstractMedicine()
        {

        }
        public AbstractMedicine(int mid, string name, string description, string type, string state, List<DrugWeight> drug_weight,double price)
        {
            this.mid = mid;
            this.name = name;
            this.description = description;
            this.type = type;
            this.state = state;
            this.price = price;
            this.drugweight = drug_weight;
        }

        public int mid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public double price { get; set; }


        public List<DrugWeight> drugweight { get; set; }


    }
}
