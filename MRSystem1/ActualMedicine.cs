using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem
{
    class Medicine : AbstractMedicine
    {
        public DateTime manufactureDate { get; set; }
        public DateTime expiryDate { get; set; }
        public Medicine(float price,DateTime mfg,DateTime exp,int mid, string name, string description, string type, string state, List<DrugWeight> drug_weight):base(mid,name,description,type,state,drug_weight,price)
        {
            this.manufactureDate = mfg;
            this.expiryDate = exp;
        }

    }
}
