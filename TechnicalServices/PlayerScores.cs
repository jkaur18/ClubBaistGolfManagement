using ClubBaistGolfManagement.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.TechnicalServices
{
    public class PlayerScores
    {
        public bool AddPlayerScores(ScoreCard newScoreCard)
        {
            bool confirmation = false;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";
            foreach (var round in newScoreCard.GolfRounds)
            {
               SqlCommand thecommand = new SqlCommand();
                thecommand.CommandType = CommandType.StoredProcedure;
                thecommand.Connection = ClubBaistConnection;
                thecommand.CommandText = "AddPlayerScore";

                //playernumber 
                SqlParameter playernumber = new SqlParameter();
                playernumber.ParameterName = "@playernumber";

                playernumber.SqlDbType = SqlDbType.VarChar;
                playernumber.Value = newScoreCard.Player.Id;
                playernumber.Direction = ParameterDirection.Input;

                thecommand.Parameters.Add(playernumber);

                //coursename
                SqlParameter coursename = new SqlParameter();
                coursename.ParameterName = "@coursename";

                coursename.SqlDbType = SqlDbType.VarChar;
                coursename.Value = newScoreCard.Course;
                coursename.Direction = ParameterDirection.Input;

                thecommand.Parameters.Add(coursename);

                //date
                SqlParameter date = new SqlParameter();
                date.ParameterName = "@date";

                date.SqlDbType = SqlDbType.Date;
                date.Value = newScoreCard.Date;
                date.Direction = ParameterDirection.Input;

                thecommand.Parameters.Add(date);

                //hole
                SqlParameter hole = new SqlParameter();
                hole.ParameterName = "@hole";

                hole.SqlDbType = SqlDbType.Int;
                hole.Value = round.Hole;
                hole.Direction = ParameterDirection.Input;

                thecommand.Parameters.Add(hole);

                //score
                SqlParameter score = new SqlParameter();
                score.ParameterName = "@score";

                score.SqlDbType = SqlDbType.Int;
                score.Value = round.Score;
                score.Direction = ParameterDirection.Input;

                thecommand.Parameters.Add(score);

                //rating
                SqlParameter rating = new SqlParameter();
                rating.ParameterName = "@rating";

                rating.SqlDbType = SqlDbType.Decimal;
                rating.Value = round.Rating;
                rating.Direction = ParameterDirection.Input;

                thecommand.Parameters.Add(rating);

                //slope
                SqlParameter slope = new SqlParameter();
                slope.ParameterName = "@slope";

                slope.SqlDbType = SqlDbType.Decimal;
                slope.Value = round.Slope;
                slope.Direction = ParameterDirection.Input;

                thecommand.Parameters.Add(slope);

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
            }
            return confirmation;   
        }

        public decimal CalculatePlayerHandicap(AspNetUsers authenticateduser)
        {
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";
            
            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "ViewPlayerHandicap";

            SqlParameter playernumber = new SqlParameter();
            playernumber.ParameterName = "@playernumber";

            playernumber.SqlDbType = SqlDbType.VarChar;
            playernumber.Value = authenticateduser.Id;
            playernumber.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(playernumber);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            List<GolfRound> rounds = new List<GolfRound>();
            
            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    GolfRound selectedround = new GolfRound();

                    selectedround.Hole = int.Parse(theDataReader["Hole"].ToString());
                    selectedround.Score = int.Parse(theDataReader["Score"].ToString());
                    selectedround.Rating = decimal.Parse(theDataReader["Rating"].ToString());
                    selectedround.Slope = decimal.Parse(theDataReader["Slope"].ToString());
                    
                    rounds.Add(selectedround);
                }
            }
            ClubBaistConnection.Close();
            
            var handicapDifferentials = rounds.Select(round => ((round.Score - round.Rating) * 113) / round.Slope).ToList();
             
            var average = handicapDifferentials.Average();
            
            average *= (decimal)0.96;

            return Math.Truncate(100 * average) / 100;;
        }
    }
}