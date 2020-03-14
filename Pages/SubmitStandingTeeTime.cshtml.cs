using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.Pages
{
    public class SubmitStandingTeeTimeModel : PageModel
    {
        public List<string> Week = new List<string>
        {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"
        };
        [BindProperty]
        public List<string> ThisWeek { get; set; }

        [TempData] public string Alert { get; set; }

        public string Day { get; set; }

        public List<StandingTeeTimeRequest> DailyStandingTeeTimeRequests { get; set; }

        public void OnGet()
        {
            ThisWeek = Week;
        }

        public ActionResult OnPost()
        {
            ThisWeek = Week;

            CBS RequestDirector = new CBS();

            Day = Request.Query["day"];

            DailyStandingTeeTimeRequests = RequestDirector.FindAvailableStandingTeeTimes(Day);

            return Page();
        }

    }
}