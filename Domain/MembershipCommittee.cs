using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public class MembershipCommittee : AspNetUsers
    {
        public string RoleType { get; set; }
    }
}
