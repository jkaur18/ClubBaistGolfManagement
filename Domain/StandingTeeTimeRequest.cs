using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public class StandingTeeTimeRequest
    {
        public string DayofWeek { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime RequestedStartDate { get; set; }
        public DateTime RequestedEndDate { get; set; }
        public AspNetUsers Shareholder1 { get; set; }
        public AspNetUsers Shareholder2 { get; set; }
        public AspNetUsers Shareholder3 { get; set; }
        public AspNetUsers Shareholder4 { get; set; }
        public string BookerNumber { get; set; }
    }
}
