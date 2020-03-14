using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public class AccountEntry
    {
        public decimal Amount { get; set; }
        public DateTime WhenCharged { get; set; }
        public DateTime WhenMade { get; set; }
        public string PaymentDescription { get; set; }
    }
}