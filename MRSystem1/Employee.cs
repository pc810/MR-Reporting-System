using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    abstract public class Employee
    {
        public Employee()
        {

        }
        public int Id { get; set; }
        public String Name { get; set; }

        public String Password { get; set; }

        public String Email { get; set; }

        public String Address { get; set; }

        public String PhoneNumber { get; set; }

        public Employee(String name,String password,String email,String address,String phonenumber)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.PhoneNumber = phonenumber;
        }
        public bool login(String Email,String Password)
        {
            return true;
        }

        public bool changePassword(string OldPassword,string NewPassword)
        {

            if(OldPassword == this.Password)
            {
                this.Password = NewPassword;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public Employee getProfileDetail()
        {
            return this;
        }

        private bool updateProfile(Employee e)
        {
            this.Name = e.Name;
            this.Email = e.Email;
            this.Address = e.Address;
            this.PhoneNumber = e.PhoneNumber;
            return true;
        }

    }
}
