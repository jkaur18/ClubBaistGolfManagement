using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubBaistGolfManagement.Domain;
using ClubBaistGolfManagement.TechnicalServices;
using Microsoft.AspNetCore.Authorization;

namespace ClubBaistGolfManagement.Pages
{
    [Authorize(Roles = "Shareholder,ShareholderSpouse")]
    [BindProperties]
    public class ReserveStandingTeeTimeModel : PageModel
    {
        [TempData] public string Alert { get; set; }
        public string Shareholder1 { get; set; }
        public string Shareholder2 { get; set; }
        public string Shareholder3 { get; set; }
        public string Shareholder4 { get; set; }
        public DateTime RequestedStartDate { get; set; }
        public DateTime RequestedEndDate { get; set; }
        public DateTime TimeSlot { get; set; }
        public string Day { get; set; }

        public bool Confirmation;

        List<StandingTeeTimeRequest> StandingTeeTimeRequests = new List<StandingTeeTimeRequest>();

        StandingTeeTimeRequest chosenstandingRequest = new StandingTeeTimeRequest();

        CBS RequestDirector = new CBS();
        CBSUsers UserManager = new CBSUsers();

        public ActionResult OnPostCancel()
        {
            
            if (ModelState.IsValid)
            {
                Day = Request.Query["day"].ToString();
                TimeSlot = DateTime.Parse(Request.Query["time"]);

                chosenstandingRequest =
                    RequestDirector.FindReservedStandingTeeTime(Day, TimeSlot, RequestedStartDate, RequestedEndDate);
                
                Confirmation = RequestDirector.CancelStandingTeeTimeRequest(chosenstandingRequest);

                if (Confirmation)
                {
                    Alert = $"Standing Tee Time Cancelled successfully!";

                    return RedirectToPage("SubmitStandingTeeTime");
                }
            }
            return Page();
        }
        
        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Day = Request.Query["day"].ToString();
                TimeSlot = DateTime.Parse(Request.Query["time"]);

                chosenstandingRequest.DayofWeek = Day;
                chosenstandingRequest.RequestedTime = TimeSlot;
                if (chosenstandingRequest.BookerNumber == "" || chosenstandingRequest.BookerNumber == " " ||
                    chosenstandingRequest.BookerNumber == null)
                {
                    chosenstandingRequest.BookerNumber = UserManager.GetUserIdFromEmail(User.Identity.Name);
                }
                 
                if (Shareholder1 != null)
                {
                    chosenstandingRequest.Shareholder1 = (Player) UserManager.GetUser(UserManager.GetUserId(Shareholder1));
                }
                if (Shareholder2 != null)
                {
                    chosenstandingRequest.Shareholder2 = (Player)UserManager.GetUser(UserManager.GetUserId(Shareholder2));
                }
                if (Shareholder3 != null) 
                {
                    chosenstandingRequest.Shareholder3 = (Player)UserManager.GetUser(UserManager.GetUserId(Shareholder3));
                }
                if (Shareholder4 != null)
                {
                    chosenstandingRequest.Shareholder4 = (Player)UserManager.GetUser(UserManager.GetUserId(Shareholder4));
                }
                chosenstandingRequest.RequestedStartDate = RequestedStartDate;
                chosenstandingRequest.RequestedEndDate = RequestedEndDate;

                Confirmation = RequestDirector.CreateStandingTeeTimeRequest(chosenstandingRequest);

                if (Confirmation)
                {
                    Alert = $"Standing Tee Time Submitted successfully!";

                    return RedirectToPage("SubmitStandingTeeTime");
                }
            }
            return Page();
        }

    }
}