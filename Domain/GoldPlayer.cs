using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public abstract class GoldPlayer : Player
    {
        public string MembershipLevel = "Gold";
        public override DateTime WeekdayRestrictedBefore => new DateTime(1, 1, 1, 0, 0, 0);
        public override DateTime WeekdayRestrictedAfter => new DateTime(1, 1, 1, 0, 0, 0);
        public override DateTime WeekendRestrictedBefore => new DateTime(1, 1, 1, 0, 0, 0);

    }
}
