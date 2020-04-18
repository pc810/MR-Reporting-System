using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem1
{
    public class MRDB
    {
        private readonly string cs;
        public Boolean isLoggedin = false;
        public Boolean isManager= false;
        public Boolean isMR = false;
        public int uid = -1;
       
        public MRDB()
        {
            //cs = @"Data Source = (LOCALDB)\MSSqlLocalDb; Initial Catalog = MrDB; Integrated Security = True; Connect Timeout = 15; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            cs = @"Data Source=(LOCALDB)\MSSqlLocalDb;AttachDbFilename=D:\work\MR-Reporting-System\MrDB.mdf;Integrated Security=True";
        }

        public List<Schedule> GetSchedulesForCurrent()
        {
            List<Schedule> list = new List<Schedule>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Schedule where uid = @uid";
                cmd.Parameters.Add(new SqlParameter("@uid", this.uid));
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Schedule sc = new Schedule();
                    sc.sid = Convert.ToInt32(reader["sid"]);
                    sc.uid = Convert.ToInt32(reader["uid"]);
                    sc.places = reader["places"].ToString();
                    sc.approved = (Boolean)reader["approved"];
                    sc.from = (DateTime)reader["from"];
                    sc.to = (DateTime)reader["to"];
                    list.Add(sc);
                }
            }
            return list;
        }

        public Boolean logIn(string email, string pass)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "select * from [dbo].[User] where email = @email and password = @password"
                };

                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@password", pass));
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        this.isLoggedin = true;
                        reader.Read();
                        uid = reader.GetInt32(0);
                        if (reader.GetString(4).Equals("manager"))
                            isManager = true;
                        else
                            isMR = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public void RegisterMR(MedicalRepresentative mr)
        {
            int mrid = -1;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "INSERT INTO [dbo].[User] ([name], [password], [email], [role], [address], [phonenumber]) VALUES (@name, @password,@email, @role, @address,@phonenumber)"
                };

                cmd.Parameters.AddWithValue("@name", mr.Name);
                cmd.Parameters.AddWithValue("@email", mr.Email);
                cmd.Parameters.AddWithValue("@password", mr.Password);                
                cmd.Parameters.AddWithValue("@role", "mr");
                cmd.Parameters.AddWithValue("@address", mr.Address);
                cmd.Parameters.AddWithValue("@phonenumber", mr.PhoneNumber);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "select * from [dbo].[User] where email = @email and password = @password"
                };

                cmd.Parameters.Add(new SqlParameter("@email", mr.Email));
                cmd.Parameters.Add(new SqlParameter("@password", mr.Password));
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        this.isLoggedin = true;
                        reader.Read();
                        mrid = reader.GetInt32(0);                        
                    }                    
                }
            }
            if (mrid != -1)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = con,
                        CommandText =
                            "INSERT INTO [dbo].[Manager_Mr] ([mid], [mrid]) VALUES (@mid, @mrid)"
                    };

                    cmd.Parameters.AddWithValue("@mid", uid);
                    cmd.Parameters.AddWithValue("@mrid", mrid);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void RegisterManger(Manager manager)
        {
            string jregion = string.Join(",", manager.getRegion());           
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "INSERT INTO [dbo].[User] ([name], [password], [region], [email], [role], [address], [phonenumber]) VALUES (@name, @password,@region,@email, @role, @address,@phonenumber)"
                };

                cmd.Parameters.AddWithValue("@name", manager.Name);
                cmd.Parameters.AddWithValue("@email", manager.Email);
                cmd.Parameters.AddWithValue("@password", manager.Password);
                cmd.Parameters.AddWithValue("@region", jregion);
                cmd.Parameters.AddWithValue("@role", "manager");
                cmd.Parameters.AddWithValue("@address", manager.Address);
                cmd.Parameters.AddWithValue("@phonenumber", manager.PhoneNumber);                
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
       
        public List<Schedule> GetSchedules()
        {
            List<Schedule> list = new List<Schedule>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Schedule";

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Schedule sc = new Schedule();
                    sc.sid = Convert.ToInt32(reader["sid"]);
                    sc.uid = Convert.ToInt32(reader["uid"]);
                    sc.places = reader["places"].ToString();
                    sc.approved = (Boolean)reader["approved"];
                    sc.from = (DateTime)reader["from"];
                    sc.to = (DateTime)reader["to"];
                    list.Add(sc);
                }
            }
            return list;
        }
       
        public Schedule getSchedule(int sid)
        {
            Schedule sc = new Schedule();            
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Schedule where sid = @sid";
                cmd.Parameters.AddWithValue("@sid", sid);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {                    
                    sc.sid = Convert.ToInt32(reader["sid"]);
                    sc.uid = Convert.ToInt32(reader["uid"]);
                    sc.places = reader["places"].ToString();
                    sc.approved = (Boolean)reader["approved"];
                    sc.from = (DateTime)reader["from"];
                    sc.to = (DateTime)reader["to"];                    
                }
            }
            return sc;
        }
        public void addSchedule(Schedule schedule)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "INSERT INTO [dbo].[Schedule] ([uid], [places], [approved], [from], [to]) VALUES (@uid, @places,@approved,@from, @to)"
                };

                cmd.Parameters.AddWithValue("@uid", schedule.uid);
                cmd.Parameters.AddWithValue("@places", schedule.places);
                cmd.Parameters.AddWithValue("@approved", schedule.approved);
                cmd.Parameters.AddWithValue("@from", schedule.from);
                cmd.Parameters.AddWithValue("@to", schedule.to);                
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void updateSchedule(int sid, Boolean approved, string places)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "UPDATE [dbo].[Schedule] set approved = @approved,places = @places where sid = @sid"
                };

                cmd.Parameters.AddWithValue("@approved", approved);
                cmd.Parameters.AddWithValue("@places", places);
                cmd.Parameters.AddWithValue("@sid", sid);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateLocality(int id, string locality)
        {            
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "UPDATE [dbo].[User] set region = @region where uid = @uid"
                };

                cmd.Parameters.AddWithValue("@region", locality);
                cmd.Parameters.AddWithValue("@uid", id);                
                con.Open();
                cmd.ExecuteNonQuery();                
            }
        }
        public List<KeyValuePair<int, string>> getMRList()
        {
            List<int> mrlistid = new List<int>();
            List<string> mrlist = new List<string>();
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "select * from [dbo].[Manager_Mr] where mid = @mid"
                };
                cmd.Parameters.Add(new SqlParameter("@mid", uid));                
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {                                                
                        mrlistid.Add(reader.GetInt32(1));
                    }
                }
                con.Close();
                for (int i = 0; i < mrlistid.Count; i++)
                {
                    using (SqlConnection con1 = new SqlConnection(cs))
                    {
                        SqlCommand cmd1 = new SqlCommand
                        {
                            Connection = con1,
                            CommandText =
                                "select * from [dbo].[User] where uid = @uid"
                        };
                        con1.Open();

                        cmd1.Parameters.Add(new SqlParameter("@uid", mrlistid[i]));
                        using (SqlDataReader reader = cmd1.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {                                
                                reader.Read();                                
                                data.Add(new KeyValuePair<int, string>(mrlistid[i], reader.GetString(3)));
                            }
                        }
                    }
                }
            }
            return data;
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

        public List<AbstractMedicine> GetAllMedicines()
        {
            List<AbstractMedicine> allMedicines = new List<AbstractMedicine>();


            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from AbstractMedicine";

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AbstractMedicine medicine = new AbstractMedicine();
                    medicine.mid = Convert.ToInt32(reader["mid"]);
                    medicine.name = reader["name"].ToString();
                    medicine.description = reader["description"].ToString();
                    medicine.price = Convert.ToDouble(reader["price"]);
                    medicine.type = reader["type"].ToString();
                    medicine.state = reader["state"].ToString();


                    allMedicines.Add(medicine);
                }
            }

            return allMedicines;

        }

        public void updateMedicine(AbstractMedicine medicine)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "Update AbstractMedicine SET name = @name, description=@description, type=@type, state=@state, price=@price where mid =@mid"
                };

                cmd.Parameters.Add(new SqlParameter("@name", medicine.name));
                cmd.Parameters.Add(new SqlParameter("@description", medicine.description));
                cmd.Parameters.Add(new SqlParameter("@price", medicine.price));
                cmd.Parameters.Add(new SqlParameter("@type", medicine.type));
                cmd.Parameters.Add(new SqlParameter("@state", medicine.state));
                cmd.Parameters.Add(new SqlParameter("@mid", medicine.mid));



                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteMedicine(int mid)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "Delete From AbstractMedicine where mid = @mid"
                };

              
                cmd.Parameters.Add(new SqlParameter("@mid", mid));



                con.Open();
                cmd.ExecuteNonQuery();
            }
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
                        "INSERT INTO Report (mrid,place,reportDate,approved) VALUES(@mrid,@place,@reportDate,@approved);select scope_identity();"
                };
                cmd.Parameters.Add(new SqlParameter("@mrid", report.mrid));
                cmd.Parameters.Add(new SqlParameter("@place",report.place));
                cmd.Parameters.Add(new SqlParameter("@reportDate", report.reportDate));
                cmd.Parameters.Add(new SqlParameter("@approved", "false"));


                con.Open();
                rid = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return rid;
        }


        public Report getReport(int mrid,DateTime date)
        {
            //List<Report> reports = new List<Report>();
            Report report = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Report where mrid=@mrid and reportDate=@reportDate";
                cmd.Parameters.Add(new SqlParameter("@mrid", mrid));
                cmd.Parameters.Add(new SqlParameter("@reportDate", date.Date));


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    report = new Report();
                    report.rid = Convert.ToInt32(reader["rid"]);
                    report.mrid = Convert.ToInt32(reader["mrid"]);
                    report.place = reader["place"].ToString();
                    report.reportDate = Convert.ToDateTime(reader["reportDate"]);
                    report.approved = reader["approved"].ToString();
                }

            }

            return report;
        }

        public List<Doctor> getReportDoctorList(int rid)
        {
            List<Doctor> doctors = new List<Doctor>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from Doctor where docid in (Select docid from ReportDoctor where rid=@rid)";
                cmd.Parameters.Add(new SqlParameter("@rid", rid));


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Doctor doctor = new Doctor();
                    doctor.docid = Convert.ToInt32(reader["docid"]);
                    doctor.name = reader["name"].ToString();
                    doctor.address = reader["address"].ToString();
                    doctor.degree = reader["degree"].ToString();
                    doctors.Add(doctor);
                }

            }

            return doctors;
        }

        public void updateReportStatus(int rid)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText =
                        "Update Report SET approved = @approved where rid =@rid"
                };

                cmd.Parameters.Add(new SqlParameter("@rid",rid));
                cmd.Parameters.Add(new SqlParameter("@approved", "true"));
               



                con.Open();
                cmd.ExecuteNonQuery();
            }
            
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
