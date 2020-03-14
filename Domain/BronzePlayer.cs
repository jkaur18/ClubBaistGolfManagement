using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public abstract class BronzePlayer : Player
    {
        public string MembershipLevel = "Bronze";
        public override DateTime WeekdayRestrictedBefore => new DateTime(1, 1, 1, 17, 59, 0);
        public override DateTime WeekdayRestrictedAfter => new DateTime(1, 1, 1, 14, 59, 0);
        public override DateTime WeekendRestrictedBefore => new DateTime(1, 1, 1, 12, 59, 0);
    }
}
