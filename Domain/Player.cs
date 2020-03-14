using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public abstract class Player : AspNetUsers
    {       
        public abstract string RoleType { get; set; }
    }
}
