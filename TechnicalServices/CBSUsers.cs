using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.TechnicalServices
{
    public class CBSUsers
    {
        public string GetUserId(string username)
        {
            string userId = "";
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetUserIdByName";

            SqlParameter player = new SqlParameter();
            player.ParameterName = "@username";

            player.SqlDbType = SqlDbType.VarChar;
            player.Value = username;
            player.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();            

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    userId = theDataReader["Id"].ToString();
                }
                theDataReader.Close();

                ClubBaistConnection.Close();
            }
            return userId;
        }

        public string GetUserIdFromEmail(string email)
        {
            string userId = "";
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetUserIdFromEmail";

            SqlParameter player = new SqlParameter();
            player.ParameterName = "@email";

            player.SqlDbType = SqlDbType.VarChar;
            player.Value = email;
            player.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(player);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();            

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    userId = theDataReader["Id"].ToString();
                }
                theDataReader.Close();

                ClubBaistConnection.Close();
            }
            return userId;
        }

        public AspNetUsers GetUser(string userid)
        {
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetUser";

            SqlParameter user = new SqlParameter();
            user.ParameterName = "@userid";

            user.SqlDbType = SqlDbType.VarChar;
            user.Value = userid;
            user.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(user);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    switch (theDataReader["Role"])
                    {
                        case "Shareholder":
                            Shareholder shareholder = new Shareholder()
                            {
                                Id = userid,
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return shareholder;
                        case "Clerk":
                            Clerk clerk = new Clerk()
                            {
                                Id = userid,
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return clerk;
                        case "ProShop":
                            ProShop proshop = new ProShop()
                            {
                                Id = userid,
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return proshop;
                        case "MembershipCommittee":
                            MembershipCommittee membershipcommittee = new MembershipCommittee()
                            {
                                Id = userid,
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return membershipcommittee;
                        case "Associate":
                            Associate associate = new Associate()
                            {
                                Id = userid,
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return associate;
                        case "Intermediate":
                            Intermediate intermediate = new Intermediate()
                            {
                                Id = userid,
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return intermediate;
                        case "ShareholderSpouse":
                            ShareholderSpouse spouse = new ShareholderSpouse()
                            {
                                Id = userid,
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return spouse;
                        default:
                            return new AspNetUsers();
                    }
                }
                theDataReader.Close();

                ClubBaistConnection.Close();
            }
            return new AspNetUsers();
        }
        
        public AspNetUsers GetUserByEmail(string Email)
        {
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetUserByEmail";

            SqlParameter email = new SqlParameter();
            email.ParameterName = "@email";

            email.SqlDbType = SqlDbType.VarChar;
            email.Value = Email;
            email.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(email);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    switch (theDataReader["Role"])
                    {
                        case "Shareholder":
                            Shareholder shareholder = new Shareholder()
                            {
                                Id = theDataReader["Id"].ToString(),
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return shareholder;
                            /*return new Shareholder()
                            {
                                FullName = theDataReader["FullName"].ToString(),
                                Id = theDataReader["Id"].ToString()
                            };*/
                        case "Clerk":
                            Clerk clerk = new Clerk()
                            {
                                Id = theDataReader["Id"].ToString(),
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return clerk;
                        case "ProShop":
                            ProShop proshop = new ProShop()
                            {
                                Id = theDataReader["Id"].ToString(),
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return proshop;
                        case "MembershipCommittee":
                            MembershipCommittee membershipcommittee = new MembershipCommittee()
                            {
                                Id = theDataReader["Id"].ToString(),
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return membershipcommittee;
                        case "Associate":
                            Associate associate = new Associate()
                            {
                                Id = theDataReader["Id"].ToString(),
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return associate;
                        case "Intermediate":
                            Intermediate intermediate = new Intermediate()
                            {
                                Id = theDataReader["Id"].ToString(),
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return intermediate;
                        case "ShareholderSpouse":
                            ShareholderSpouse spouse = new ShareholderSpouse()
                            {
                                Id = theDataReader["Id"].ToString(),
                                FullName = theDataReader["FullName"].ToString()
                            };
                            return spouse;
                        /*default:
                            return new AspNetUsers();*/
                    }
                }
                theDataReader.Close();

                ClubBaistConnection.Close();
            }
            /*return new AspNetUsers();*/
            return null;
        }
    }
}
