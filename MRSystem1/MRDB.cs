using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRSystem;

namespace MRSystem1
{
    public class MRDB
    {
        private readonly string cs;
       
        public MRDB()
        {
            cs = @"Data Source = (LOCALDB)\MSSqlLocalDb; Initial Catalog = MrDB; Integrated Security = True; Connect Timeout = 15; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        }

        public int createMedince(AbstractMedicine medicine)
        {
            int mid = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "INSERT INTO AbstractMedicine (name,description,type,state,price) VALUES(@name,@description,@type,@state,@price);select scope_identity();"
                };

                cmd.Parameters.Add(new SqlParameter("@name", medicine.name));
                cmd.Parameters.Add(new SqlParameter("@description", medicine.description));
                cmd.Parameters.Add(new SqlParameter("@type", medicine.type));
                cmd.Parameters.Add(new SqlParameter("@state", medicine.state));
                cmd.Parameters.Add(new SqlParameter("@price", medicine.price));
                con.Open();
                mid = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return mid;
        }

        public void createDrug(Drug drug)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "INSERT INTO Drug (name,description,price) VALUES(@name,@description,@price)"
                };

                cmd.Parameters.Add(new SqlParameter("@name", drug.name));
                cmd.Parameters.Add(new SqlParameter("@description", drug.description));
                cmd.Parameters.Add(new SqlParameter("@price", drug.price));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void addDrugToMedicine(int mid, List<DrugWeight> drugWeights)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                foreach (DrugWeight dw in drugWeights)
                {


                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = con,
                        CommandText =
                            "INSERT INTO MedicineDrug (mid,did,weight) VALUES(@mid,@did,@weight)"
                    };

                    cmd.Parameters.Add(new SqlParameter("@mid", mid));
                    cmd.Parameters.Add(new SqlParameter("@did", dw.did));
                    cmd.Parameters.Add(new SqlParameter("@weight", dw.weight));


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Drug> getAllDrugs()
        {
            List<Drug> allDrugs = new List<Drug>();

           
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Drug";
               
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Drug d = new Drug();
                    d.did = Convert.ToInt32(reader["did"]);
                    d.name = reader["name"].ToString();
                    d.description = reader["description"].ToString();
                    d.price = Convert.ToDouble(reader["price"]);
                 
                    allDrugs.Add(d);
                }
            }

            return allDrugs;
        
    }
        public int getDrugIdByName(string drugname)
        {

            int did = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = "select did from Drug where name = @name";
                command.Parameters.Add(new SqlParameter("@name", drugname));
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    did = Convert.ToInt32(reader["did"]);
                }
            }
            return did;
        }


        public List<Doctor> getDoctorByCity(string city)
        {
            List<Doctor> doctors = new List<Doctor>();


            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Doctor where city = @city";
                cmd.Parameters.Add(new SqlParameter("@city", city));
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Doctor d = new Doctor();
                    d.docid = Convert.ToInt32(reader["docid"]);
                    d.name = reader["name"].ToString();
                    d.address = reader["address"].ToString();
                    d.city = reader["city"].ToString();
                    d.phonenumber = reader["phonenumber"].ToString();
                    d.degree = reader["degree"].ToString();


                    doctors.Add(d);
                }
            }

            return doctors;
        }

        public int getDocIdByNameAndCity(string name, string city)
        {
            int docid = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Doctor where name=@name and city = @city";
                cmd.Parameters.Add(new SqlParameter("@city", city));
                cmd.Parameters.Add(new SqlParameter("@name", name));

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    docid = Convert.ToInt32(reader["docid"]);
                }

            }

            return docid;
        }

        public void addDoctor(Doctor doctor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "INSERT INTO Doctor (name,address,city,phonenumber,degree) VALUES(@name,@address,@city,@phonenumber,@degree)"
                };

                cmd.Parameters.Add(new SqlParameter("@name", doctor.name));
                cmd.Parameters.Add(new SqlParameter("@description", doctor.address));
                cmd.Parameters.Add(new SqlParameter("@price", doctor.city));
                cmd.Parameters.Add(new SqlParameter("@phonenumber", doctor.phonenumber));
                cmd.Parameters.Add(new SqlParameter("@degree", doctor.degree));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public int createReport(Report report)
        {
            int rid = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "INSERT INTO Report (mrid,place,reportDate) VALUES(@mrid,@place,@reportDate);select scope_identity();"
                };
                cmd.Parameters.Add(new SqlParameter("@mrid", report.mrid));
                cmd.Parameters.Add(new SqlParameter("@place",report.place));
                cmd.Parameters.Add(new SqlParameter("@reportDate", report.reportDate));

                con.Open();
                rid = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return rid;
        }

        public void addDoctorToReport(int rid, List<int> visitedDoctor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                foreach (int docid in visitedDoctor)
                {


                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = con,
                        CommandText =
                            "INSERT INTO ReportDoctor (rid,docid) VALUES(@rid,@docid)"
                    };

                    cmd.Parameters.Add(new SqlParameter("@rid", rid));
                    cmd.Parameters.Add(new SqlParameter("@docid", docid));
                   

                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
