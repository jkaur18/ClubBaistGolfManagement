using ClubBaistGolfManagement.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace ClubBaistGolfManagement.TechnicalServices
{
    public class MemberAccounts
    {
        public MemberAccount FindMemberAccount(string memberID)
        {
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";
            
            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetMemberAccount";

            SqlParameter memberid = new SqlParameter();
            memberid.ParameterName = "@memberid";

            memberid.SqlDbType = SqlDbType.VarChar;
            memberid.Value = memberID;
            memberid.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(memberid);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            MemberAccount chosenMemberAccount = new MemberAccount();
            List<AccountEntry> accountEntriesforMember = new List<AccountEntry>();
            
            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    AccountEntry accountEntry = new AccountEntry();
                    
                    chosenMemberAccount.MemberId = theDataReader["MemberID"].ToString();
                    chosenMemberAccount.Balance = Decimal.Parse(theDataReader["Balance"].ToString());
                    
                    accountEntry.WhenCharged = DateTime.Parse(theDataReader["WhenCharged"].ToString());
                    accountEntry.WhenMade = DateTime.Parse(theDataReader["WhenMade"].ToString());
                    accountEntry.Amount = Decimal.Parse(theDataReader["Amount"].ToString());
                    accountEntry.PaymentDescription = theDataReader["PaymentDescription"].ToString();
                    
                    accountEntriesforMember.Add(accountEntry);
                }
            }
            ClubBaistConnection.Close();
            chosenMemberAccount.AccountEntries = accountEntriesforMember;
            return chosenMemberAccount;
        }
    }
}