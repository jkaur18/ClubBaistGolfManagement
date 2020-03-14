using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubBaistGolfManagement.Domain;
using ClubBaistGolfManagement.TechnicalServices;
using Microsoft.AspNetCore.Authorization;

namespace ClubBaistGolfManagement.Pages
{
    [BindProperties]
    public class RecordMembershipApplication : PageModel
    {
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
        [TempData] public string Alert { get; set; }
        public MembershipApplication NewMembershipApplication { get; set; }    

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                NewMembershipApplication.LastName = LastName;
                NewMembershipApplication.FirstName = FirstName;
                NewMembershipApplication.Address = Address;
                NewMembershipApplication.PostalCode = PostalCode;
                NewMembershipApplication.City = City;
                NewMembershipApplication.DateofBirth = DateofBirth;
                NewMembershipApplication.Email = Email;
                NewMembershipApplication.Phone = Phone;
                NewMembershipApplication.AlternatePhone = AlternatePhone;
                NewMembershipApplication.Occupation = Occupation;
                NewMembershipApplication.CompanyName = CompanyName;
                NewMembershipApplication.CompanyAddress = CompanyAddress;
                NewMembershipApplication.CompanyPostalCode = CompanyPostalCode;
                NewMembershipApplication.CompanyCity = CompanyCity;
                NewMembershipApplication.CompanyPhone = CompanyPhone;
                NewMembershipApplication.Shareholder1 = Shareholder1;
                NewMembershipApplication.Shareholder2 = Shareholder2;
                NewMembershipApplication.MembershipType = MembershipType;

                bool confirmation;
                
                CBS RequestDirector = new CBS();
                confirmation = RequestDirector.RecordMembershipApplication(NewMembershipApplication);

                if (confirmation)
                {
                    Alert = $"Application has been Submitted Successfully!";
                }
                
            }
            return Page();
        }

    }
}