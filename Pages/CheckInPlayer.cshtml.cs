using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaistGolfManagement.Pages
{
    [BindProperties]
    public class CheckInPlayerModel : PageModel
    {
        public bool player1checked { get;set; }
        public bool player2checked { get; set; }
        public bool player3checked { get; set; }
        public bool player4checked { get; set; }
        public bool Confirmation { get; set; }
        [TempData] public string Alert { get; set; }

        public Domain.TeeTime selectedteetime { get; set; }

        Domain.CBS RequestDirector = new Domain.CBS();
        public void OnGet()
        {           
            DateTime selectedtime = DateTime.Parse(Request.Query["time"]);
            DateTime selecteddate = DateTime.Parse(Request.Query["date"]);
            selectedteetime = RequestDirector.FindTeeTime(selecteddate, selectedtime);
            player1checked = selectedteetime.Player1CheckedIn;
            player2checked = selectedteetime.Player2CheckedIn;
            player3checked = selectedteetime.Player3CheckedIn;
            player4checked = selectedteetime.Player4CheckedIn;
        }       

        public ActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                DateTime selectedtime = DateTime.Parse(Request.Query["time"]);
                DateTime selecteddate = DateTime.Parse(Request.Query["date"]);
                selectedteetime.Date = selecteddate;
                selectedteetime.TimeSlot = selectedtime;
                selectedteetime.Player1CheckedIn = player1checked;
                selectedteetime.Player2CheckedIn = player2checked;
                selectedteetime.Player3CheckedIn = player3checked;
                selectedteetime.Player4CheckedIn = player4checked;

                Confirmation = RequestDirector.CheckInPlayer(selectedteetime);
                if (Confirmation)
                {
                    Alert = $"Selected Players are checked in!";

                    return RedirectToPage("ReserveTeeTime");
                }
            }
            return Page();     

        }
    }
}