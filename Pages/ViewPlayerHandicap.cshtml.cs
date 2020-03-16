using System;
using System.Collections.Generic;
using ClubBaistGolfManagement.Domain;
using ClubBaistGolfManagement.TechnicalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBaistGolfManagement.Pages
{
    [Authorize]
    public class ViewPlayerHandicap : PageModel
    {
        [TempData] public string Alert { get; set; }

        public AspNetUsers authenticatedUser { get; set; }
        public decimal PlayerHandicap { get; set; }
        
        CBS RequestDirector = new CBS();
        CBSUsers UserManager = new CBSUsers();
        public void OnGet()
        {
            authenticatedUser = UserManager.GetUserByEmail(User.Identity.Name);
            PlayerHandicap = RequestDirector.FindPlayerHandicap(authenticatedUser);
        }
    }
}