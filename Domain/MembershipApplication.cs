using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClubBaistGolfManagement.Domain
{
    public class MembershipApplication
    {
        public int MembershipApplicationId { get; set; }
        public string LastName { get; set; }    
        public string FirstName { get; set; }    
        public string Address { get; set; }    
        public string PostalCode { get; set; }    
        public string City { get; set; }    
        public DateTime DateofBirth { get; set; }    
        public string Email { get; set; }    
        public string Phone { get; set; }    
        public string AlternatePhone { get; set; }    
        public string Occupation { get; set; }    
        public string CompanyName { get; set; }    
        public string CompanyAddress { get; set; }    
        public string CompanyPostalCode { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPhone { get; set; }    
        public string Shareholder1 { get; set; }    
        public string Shareholder2 { get; set; }    
        public string MembershipType { get; set; } 
        public string ApplicationStatus { get; set; }
        public string NewMemberID { get; set; }
    }
}