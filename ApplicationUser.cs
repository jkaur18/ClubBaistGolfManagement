using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement
{
    public class ApplicationUser:IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }        
        [PersonalData]
        public string  MemberDescription { get; set; }


    }
}
