using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClubBaistGolfManagement.Domain
{
    public class GolfRound
    {
        public int Hole { get; set; }
        public int Score { get; set; }
        public decimal Rating { get; set; }
        public decimal Slope { get; set; }
    }
}