using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.Pages
{
    public class ReviewMembershipApplication : PageModel
    {
        public List<MembershipApplication> newmembershipapplications { get; set; }

        private CBS RequestDirector = new CBS();
        
        [TempData] public string Alert { get; set; }
        
        public void OnGet()
        {
            newmembershipapplications = RequestDirector.ViewOnHoldMembershipApplications();
        }

    }
}