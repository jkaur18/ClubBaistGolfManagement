using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubBaistGolfManagement.Domain;
using ClubBaistGolfManagement.TechnicalServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace ClubBaistGolfManagement.Pages
{
    [Authorize]
    [BindProperties]
    public class AddPlayerScores : PageModel
    {
        public List<GolfRound> rounds { get; set; }
        
        public string Course { get; set; }
        
        public DateTime DayofGame { get; set; }
        
        [TempData] public string Alert { get; set; }

        public AspNetUsers authenticatedUser;
        
        ScoreCard newScoreCard = new ScoreCard();
        
        CBS RequestDirector = new CBS();
        CBSUsers UserManager = new CBSUsers();

        public bool confirmation;

        public void OnGet()
        {
            rounds = new List<GolfRound>();
            
            for (var i = 1; i <= 18; i++)
            {
                var oneRound = new GolfRound()
                {
                    Hole = i
                };
                rounds.Add(oneRound);
            }
            newScoreCard.GolfRounds = rounds;
        }

        public ActionResult OnPost()
        {
            authenticatedUser = UserManager.GetUserByEmail(User.Identity.Name);
            if (ModelState.IsValid)
            {
                newScoreCard.Course = Course;
                newScoreCard.Date = DayofGame;
                newScoreCard.GolfRounds = rounds;
                newScoreCard.Player = (Player) authenticatedUser;

                confirmation = RequestDirector.AddPlayerScores(newScoreCard);

                if (confirmation)
                {
                    Alert = $"Player Scores are added!";
                }
            }
            return Page();
        }
    }
}