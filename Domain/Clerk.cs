using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public class Clerk : AspNetUsers
    {
        public string RoleType { get; set; }
    }
}
