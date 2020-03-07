using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem
{
    public class PharmaCompany
    {
        public PharmaCompany(int cid, string name, string address, string pincode, string officePhoneNumber, string factoryPhoneNumber, string email)
        {
            this.cid = cid;
            Name = name;
            Address = address;
            Pincode = pincode;
            OfficePhoneNumber = officePhoneNumber;
            FactoryPhoneNumber = factoryPhoneNumber;
            Email = email;
            Manager = null;
            MRs = null;
            medicines = null;
        }

        private List<Manager> Manager { get; }

        private List<MedicalRepresentative> MRs { get; }

        private List<AbstractMedicine> medicines { get; }
        private int cid { get; set; }
           
        private String Name { get; set; }
        
        private String Address { get; set; }

        private String Pincode { get; set; }

        private String OfficePhoneNumber { get; set; }

        private String FactoryPhoneNumber { get; set; }

        private String Email { get; set; }


        public bool addManager(Manager m)
        {
                Manager.Add(m);
                return true;
        }

        public bool removeManager(Manager m)
        {
            Manager.Remove(m);
            return true;
        }

        public bool addMedicalRepresentative(MedicalRepresentative m)
        {
            MRs.Add(m);
            return true;
        }

        public bool removeMedicalRepresentative(MedicalRepresentative m)
        {
            MRs.Remove(m);
            return true;
        }

        public bool addNewMedicine(AbstractMedicine m)
        {
            medicines.Add(m);
            return true;
        }

        public bool removeMedicine(AbstractMedicine m)
        {
            medicines.Remove(m);
            return true;
        }



    }
}
