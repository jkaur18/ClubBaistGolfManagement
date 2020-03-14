using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClubBaistGolfManagement.Domain
{
    public class MemberAccount
    {
        public string MemberId { get; set; }
        public List<AccountEntry> AccountEntries { get; set; }
        public decimal Balance { get; set; }  
    }
}