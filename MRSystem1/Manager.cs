using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    public class Manager : Employee
    {
        private List<String> Region { get; set; }

        private List<Employee> mrs;
        public Manager()
        {

        }
        public Manager(String name, String password, String email, String address, String phonenumber,List<String> region):base(name,password,email,address,phonenumber)
        {
            this.Region = region;
        }

        public void addRegion(String region)
        {
            Region.Add(region);
        }

        public void removeRegion(String region)
        {
            Region.Remove(region);
        }
        public bool addSubordinate(Employee e)
        {
            if (e.GetType().Equals(new MedicalRepresentative().GetType()))
            {
                mrs.Add(e);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Employee> getSubordinates()
        {
            return mrs;
        }
        public List<String> getRegion()
        {
            return this.Region;
        }
    }
}
