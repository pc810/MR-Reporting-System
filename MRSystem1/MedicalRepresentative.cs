using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    public class MedicalRepresentative : Employee
    {
        private List<String> Locality { get; set; }

        private bool Assigned { get; set; }
        public MedicalRepresentative()
        {
            Assigned = false;
        }
        public MedicalRepresentative(String name, String password, String email, String address, String phonenumber, List<String> locality):base(name,password,email,address,phonenumber)
        {
            this.Locality = locality;
        }
        public void addLocality(String locality)
        {
            Locality.Add(locality);
        }

        public void removeLocality(String locality)
        {
            Locality.Remove(locality);
        }
    }
}
