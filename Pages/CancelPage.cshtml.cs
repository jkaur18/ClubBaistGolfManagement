using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubBaistGolfManagement.Domain;
using ClubBaistGolfManagement.TechnicalServices;

namespace ClubBaistGolfManagement.Pages
{
    [BindProperties]
    public class CancelPage : PageModel
    {
        
        public string Player1 { get; set; }
        
        public string Player2 { get; set; }
        
        public string Player3 { get; set; }
        
        public string Player4 { get; set; }
        
        public AspNetUsers authenticatedUser { get; set; }
       
        /*public DateTime selectedtime { get; set; }
        [BindProperty]
        public DateTime selecteddate { get; set; }*/
        
        public bool Confirmation { get; set; }
        [TempData] public string Alert { get; set; }

        [BindProperty]
        public TeeTime selectedteetime { get; set; }

        CBS RequestDirector = new Domain.CBS();
        
        CBSUsers UserManager = new CBSUsers();
        
        public void OnGet()
        {           
            DateTime selectedtime = DateTime.Parse(Request.Query["time"]);
            DateTime selecteddate = DateTime.Parse(Request.Query["date"]);
            selectedteetime = RequestDirector.FindTeeTime(selecteddate, selectedtime);
            authenticatedUser = UserManager.GetUser(UserManager.GetUserIdFromEmail(User.Identity.Name));
        }       

        public ActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                DateTime selectedtime = DateTime.Parse(Request.Query["time"]);
                DateTime selecteddate = DateTime.Parse(Request.Query["date"]);
                selectedteetime.Date = selecteddate;
                selectedteetime.TimeSlot = selectedtime;
                
                Confirmation = RequestDirector.CancelTeeTime(selectedteetime);
                if (Confirmation)
                {
                    Alert = $"Selected Tee Time has been Cancelled!";

                    return RedirectToPage("ReserveTeeTime");
                }
            }
            return Page();
        }
    }
}