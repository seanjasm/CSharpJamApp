﻿using System;
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
            object result = TempData["result"];
            TempData.Remove("result");

            return View(result);
        }

        public ActionResult Simulate()
        {
            Team monstars = CSharpDbDAL.GetTeam(MONSTAR_OWNER_ID);
            AspNetUser currentUser = CSharpDbDAL.GetContextUser(User.Identity.Name);
            Team current = CSharpDbDAL.GetTeam(currentUser.Id);

            double monstarsSum = monstars.Players.Sum(player => player.Rating);
            double currentSum = current.Players.Sum(player => player.Rating);
            string result;

            if (currentSum > monstarsSum)
            {
                result = "You won!";
            }
            else if (currentSum < monstarsSum)
            {
                result = "You lost!";
            }
            else
            {
                result = "You tied...";
            }

            //TempData["result"] = new BattleResult(result);
            return RedirectToAction("Battle");
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

                Battle(HeadToHead);

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


        public void Battle(List<UserPlayer> players)
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