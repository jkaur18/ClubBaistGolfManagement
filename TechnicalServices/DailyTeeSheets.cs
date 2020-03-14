using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.TechnicalServices
{
    public class DailyTeeSheets
    {
        CBSUsers UserManager = new CBSUsers();
        public TeeTime GetTeeTime(DateTime RequestedDate, DateTime timeslot)
        {

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "FindTeeTime";

            SqlParameter date = new SqlParameter();
            date.ParameterName = "@date";

            date.SqlDbType = SqlDbType.DateTime;
            date.Value = RequestedDate;
            date.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(date);

            SqlParameter time = new SqlParameter();
            time.ParameterName = "@time";

            time.SqlDbType = SqlDbType.DateTime;
            time.Value = timeslot;
            time.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(time);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            TeeTime requestedteetime = new TeeTime();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    requestedteetime.Date = DateTime.Parse(theDataReader["Date"].ToString());
                    requestedteetime.TimeSlot = DateTime.Parse(theDataReader["TimeSlot"].ToString());
                    requestedteetime.Player1 = !DBNull.Value.Equals(theDataReader["Player1Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player1Number"]) : new Shareholder();
                    requestedteetime.Player2 = !DBNull.Value.Equals(theDataReader["Player2Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player2Number"]) : new Shareholder();
                    requestedteetime.Player3 = !DBNull.Value.Equals(theDataReader["Player3Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player3Number"]) : new Shareholder();
                    requestedteetime.Player4 = !DBNull.Value.Equals(theDataReader["Player4Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player4Number"]) : new Shareholder();
                    requestedteetime.BookerNumber = theDataReader["BookerNumber"].ToString();
                    requestedteetime.Player1CheckedIn = bool.Parse(theDataReader["Player1CheckedIn"].ToString());
                    requestedteetime.Player2CheckedIn = bool.Parse(theDataReader["Player2CheckedIn"].ToString());
                    requestedteetime.Player3CheckedIn = bool.Parse(theDataReader["Player3CheckedIn"].ToString());
                    requestedteetime.Player4CheckedIn = bool.Parse(theDataReader["Player4CheckedIn"].ToString());
                }
            }
            theDataReader.Close();

            ClubBaistConnection.Close();

            return requestedteetime;
        }
        public List<TeeTime> GetDailyTeeSheet(DateTime RequestedDate, AspNetUsers authenticatedUser)
        {
           
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetDailyTeeSheet";

            SqlParameter date = new SqlParameter();
            date.ParameterName = "@Date";

            date.SqlDbType = SqlDbType.DateTime;
            date.Value = RequestedDate;
            date.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(date);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            List<TeeTime> dailyteesheet = new List<TeeTime>();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    TeeTime teetime = new TeeTime
                    {
                        Date = DateTime.Parse(theDataReader["Date"].ToString()),
                        TimeSlot = DateTime.Parse(theDataReader["TimeSlot"].ToString()),
                        Player1 = !DBNull.Value.Equals(theDataReader["Player1Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player1Number"]) : new Shareholder(),
                        Player2 = !DBNull.Value.Equals(theDataReader["Player2Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player2Number"]) : new Shareholder(),
                        Player3 = !DBNull.Value.Equals(theDataReader["Player3Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player3Number"]) : new Shareholder(),
                        Player4 = !DBNull.Value.Equals(theDataReader["Player4Number"]) ? (Player)UserManager.GetUser((String)theDataReader["Player4Number"]) : new Shareholder(),
                    };

                    DateTime timeslot = teetime.TimeSlot;

                    if ((teetime.Date.DayOfWeek == DayOfWeek.Saturday) || (teetime.Date.DayOfWeek == DayOfWeek.Sunday))
                    {
                        if (timeslot >= authenticatedUser.WeekendRestrictedBefore)
                            dailyteesheet.Add(teetime);
                    }
                    else
                    {
                        if (!(authenticatedUser.WeekdayRestrictedBefore > timeslot
                            && authenticatedUser.WeekdayRestrictedAfter < timeslot))
                            dailyteesheet.Add(teetime);
                    }
                }
            }
            theDataReader.Close();

            ClubBaistConnection.Close();

            return dailyteesheet;
        }

        public bool AddTeeTime(Domain.TeeTime chosenteetime)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "AddTeeTime";

            //Date 
            SqlParameter date = new SqlParameter();
            date.ParameterName = "@Date";

            date.SqlDbType = SqlDbType.Date;
            date.Value = chosenteetime.Date.Date;
            date.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(date);

            //timeslot
            SqlParameter timeslot = new SqlParameter();
            timeslot.ParameterName = "@TimeSlot";

            timeslot.SqlDbType = SqlDbType.Time;
            timeslot.Value = chosenteetime.TimeSlot.TimeOfDay;
            timeslot.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(timeslot);

            //Player 1
            SqlParameter player1 = new SqlParameter();
            player1.ParameterName = "@player1";

            player1.SqlDbType = SqlDbType.VarChar;
            player1.Value = chosenteetime.Player1.Id;
            player1.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player1);

            //Player 2
            SqlParameter player2 = new SqlParameter();
            player2.ParameterName = "@player2";

            player2.SqlDbType = SqlDbType.VarChar;
            player2.Value = chosenteetime.Player2.Id;
            player2.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player2);

            //Player 3
            SqlParameter player3 = new SqlParameter();
            player3.ParameterName = "@player3";

            player3.SqlDbType = SqlDbType.VarChar;
            player3.Value = chosenteetime.Player3.Id;
            player3.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player3);

            //Player 4
            SqlParameter player4 = new SqlParameter();
            player4.ParameterName = "@player4";

            player4.SqlDbType = SqlDbType.VarChar;
            player4.Value = chosenteetime.Player4.Id;
            player4.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(player4);
             
             //Booker
             SqlParameter booker = new SqlParameter();
             booker.ParameterName = "@booker";

             booker.SqlDbType = SqlDbType.VarChar;
             booker.Value = chosenteetime.BookerNumber;
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

        public bool CancelTeeTime(Domain.TeeTime chosenteetime)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "CancelTeeTime";

            //Date 
            SqlParameter date = new SqlParameter();
            date.ParameterName = "@Date";

            date.SqlDbType = SqlDbType.Date;
            date.Value = chosenteetime.Date.Date;
            date.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(date);

            //timeslot
            SqlParameter timeslot = new SqlParameter();
            timeslot.ParameterName = "@TimeSlot";

            timeslot.SqlDbType = SqlDbType.Time;
            timeslot.Value = chosenteetime.TimeSlot.TimeOfDay;
            timeslot.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(timeslot);


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

        public bool CheckInPlayer( Domain.TeeTime selectedteetime)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "CheckInPlayer";

            //Date 
            SqlParameter date = new SqlParameter();
            date.ParameterName = "@date";

            date.SqlDbType = SqlDbType.Date;
            date.Value = selectedteetime.Date.Date;
            date.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(date);

            //timeslot
            SqlParameter timeslot = new SqlParameter();
            timeslot.ParameterName = "@time";

            timeslot.SqlDbType = SqlDbType.Time;
            timeslot.Value = selectedteetime.TimeSlot.TimeOfDay;
            timeslot.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(timeslot);

            //Check in Player 1
            
            SqlParameter player1checked = new SqlParameter();
            player1checked.ParameterName = "@player1status";

            player1checked.SqlDbType = SqlDbType.Bit;
            player1checked.Value = selectedteetime.Player1CheckedIn;
            player1checked.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player1checked);

            //Check in Player 2

            SqlParameter player2checked = new SqlParameter();
            player2checked.ParameterName = "@player2status";

            player2checked.SqlDbType = SqlDbType.Bit;
            player2checked.Value = selectedteetime.Player2CheckedIn;
            player2checked.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player2checked);

            //Check in Player 3

            SqlParameter player3checked = new SqlParameter();
            player3checked.ParameterName = "@player3status";

            player3checked.SqlDbType = SqlDbType.Bit;
            player3checked.Value = selectedteetime.Player3CheckedIn;
            player3checked.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player3checked);

            //Check in Player 4

            SqlParameter player4checked = new SqlParameter();
            player4checked.ParameterName = "@player4status";

            player4checked.SqlDbType = SqlDbType.Bit;
            player4checked.Value = selectedteetime.Player4CheckedIn;
            player4checked.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player4checked);

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
