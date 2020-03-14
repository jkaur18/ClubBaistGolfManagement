using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public class TeeTime
    {
        public DateTime Date { get; set; }

        public DateTime TimeSlot { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Player3 { get; set; }
        public Player Player4 { get; set; }        
        public string BookerNumber { get; set; }
        public bool Player1CheckedIn { get; set; }
        public bool Player2CheckedIn { get; set; }
        public bool Player3CheckedIn { get; set; }
        public bool Player4CheckedIn { get; set; }
    }
}
