using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.Pages
{
    public class MembershipApplicationDetails : PageModel
    {
        public MembershipApplication chosenMembershipApplication { get; set; }
        
        [TempData] public string Alert { get; set; }
         
        CBS RequestDirector = new CBS();

        public bool confirmation;
        
        public void OnGet()
        {
            int applicationId = int.Parse(Request.Query["membershipid"]);

            chosenMembershipApplication = RequestDirector.ViewMembershipApplicationDetails(applicationId);
        }

        public ActionResult OnPostApprove()
        {
            if(ModelState.IsValid)
            {
                int applicationId = int.Parse(Request.Query["applicationid"]);
                chosenMembershipApplication = RequestDirector.ViewMembershipApplicationDetails(applicationId);
                confirmation = RequestDirector.ApproveMembershipApplication(chosenMembershipApplication);
                if (confirmation)
                {
                    Alert = $"Application for"+" "+ chosenMembershipApplication.FirstName +" "+ chosenMembershipApplication.LastName+" "+"has been approved!";

                    return RedirectToPage("ReviewMembershipApplication");
                }
            }
            return Page();
        }
        public ActionResult OnPostWaitlist()
        {
            if(ModelState.IsValid)
            {
                int applicationId = int.Parse(Request.Query["applicationid"]);
                chosenMembershipApplication = RequestDirector.ViewMembershipApplicationDetails(applicationId);

                confirmation = RequestDirector.WaitlistMembershipApplication(applicationId);
                if (confirmation)
                {
                    Alert = $"Application for"+" "+ chosenMembershipApplication.FirstName +" "+ chosenMembershipApplication.LastName+" " +"has been waitlisted!";

                    return RedirectToPage("ReviewMembershipApplication");
                }
            }
            return Page();
        }
        public ActionResult OnPostCancel()
        {
            if(ModelState.IsValid)
            {
                int applicationId = int.Parse(Request.Query["applicationid"]);
                chosenMembershipApplication = RequestDirector.ViewMembershipApplicationDetails(applicationId);
                confirmation = RequestDirector.CancelMembershipApplication(applicationId);
                if (confirmation)
                {
                    Alert = $"Application for"+" "+chosenMembershipApplication.FirstName +" "+ chosenMembershipApplication.LastName+" " +"has been cancelled!";

                    return RedirectToPage("ReviewMembershipApplication");
                }
            }
            return Page();
        }
    }
}