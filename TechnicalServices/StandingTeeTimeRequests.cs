using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.TechnicalServices
{
    public class StandingTeeTimeRequests
    {
        public List<StandingTeeTimeRequest> GetAvailableStandingTeeTimes(string DayOfWeek)
        {
            List<StandingTeeTimeRequest> DailyStandingTeeTimeRequests = new List<StandingTeeTimeRequest>();

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetAvailableStandingTeeTime";

            SqlParameter day = new SqlParameter();
            day.ParameterName = "@dayofweek";

            day.SqlDbType = SqlDbType.VarChar;
            day.Value = DayOfWeek;
            day.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(day);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            CBSUsers UserManager = new CBSUsers();
            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    StandingTeeTimeRequest standingteetimerequest = new StandingTeeTimeRequest();

                    standingteetimerequest.RequestedTime = DateTime.Parse(theDataReader["RequestedTeeTime"].ToString());
                    standingteetimerequest.DayofWeek = theDataReader["DayofWeek"].ToString();
                    standingteetimerequest.Shareholder1 = !string.IsNullOrEmpty(theDataReader["Shareholder1Number"].ToString()) 
                        ? UserManager.GetUser((string)(theDataReader["Shareholder1Number"])) 
                        : new Shareholder();
                    standingteetimerequest.Shareholder2 = !string.IsNullOrEmpty(theDataReader["Shareholder2Number"].ToString()) 
                        ? UserManager.GetUser((string)(theDataReader["Shareholder2Number"])) 
                        : new Shareholder();
                    standingteetimerequest.Shareholder3 = !string.IsNullOrEmpty(theDataReader["Shareholder3Number"].ToString()) 
                        ? UserManager.GetUser((string)(theDataReader["Shareholder3Number"])) 
                        : new Shareholder();
                    standingteetimerequest.Shareholder4 = !string.IsNullOrEmpty(theDataReader["Shareholder4Number"].ToString()) 
                        ? UserManager.GetUser((string)(theDataReader["Shareholder4Number"])) 
                        : new Shareholder();
                    DailyStandingTeeTimeRequests.Add(standingteetimerequest);
                }
                theDataReader.Close();

                ClubBaistConnection.Close();

            }
            return DailyStandingTeeTimeRequests;
        }

 public StandingTeeTimeRequest GetReservedStandingTeeTime(string DayOfWeek, DateTime requestedTime, 
                                                            DateTime requestedStartDate, DateTime requestedEndDate)
        {
            StandingTeeTimeRequest standingteetimerequest = new StandingTeeTimeRequest();

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetReservedStandingTeeTime";

            SqlParameter day = new SqlParameter();
            day.ParameterName = "@dayofweek";
            day.SqlDbType = SqlDbType.VarChar;
            day.Value = DayOfWeek;
            day.Direction = ParameterDirection.Input;
            thecommand.Parameters.Add(day);
            
            SqlParameter time = new SqlParameter();
            time.ParameterName = "@time";
            time.SqlDbType = SqlDbType.Time;
            time.Value = requestedTime;
            time.Direction = ParameterDirection.Input;
            thecommand.Parameters.Add(time);
            
            SqlParameter startDate = new SqlParameter();
            startDate.ParameterName = "@startDate";
            startDate.SqlDbType = SqlDbType.Date;
            startDate.Value = requestedStartDate;
            startDate.Direction = ParameterDirection.Input;
            thecommand.Parameters.Add(startDate);
            
            SqlParameter endDate = new SqlParameter();
            endDate.ParameterName = "@endDate";
            endDate.SqlDbType = SqlDbType.Date;
            endDate.Value = requestedEndDate;
            endDate.Direction = ParameterDirection.Input;
            thecommand.Parameters.Add(endDate);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();
            
            CBSUsers UserManager = new CBSUsers();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {

                    standingteetimerequest.RequestedTime = DateTime.Parse(theDataReader["RequestedTeeTime"].ToString());
                    standingteetimerequest.DayofWeek = theDataReader["DayofWeek"].ToString();
                    standingteetimerequest.RequestedEndDate = DateTime.Parse(theDataReader["RequestedEndDate"].ToString());
                    standingteetimerequest.Shareholder1 = !string.IsNullOrEmpty(theDataReader["Shareholder1Number"].ToString()) 
                                                            ? UserManager.GetUser((string)(theDataReader["Shareholder1Number"])) 
                                                            : new Shareholder();
                    standingteetimerequest.Shareholder2 = !string.IsNullOrEmpty(theDataReader["Shareholder2Number"].ToString()) 
                                                            ? UserManager.GetUser((string)(theDataReader["Shareholder2Number"])) 
                                                            : new Shareholder();
                    standingteetimerequest.Shareholder3 = !string.IsNullOrEmpty(theDataReader["Shareholder3Number"].ToString()) 
                                                            ? UserManager.GetUser((string)(theDataReader["Shareholder3Number"])) 
                                                            : new Shareholder();
                    standingteetimerequest.Shareholder4 = !string.IsNullOrEmpty(theDataReader["Shareholder4Number"].ToString()) 
                                                            ? UserManager.GetUser((string)(theDataReader["Shareholder4Number"])) 
                                                            : new Shareholder();
                    
                }
                theDataReader.Close();

                ClubBaistConnection.Close();

            }

            return standingteetimerequest;
        }

        
        public bool AddStandingTeeTimeRequest(StandingTeeTimeRequest standingTeeTime)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "AddStandingTeeTime";

            //dayofweek
            SqlParameter day = new SqlParameter();
            day.ParameterName = "@dayofweek";

            day.SqlDbType = SqlDbType.VarChar;
            day.Value = standingTeeTime.DayofWeek;
            day.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(day);

            //RequestedTeeTime
            SqlParameter time = new SqlParameter();
            time.ParameterName = "@reqTime";

            time.SqlDbType = SqlDbType.VarChar;
            time.Value = standingTeeTime.RequestedTime;
            time.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(time);

            //RequestedStartDate
            SqlParameter sdate = new SqlParameter();
            sdate.ParameterName = "@reqStartDate";

            sdate.SqlDbType = SqlDbType.VarChar;
            sdate.Value = standingTeeTime.RequestedStartDate;
            sdate.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(sdate);

            //RequestedEndDate
            SqlParameter edate = new SqlParameter();
            edate.ParameterName = "@reqEndDate";

            edate.SqlDbType = SqlDbType.VarChar;
            edate.Value = standingTeeTime.RequestedEndDate;
            edate.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(edate);

            //Shareholder1Number
            SqlParameter s1n = new SqlParameter();
            s1n.ParameterName = "@Shareholder1";

            s1n.SqlDbType = SqlDbType.VarChar;
            s1n.Value = standingTeeTime.Shareholder1.Id;
            s1n.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(s1n);

            //Shareholder2Number
            SqlParameter s2n = new SqlParameter();
            s2n.ParameterName = "@Shareholder2";

            s2n.SqlDbType = SqlDbType.VarChar;
            s2n.Value = standingTeeTime.Shareholder2.Id;
            s2n.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(s2n);

            //Shareholder3Number
            SqlParameter s3n = new SqlParameter();
            s3n.ParameterName = "@Shareholder3";

            s3n.SqlDbType = SqlDbType.VarChar;
            s3n.Value = standingTeeTime.Shareholder3.Id;
            s3n.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(s3n);

            //Shareholder4Number
            SqlParameter s4n = new SqlParameter();
            s4n.ParameterName = "@Shareholder4";

            s4n.SqlDbType = SqlDbType.VarChar;
            s4n.Value = standingTeeTime.Shareholder4.Id;
            s4n.Direction = ParameterDirection.Input;
            
            thecommand.Parameters.Add(s4n);
            
            //Booker
            SqlParameter booker = new SqlParameter();
            booker.ParameterName = "@booker";

            booker.SqlDbType = SqlDbType.VarChar;
            booker.Value = standingTeeTime.BookerNumber;
            booker.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(booker);

            ClubBaistConnection.Open();

            int rowsaffected = thecommand.ExecuteNonQuery();

            if (rowsaffected >= 1)
            {
                confirmation = true;
            }
            else
            {
                confirmation = false;
            }

            ClubBaistConnection.Close();

            return confirmation;
        }
        
        public bool RemoveStandingTeeTimeRequest(StandingTeeTimeRequest standingTeeTime)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "AddStandingTeeTime";

            //dayofweek
            SqlParameter day = new SqlParameter();
            day.ParameterName = "@dayofweek";

            day.SqlDbType = SqlDbType.VarChar;
            day.Value = standingTeeTime.DayofWeek;
            day.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(day);

            //RequestedTeeTime
            SqlParameter time = new SqlParameter();
            time.ParameterName = "@reqTime";

            time.SqlDbType = SqlDbType.VarChar;
            time.Value = standingTeeTime.RequestedTime;
            time.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(time);

            //RequestedStartDate
            SqlParameter sdate = new SqlParameter();
            sdate.ParameterName = "@reqStartDate";

            sdate.SqlDbType = SqlDbType.VarChar;
            sdate.Value = standingTeeTime.RequestedStartDate;
            sdate.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(sdate);

            //RequestedEndDate
            SqlParameter edate = new SqlParameter();
            edate.ParameterName = "@reqEndDate";

            edate.SqlDbType = SqlDbType.VarChar;
            edate.Value = standingTeeTime.RequestedEndDate;
            edate.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(edate);

            //Shareholder1Number
            SqlParameter s1n = new SqlParameter();
            s1n.ParameterName = "@Shareholder1";

            s1n.SqlDbType = SqlDbType.VarChar;
            s1n.Value = " ";
            s1n.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(s1n);

            //Shareholder2Number
            SqlParameter s2n = new SqlParameter();
            s2n.ParameterName = "@Shareholder2";

            s2n.SqlDbType = SqlDbType.VarChar;
            s2n.Value = " ";
            s2n.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(s2n);

            //Shareholder3Number
            SqlParameter s3n = new SqlParameter();
            s3n.ParameterName = "@Shareholder3";

            s3n.SqlDbType = SqlDbType.VarChar;
            s3n.Value = " ";
            s3n.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(s3n);

            //Shareholder4Number
            SqlParameter s4n = new SqlParameter();
            s4n.ParameterName = "@Shareholder4";

            s4n.SqlDbType = SqlDbType.VarChar;
            s4n.Value = " ";
            s4n.Direction = ParameterDirection.Input;
            
            thecommand.Parameters.Add(s4n);
            
            //Booker
            SqlParameter booker = new SqlParameter();
            booker.ParameterName = "@booker";

            booker.SqlDbType = SqlDbType.VarChar;
            booker.Value = " ";
            booker.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(booker);

            ClubBaistConnection.Open();

            int rowsaffected = thecommand.ExecuteNonQuery();

            if (rowsaffected >= 1)
            {
                confirmation = true;
            }
            else
            {
                confirmation = false;
            }

            ClubBaistConnection.Close();

            return confirmation;
        }
    }
}
