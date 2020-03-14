using ClubBaistGolfManagement.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;



namespace ClubBaistGolfManagement.TechnicalServices
{
    public class MembershipApplications
    {
        public bool AddMembershipApplication(MembershipApplication NewMembershipAplication)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "RecordMembershipApplication";

            //LastName 
            SqlParameter lastname = new SqlParameter();
            lastname.ParameterName = "@lastname";

            lastname.SqlDbType = SqlDbType.VarChar;
            lastname.Value = NewMembershipAplication.LastName;
            lastname.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(lastname);

            //firstname
            SqlParameter firstname = new SqlParameter();
            firstname.ParameterName = "@firstname";

            firstname.SqlDbType = SqlDbType.VarChar;
            firstname.Value = NewMembershipAplication.FirstName;
            firstname.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(firstname);

            //address
            SqlParameter address = new SqlParameter();
            address.ParameterName = "@address";

            address.SqlDbType = SqlDbType.VarChar;
            address.Value = NewMembershipAplication.Address;
            address.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(address);

            //PostalCode
            SqlParameter postalcode = new SqlParameter();
            postalcode.ParameterName = "@postalcode";

            postalcode.SqlDbType = SqlDbType.VarChar;
            postalcode.Value = NewMembershipAplication.PostalCode;
            postalcode.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(postalcode);

            //city
            SqlParameter city = new SqlParameter();
            city.ParameterName = "@city";

            city.SqlDbType = SqlDbType.VarChar;
            city.Value = NewMembershipAplication.City;
            city.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(city);

            //dateofbirth
            SqlParameter dateofbirth = new SqlParameter();
            dateofbirth.ParameterName = "@dateofbirth";

            dateofbirth.SqlDbType = SqlDbType.Date;
            dateofbirth.Value = NewMembershipAplication.DateofBirth;
            dateofbirth.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(dateofbirth);
             
             //email
             SqlParameter email = new SqlParameter();
             email.ParameterName = "@email";

             email.SqlDbType = SqlDbType.VarChar;
             email.Value = NewMembershipAplication.Email;
             email.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(email);
             
            //phone
             SqlParameter phone = new SqlParameter();
             phone.ParameterName = "@phone";

             phone.SqlDbType = SqlDbType.VarChar;
             phone.Value = NewMembershipAplication.Phone;
             phone.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(phone);
             
            //alternatephone
             SqlParameter alternatephone = new SqlParameter();
             alternatephone.ParameterName = "@alternatephone";

             alternatephone.SqlDbType = SqlDbType.VarChar;
             alternatephone.Value = NewMembershipAplication.AlternatePhone;
             alternatephone.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(alternatephone);
            
             //occupation
             SqlParameter occupation = new SqlParameter();
             occupation.ParameterName = "@occupation";

             occupation.SqlDbType = SqlDbType.VarChar;
             occupation.Value = NewMembershipAplication.Occupation;
             occupation.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(occupation);

             //companyname
             SqlParameter companyname = new SqlParameter();
             companyname.ParameterName = "@companyname";

             companyname.SqlDbType = SqlDbType.VarChar;
             companyname.Value = NewMembershipAplication.CompanyName;
             companyname.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(companyname);

             //companyaddress
             SqlParameter companyaddress = new SqlParameter();
             companyaddress.ParameterName = "@companyaddress";

             companyaddress.SqlDbType = SqlDbType.VarChar;
             companyaddress.Value = NewMembershipAplication.CompanyAddress;
             companyaddress.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(companyaddress);
 
             //companypostalcode
             SqlParameter companypostalcode = new SqlParameter();
             companypostalcode.ParameterName = "@companypostalcode";

             companypostalcode.SqlDbType = SqlDbType.VarChar;
             companypostalcode.Value = NewMembershipAplication.CompanyPostalCode;
             companypostalcode.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(companypostalcode);
 
             //companycity
             SqlParameter companycity = new SqlParameter();
             companycity.ParameterName = "@companycity";

             companycity.SqlDbType = SqlDbType.VarChar;
             companycity.Value = NewMembershipAplication.CompanyCity;
             companycity.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(companycity);
             
             //companyphone
             SqlParameter companyphone = new SqlParameter();
             companyphone.ParameterName = "@companyphone";

