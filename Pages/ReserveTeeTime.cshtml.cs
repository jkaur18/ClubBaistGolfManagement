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
    [Authorize]
    [BindProperties]
    // TODO: Disable Cancel button if nobody is reserved
    // TODO: Disable Reserve button if all 4 spots are reserved
    // TODO: Don't allow same golfer to book in themselves again on a teetime
    // TODO: Ask Antonio if all teetimes in an hour going vertically should have unique users
    public class ReserveTeeTimeModel : PageModel
    {
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime TimeSlot { get; set; }

        public string player1Name { get; set; }

        public string player2Name { get; set; }

        public string player3Name { get; set; }

        public string player4Name { get; set; }

        [TempData] public string Alert { get; set; }

        public AspNetUsers authenticatedUser { get; set; }

        public List<Domain.TeeTime> dailyteesheet { get; set; }

        public bool Confirmation { get; set; }        

        public List<DateTime> Week { get; set; }

        public List<DateTime> ThisWeek = new List<DateTime>();

        public TeeTime chosenteetime;
        public DateTime chosenDate { get; set; }
        public DateTime chosenTime { get; set; }
        public void OnGet()
        {
            getDatesForThisWeek();
            authenticatedUser = UserManager.GetUser(UserManager.GetUserIdFromEmail(User.Identity.Name));
        }

        private void getDatesForThisWeek()
        {
            for (int i = 0; i < 7; i++)
            {
                ThisWeek.Add(DateTime.Now.AddDays(i));
            }
            Week = ThisWeek;
        }

        CBS RequestDirector = new CBS();
        CBSUsers UserManager = new CBSUsers();

        public void OnPostFind()
        {
            getDatesForThisWeek();
            chosenDate = DateTime.Parse(Request.Query["date"]);
            
            authenticatedUser = UserManager.GetUser(UserManager.GetUserIdFromEmail(User.Identity.Name));
            dailyteesheet = RequestDirector.ViewDailyTeeSheet(chosenDate, authenticatedUser);
        }
        public ActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            getDatesForThisWeek();
            chosenTime = DateTime.Parse(Request.Query["time"]);
            chosenDate = DateTime.Parse(Request.Query["date"]);
            chosenteetime = RequestDirector.FindTeeTime(chosenDate,chosenTime);
            
            // Assign the booker to TeeTime
            if (chosenteetime.BookerNumber == "" || chosenteetime.BookerNumber == " " ||
                chosenteetime.BookerNumber == null)
            {
                chosenteetime.BookerNumber = UserManager.GetUserIdFromEmail(User.Identity.Name);
            }
              
            if (chosenteetime.Player1 != null && player1Name != null)
            {
                chosenteetime.Player1 = (Player) UserManager.GetUser(UserManager.GetUserId(player1Name));
            }
            if (chosenteetime.Player2 !=null && player2Name != null)
            {
                chosenteetime.Player2 = (Player)UserManager.GetUser(UserManager.GetUserId(player2Name));
            }
            if (chosenteetime.Player3 != null && player3Name != null) 
            {
                chosenteetime.Player3 = (Player)UserManager.GetUser(UserManager.GetUserId(player3Name));
            }
            if (chosenteetime.Player4 != null && player4Name != null)
            {
                chosenteetime.Player4 = (Player)UserManager.GetUser(UserManager.GetUserId(player4Name));
            }
            
            Confirmation = RequestDirector.ReserveTeeTime(chosenteetime);

            if (Confirmation)
            {
                Alert = $"Tee Time reserved successfully!";
            }
            return Page();
        }
    }
}