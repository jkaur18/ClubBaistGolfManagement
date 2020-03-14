using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBaistGolfManagement.TechnicalServices;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.Domain
{
    public class CBS
    { 
        DailyTeeSheets DailyTeeSheetManager = new TechnicalServices.DailyTeeSheets();

        StandingTeeTimeRequests StandingTeeTimeRequestManager = new TechnicalServices.StandingTeeTimeRequests();
        
        MembershipApplications MembershipAplicationManager = new MembershipApplications();
        
        MemberAccounts MemberAccountManager = new MemberAccounts();
        
        PlayerScores PlayerScoreManager = new PlayerScores();

        bool confirmation;
        
        
        //teetimes
        public List<TeeTime> ViewDailyTeeSheet(DateTime date, AspNetUsers authenticatedUser)
        {
            List<TeeTime> RequestedTeeSheet;

            RequestedTeeSheet = DailyTeeSheetManager.GetDailyTeeSheet(date, authenticatedUser);

            return RequestedTeeSheet;
        }

        public TeeTime FindTeeTime(DateTime date, DateTime timeslot)
        {
            TeeTime chosenteetime;

            chosenteetime = DailyTeeSheetManager.GetTeeTime(date, timeslot);

            return chosenteetime;
        }

        public bool ReserveTeeTime(TeeTime chosenteetime)
        {
            confirmation = DailyTeeSheetManager.AddTeeTime(chosenteetime);

            return confirmation;
        }

        public List<Domain.StandingTeeTimeRequest> FindAvailableStandingTeeTimes(string dayofweek)
        {
            List<Domain.StandingTeeTimeRequest> standingrequest;

            standingrequest = StandingTeeTimeRequestManager.GetAvailableStandingTeeTimes(dayofweek);

            return standingrequest;
        }
        public Domain.StandingTeeTimeRequest FindReservedStandingTeeTime(string dayOfWeek, DateTime requestedTime, 
                                                                    DateTime requestedStartDate, DateTime requestedEndDate)
        {
            return StandingTeeTimeRequestManager.GetReservedStandingTeeTime(dayOfWeek,requestedTime,requestedStartDate,requestedEndDate);
        }
        
        public bool CreateStandingTeeTimeRequest(Domain.StandingTeeTimeRequest chosenstandingteetime)
        {
            confirmation = StandingTeeTimeRequestManager.AddStandingTeeTimeRequest(chosenstandingteetime);

            return confirmation;
        }

        public bool CancelStandingTeeTimeRequest(Domain.StandingTeeTimeRequest chosenstandingteetime)
        {
            confirmation = StandingTeeTimeRequestManager.RemoveStandingTeeTimeRequest(chosenstandingteetime);
            return confirmation;
        }

        public bool CancelTeeTime(TeeTime chosenteetime)
        {
            confirmation = DailyTeeSheetManager.CancelTeeTime(chosenteetime);

            return confirmation;
        }

        public bool CheckInPlayer(TeeTime chosenteetime )
        {
            confirmation = DailyTeeSheetManager.CheckInPlayer(chosenteetime);

                return confirmation;
        }

        //membership applications
        public bool RecordMembershipApplication(MembershipApplication NewMembershipApplication)
        {
            confirmation = MembershipAplicationManager.AddMembershipApplication(NewMembershipApplication);

            return confirmation;
        }
        public List<MembershipApplication> ViewOnHoldMembershipApplications()
        {
            List<MembershipApplication> allmembershipapplications;

            allmembershipapplications = MembershipAplicationManager.FindOnHoldMembershipApplications();

            return allmembershipapplications;
        }
        public MembershipApplication ViewMembershipApplicationDetails(int applicationID)
        {
            MembershipApplication selectedapplication = new MembershipApplication();

            selectedapplication = MembershipAplicationManager.ViewMembershipApplicationDetails(applicationID);

            return selectedapplication;
        }
        public bool ApproveMembershipApplication(MembershipApplication approvedMembershipApplication)
        {
            confirmation = MembershipAplicationManager.ApproveMembershipApplication(approvedMembershipApplication);

            return confirmation;
        }
        public bool WaitlistMembershipApplication(int applicationID)
        {
            confirmation = MembershipAplicationManager.WaitlistMembershipApplication(applicationID);

            return confirmation;
        }
        public bool CancelMembershipApplication(int applicationID)
        {
            confirmation = MembershipAplicationManager.CancelMembershipApplication(applicationID);

            return confirmation;
        }
        
        //member account
        public MemberAccount ViewMemberAccount(string MemberID)
        {
            MemberAccount chosenMemberAccount = new MemberAccount();

            chosenMemberAccount = MemberAccountManager.FindMemberAccount(MemberID);

            return chosenMemberAccount;
        }
        
        //player scores

        public bool AddPlayerScores(ScoreCard newScoreCard)
        {
            confirmation = PlayerScoreManager.AddPlayerScores(newScoreCard);
            return confirmation;
        }

        public decimal FindPlayerHandicap(AspNetUsers authenticatedUser)
        {
            decimal playerHandicap;

            playerHandicap = PlayerScoreManager.CalculatePlayerHandicap(authenticatedUser);

            return playerHandicap;
        }
        
    }
}
