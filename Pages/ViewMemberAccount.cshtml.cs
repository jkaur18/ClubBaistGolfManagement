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
    [Authorize]
    public class ViewMemberAccount : PageModel
    {
        public MemberAccount SelectedMemberAccount { get; set; }
        public AspNetUsers authenticatedUser { get; set; }
        
        CBS RequestDirector = new CBS();
        CBSUsers UserManager = new CBSUsers();
        
        public void OnGet()
        {
            authenticatedUser = UserManager.GetUserByEmail(User.Identity.Name);
            SelectedMemberAccount = RequestDirector.ViewMemberAccount(authenticatedUser.Id);
        }
    }
}