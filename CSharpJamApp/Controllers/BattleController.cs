using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpJamApp.Models;

namespace CSharpJamApp.Controllers
{
    [Authorize]
    public class BattleController : Controller
    {

        private const string MONSTAR_OWNER_ID = "5b450305-83f7-4153-a08f-5dca9264555e";
        // GET: Battle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Monstars()
        {
            Team team = CSharpDbDAL.GetTeam(MONSTAR_OWNER_ID);
            List<Player> players = team.Players.ToList();

            return View(players);
        }

        public ActionResult Battle()
        {
            TempData["Message"] = "";
            TempData["Monstars"] = CSharpDbDAL.GetTeamAsUserPlayer(MONSTAR_OWNER_ID);
            AspNetUser user = CSharpDbDAL.GetContextUser(User.Identity.Name);
            TempData["MyTeam"] = CSharpDbDAL.GetTeamAsUserPlayer(user.Id);
            return View();
        }

        public ActionResult Simulate(string mode="easy")
        {
            AspNetUser user = CSharpDbDAL.GetContextUser(User.Identity.Name);

            if (TempData["Monstars"] is null)
            {
                //Generate monstars based on Easy, Normal, or Hard
                TempData["Monstars"] = GenerateMonstarByMode(mode);
            }

            if (TempData["MyTeam"] is null)
            {                
                TempData["MyTeam"] = CSharpDbDAL.GetTeamAsUserPlayer(user.Id);
            }

            List<UserPlayer> myTeam = (List<UserPlayer>)TempData["MyTeam"];

            if (myTeam.Count < 5)
            {
                string noun = (5 - myTeam.Count) > 1 ? "players" : "player";
                TempData["Message"] = $"You need {5 - myTeam.Count} more {noun} before you battle the Monstars";
                return View("Battle");
            }

            var match = Arena();

            CSharpDbDAL.UpdateTeamStats(user.Id, match.Item1);            

            ViewBag.Message = match.Item2;

            return View();
        }

        private List<UserPlayer> GenerateMonstarByMode(string mode)
        {
            List<UserPlayer> monstars = CSharpDbDAL.GetTeamAsUserPlayer(MONSTAR_OWNER_ID);
           
           //Default is database set attributes
            switch (mode)
            {
                case "easy":
                        SetModeAttributes(monstars, PlayerFactory.GetPlayer(PlayerType.Weak));
                    break;
                case "normal":
                    SetModeAttributes(monstars, PlayerFactory.GetPlayer(PlayerType.Normal));
                    break;
            }
            return monstars;
        }

        //Assign difficulty attributes to the Monstars
        private void SetModeAttributes(List<UserPlayer> monstars, UserPlayer modeSpecific)
        {
            foreach (UserPlayer original in monstars)
            {
                original.Aggression = modeSpecific.Aggression;
                original.Agility = modeSpecific.Agility;
                original.Endurance = modeSpecific.Endurance;
                original.Humor = modeSpecific.Humor;
                original.Strength = modeSpecific.Strength;
                original.TeamWork = modeSpecific.TeamWork;
                original.Skill = modeSpecific.Skill;
            }            
        }

        public Tuple<bool, string> Arena()
        {
            AspNetUser currentUser = CSharpDbDAL.GetContextUser(User.Identity.Name);

            List<UserPlayer> monstarsDown = new List<UserPlayer>();
            List<UserPlayer> monstars = CSharpDbDAL.GetTeamAsUserPlayer(MONSTAR_OWNER_ID);

            List<UserPlayer> myTeam = CSharpDbDAL.GetTeamAsUserPlayer(currentUser.Id);
            List<UserPlayer> myTeamDown = new List<UserPlayer>();

            List<UserPlayer> HeadToHead = new List<UserPlayer>();

            while (myTeam.Count >= 1 && monstars.Count >= 1)
            {
                //Players are picked at random from each team to go against each other
                int myTeamIndex = new Random().Next(0, myTeam.Count);
                int monstarIndex = new Random().Next(0, monstars.Count);

                HeadToHead = new List<UserPlayer>
                {
                    myTeam[myTeamIndex],
                    monstars[monstarIndex]
                };

                //One player from each team goes against each other
                MatchUp(HeadToHead);

                //If my team player has no health
                if (HeadToHead[0].Health <= 0)
                {
                    RemoveDownedPlayer(myTeam, myTeamDown, myTeamIndex);
                }

                //If monstar player is downed
                if (HeadToHead[1].Health <= 0)
                {
                    RemoveDownedPlayer(monstars, monstarsDown, monstarIndex);
                }

            }
                     
            if (myTeam.Count >= 1)
            {
                return Tuple.Create(true, $"You won {myTeam.Count}-{monstars.Count}");
            }

            string message = $"The Monstars won {monstars.Count}-{myTeam.Count}";

            //Tuple to return win as bool and a custom message
            return Tuple.Create(false, message);
        }

        //When a player has no health remaining, it is added to downed list and removed from 
        //Active player list
        private void RemoveDownedPlayer(List<UserPlayer> team, List<UserPlayer> downList, int index)
        {            
            downList.Add(team[index]);
            team.RemoveAt(index);
        }

        //Used a random to decide which player draws first blood
        //One player from each team is passed in a list        
        public void MatchUp(List<UserPlayer> players)
        {
            //The index of the player that draws first blood is decided here
            int whoGoesFirst = new Random().Next(0, 2);
            int whoGoesNext = whoGoesFirst > 0 ? 0 : 1;
            int damage = 0;

            damage = Convert.ToInt32(players[whoGoesFirst].Attack(players[whoGoesNext]));

            players[whoGoesNext].Injured = MarkAsInjured(players[whoGoesNext], damage);

            players[whoGoesNext].Health -= damage;

            //Checks if the attecked player has health remaining
            //If so, then attack player
            if (players[whoGoesNext].Health > 0)
            {
                damage = Convert.ToInt32(players[whoGoesNext].Attack(players[whoGoesFirst]));
                players[whoGoesFirst].Injured = MarkAsInjured(players[whoGoesFirst], damage);
                players[whoGoesFirst].Health -= damage;
            }
        }

        //Player gets injured if health falls below 50, next damage is greater than 8
        //and random agility is divisible by 3
        private bool MarkAsInjured(UserPlayer player, int damage)
        {
            if (player.Health < 50 && damage > 8 
                && new Random(DateTime.Now.Millisecond).Next(0,
                Convert.ToInt32(player.Agility)) % 3 == 0)
            {
                return true;
            }
            return false;
        }

    }
}