             companyphone.SqlDbType = SqlDbType.VarChar;
             companyphone.Value = NewMembershipAplication.CompanyPhone;
             companyphone.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(companyphone);
 
             //shareholder1
             SqlParameter shareholder1 = new SqlParameter();
             shareholder1.ParameterName = "@shareholder1";

             shareholder1.SqlDbType = SqlDbType.VarChar;
             shareholder1.Value = NewMembershipAplication.Shareholder1;
             shareholder1.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(shareholder1);
 
             //shareholder2
             SqlParameter shareholder2 = new SqlParameter();
             shareholder2.ParameterName = "@shareholder2";

             shareholder2.SqlDbType = SqlDbType.VarChar;
             shareholder2.Value = NewMembershipAplication.Shareholder2;
             shareholder2.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(shareholder2);
 
             //membershiptype
             SqlParameter membershiptype = new SqlParameter();
             membershiptype.ParameterName = "@membershiptype";

             membershiptype.SqlDbType = SqlDbType.VarChar;
             membershiptype.Value = NewMembershipAplication.MembershipType;
             membershiptype.Direction = ParameterDirection.Input;

             thecommand.Parameters.Add(membershiptype);

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

        public List<MembershipApplication> FindOnHoldMembershipApplications()
        {
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";
            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetMembershipApplications";

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            List<MembershipApplication> membershipapplications = new List<MembershipApplication>();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    MembershipApplication newmember = new MembershipApplication();
                    newmember.MembershipApplicationId = int.Parse(theDataReader["MembershipApplicationId"].ToString());
                    newmember.LastName = theDataReader["FirstName"].ToString();
                    newmember.FirstName = theDataReader["LastName"].ToString();
                    newmember.MembershipType = theDataReader["MembershipType"].ToString();
                    newmember.ApplicationStatus = theDataReader["ApplicationStatus"].ToString();
                    
                    membershipapplications.Add(newmember);
                }
            }
            ClubBaistConnection.Close();

