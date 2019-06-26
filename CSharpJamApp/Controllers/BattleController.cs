using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpJamApp.Models;

namespace CSharpJamApp.Controllers
{
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

        public ActionResult Simulate()
        {
            if (TempData["Monstars"] is null)
            {
                TempData["Monstars"] = CSharpDbDAL.GetTeamAsUserPlayer(MONSTAR_OWNER_ID);
            }

            if (TempData["MyTeam"] is null)
            {
                AspNetUser user = CSharpDbDAL.GetContextUser(User.Identity.Name);
                TempData["MyTeam"] = CSharpDbDAL.GetTeamAsUserPlayer(user.Id);
            }

            List<UserPlayer> myTeam = (List<UserPlayer>)TempData["MyTeam"];

            if(myTeam.Count < 5)
            {
                string noun = (5 - myTeam.Count) > 1 ? "players" : "player";
                TempData["Message"] = $"You need {5 - myTeam.Count} more {noun} before you battle the Monstars";
                return View("Battle");
            }

            return View();
        }

        public void Arena()
        {
            AspNetUser currentUser = CSharpDbDAL.GetContextUser(User.Identity.Name);

            List<UserPlayer> monstarsDown = new List<UserPlayer>();
            List<UserPlayer> monstars = CSharpDbDAL.GetTeamAsUserPlayer(MONSTAR_OWNER_ID);

            List<UserPlayer> myTeam = CSharpDbDAL.GetTeamAsUserPlayer(currentUser.Id);
            List<UserPlayer> myTeamDown = new List<UserPlayer>();

            List<UserPlayer> HeadToHead = new List<UserPlayer>();

            while (myTeam.Count >= 1 && monstars.Count >= 1)
            {
                int myTeamIndex = new Random().Next(0, myTeam.Count);
                int monstarIndex = new Random().Next(0, monstars.Count);

                HeadToHead = new List<UserPlayer>
                {
                    myTeam[myTeamIndex],
                    monstars[monstarIndex]
                };

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
        }

        private void RemoveDownedPlayer(List<UserPlayer> team, List<UserPlayer> downList, int index)
        {
            downList.Add(team[index]);
            team.RemoveAt(index);
        }


        public void MatchUp(List<UserPlayer> players)
        {
            int whoGoesFirst = new Random().Next(0, 2);
            int whoGoesNext = whoGoesFirst > 0 ? 0 : 1;
            int damage = 0;

            damage = Convert.ToInt32(players[whoGoesFirst].Attack(players[whoGoesNext]));

            players[whoGoesNext].Injured = MarkAsInjured(players[whoGoesNext], damage);

            players[whoGoesNext].Health -= damage;

            if (players[whoGoesFirst].Health > 0)
            {
                damage = Convert.ToInt32(players[whoGoesNext].Attack(players[whoGoesFirst]));
                players[whoGoesFirst].Injured = MarkAsInjured(players[whoGoesFirst], damage);
                players[whoGoesFirst].Health -= damage;
            }
        }

        private bool MarkAsInjured(UserPlayer player, int damage)
        {
            if (player.Health < 50 && damage > 8)
            {
                return true;
            }
            return false;
        }

    }
}