using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public abstract class SilverPlayer : Player
    {
        public string MembershipLevel = "Silver";
        public override DateTime WeekdayRestrictedBefore => new DateTime(1, 1, 1, 17, 29, 0);
        public override DateTime WeekdayRestrictedAfter => new DateTime(1, 1, 1, 14, 59, 0);
        public override DateTime WeekendRestrictedBefore => new DateTime(1, 1, 1, 10, 59, 0);
    }
}
