using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBaistGolfManagement.Domain
{
    public class ScoreCard
    {
        public Player Player { get; set; }
        public string Course { get; set; }
        public DateTime Date { get; set; }
        public List<GolfRound> GolfRounds { get; set; }
    }
}