            return membershipapplications;
        }

        public MembershipApplication ViewMembershipApplicationDetails(int applicationID)
        {
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";
            
            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "GetMembershipApplicationDetails";

            SqlParameter applicationid = new SqlParameter();
            applicationid.ParameterName = "@applicationid";

            applicationid.SqlDbType = SqlDbType.VarChar;
            applicationid.Value = applicationID;
            applicationid.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(applicationid);

            ClubBaistConnection.Open();

            SqlDataReader theDataReader;
            theDataReader = thecommand.ExecuteReader();

            MembershipApplication chosenApplication = new MembershipApplication();

            if (theDataReader.HasRows)
            {
                while (theDataReader.Read())
                {
                    chosenApplication.MembershipApplicationId = int.Parse(theDataReader["MembershipApplicationId"].ToString());
                    chosenApplication.LastName = theDataReader["LastName"].ToString();
                    chosenApplication.FirstName = theDataReader["FirstName"].ToString();
                    chosenApplication.Address = theDataReader["Address"].ToString();
                    chosenApplication.PostalCode = theDataReader["PostalCode"].ToString();
                    chosenApplication.City = theDataReader["City"].ToString();
                    chosenApplication.DateofBirth = DateTime.Parse(theDataReader["DateOfBirth"].ToString());
                    chosenApplication.Phone = theDataReader["Phone"].ToString();
                    chosenApplication.AlternatePhone = theDataReader["AlternatePhone"].ToString();
                    chosenApplication.Email = theDataReader["Email"].ToString();
                    chosenApplication.Occupation = theDataReader["Occupation"].ToString();
                    chosenApplication.CompanyName = theDataReader["CompanyName"].ToString();
                    chosenApplication.CompanyAddress = theDataReader["CompanyAddress"].ToString();
                    chosenApplication.CompanyPostalCode = theDataReader["CompanyPostalCode"].ToString();
                    chosenApplication.CompanyCity = theDataReader["CompanyCity"].ToString();
                    chosenApplication.CompanyPhone = theDataReader["CompanyPhone"].ToString();
                    chosenApplication.Shareholder1 = theDataReader["Shareholder1"].ToString();
                    chosenApplication.Shareholder2 = theDataReader["Shareholder2"].ToString();
                    chosenApplication.MembershipType = theDataReader["MembershipType"].ToString();
                }
            }
            ClubBaistConnection.Close();

            return chosenApplication;
        }

        public bool ApproveMembershipApplication(MembershipApplication newmMembershipApplication)
        {
            bool confirmation;

            newmMembershipApplication.NewMemberID = Guid.NewGuid().ToString();
            var passwordHasher = new PasswordHasher<string>();
            var hashedPassword = passwordHasher.HashPassword(newmMembershipApplication.Email, "Baist123$");
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(newmMembershipApplication.Email, hashedPassword, "Baist123$");
            
            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "CreateMemberAccount";

            //newmemberid
            SqlParameter newmemberid = new SqlParameter();
            newmemberid.ParameterName = "@newid";

            newmemberid.SqlDbType = SqlDbType.VarChar;
            newmemberid.Value = newmMembershipApplication.NewMemberID;
            newmemberid.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(newmemberid);

            //username
            SqlParameter username = new SqlParameter();
            username.ParameterName = "@username";

            username.SqlDbType = SqlDbType.VarChar;
            username.Value = newmMembershipApplication.Email;
            username.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(username);

            //normalizedusername
            SqlParameter normalizedusername = new SqlParameter();
            normalizedusername.ParameterName = "@normalizedusername";

            normalizedusername.SqlDbType = SqlDbType.VarChar;
            normalizedusername.Value = newmMembershipApplication.Email.ToUpper();
            normalizedusername.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(normalizedusername);

            //passwordhash
            SqlParameter passwordhash = new SqlParameter();
            passwordhash.ParameterName = "@passwordhash";

            passwordhash.SqlDbType = SqlDbType.VarChar;
            passwordhash.Value = hashedPassword;
            passwordhash.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(passwordhash);

            //fullname
            SqlParameter fullname = new SqlParameter();
            fullname.ParameterName = "@fullname";

            fullname.SqlDbType = SqlDbType.VarChar;
            fullname.Value = newmMembershipApplication.FirstName+" "+ newmMembershipApplication.LastName;
            fullname.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(fullname);

            //membertype
            SqlParameter membertype = new SqlParameter();
            membertype.ParameterName = "@memberdescription";

            membertype.SqlDbType = SqlDbType.VarChar;
            membertype.Value = newmMembershipApplication.MembershipType;
            membertype.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(membertype);

            //membershipapplicationid
            SqlParameter membershipapplicationid = new SqlParameter();
            membershipapplicationid.ParameterName = "@membershipapplicationid";

            membershipapplicationid.SqlDbType = SqlDbType.VarChar;
            membershipapplicationid.Value = newmMembershipApplication.MembershipApplicationId;
            membershipapplicationid.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(membershipapplicationid);

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
        
        public bool WaitlistMembershipApplication(int applicationID)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "WaitlistMembershipApplication";

            //ApplicationID
            SqlParameter applicationid = new SqlParameter();
            applicationid.ParameterName = "@applicationid";

            applicationid.SqlDbType = SqlDbType.VarChar;
            applicationid.Value = applicationID;
            applicationid.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(applicationid);

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
        
        public bool CancelMembershipApplication(int applicationID)
        {
            bool confirmation;

            SqlConnection ClubBaistConnection = new SqlConnection();
            ClubBaistConnection.ConnectionString = @"Data Source= (LocalDB)\MSSQLLocalDB; 
                                  Initial Catalog = aspnet-ClubBaistGolfManagement-53bc9b9d-9d6a-45d4-8429-2a2761773502;
                                                     Integrated Security = True; MultipleActiveResultSets=True";

            SqlCommand thecommand = new SqlCommand();
            thecommand.CommandType = CommandType.StoredProcedure;
            thecommand.Connection = ClubBaistConnection;
            thecommand.CommandText = "CancelMembershipApplication";

            //ApplicationID
            SqlParameter applicationid = new SqlParameter();
            applicationid.ParameterName = "@applicationid";

            applicationid.SqlDbType = SqlDbType.VarChar;
            applicationid.Value = applicationID;
            applicationid.Direction = ParameterDirection.Input;

            thecommand.Parameters.Add(applicationid);

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