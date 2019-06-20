using CSharpJamApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CSharpJamApp.Controllers
{
    public class HomeController : Controller
    {
        public static csharpjamDBEntities ORM = new csharpjamDBEntities();
        private const string MONSTAR_OWNER_ID = "5b450305-83f7-4153-a08f-5dca9264555e";

        public ActionResult Index()
        {
            // create their team
            AspNetUser currentUser = ORM.AspNetUsers.SingleOrDefault(u => u.Email == User.Identity.Name);

            if (currentUser != null)
            {
                Team currentUserTeam = CSharpDbDAL.GetTeam(currentUser.Id);

                if (currentUserTeam == null)
                {
                    currentUserTeam = new Team()
                    {
                        Name = $"Team {currentUser.Email.Split('@')[0]}",
                        OwnerId = currentUser.Id,
                        Location = "Earth"
                    };

                    if (!CSharpDbDAL.AddTeam(currentUserTeam))
                    {
                        Console.WriteLine("Failed to add team");
                    }
                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "C Sharp Jam";
            ViewBag.Message = "Anita";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Search
        [Authorize]
        public ActionResult Search()
        {
            AspNetUser currentUser = ORM.AspNetUsers.Single(user => user.Email == User.Identity.Name);
            Team currentUserTeam = CSharpDbDAL.GetTeam(currentUser.Id);

            return View(currentUserTeam.Players);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(string player, string team)
        {
            return View();
        }

        [Authorize]
        public ActionResult AddPlayer(string playerId)

        {
            AspNetUser currentUser = ORM.AspNetUsers.Single(user => user.Email == User.Identity.Name);
            Team currentUserTeam = CSharpDbDAL.GetTeam(currentUser.Id);

            if (currentUserTeam.Players.Count >= 5)
            {
                return RedirectToAction("Search");
            }

            Player alreadyExistingPlayer = currentUserTeam.Players.SingleOrDefault(p => p.Id == playerId);

            if (alreadyExistingPlayer != null)
            {
                return RedirectToAction("Search");
            }

            JObject data = CsharpJamApi.GetSportPlayerId(playerId);
            JArray playerDataArray = (JArray)data["players"];
            JObject playerData = (JObject)playerDataArray[0];
            UserPlayer userPlayer = new UserPlayer(playerData)
            {
                Team = currentUserTeam
            };

            Player player = new Player()
            {
                Id = userPlayer.Id,
                TeamId = userPlayer.TeamId,
                Name = userPlayer.Name,
                Skill = userPlayer.Skill,
                Agility = userPlayer.Agility,
                Strength = userPlayer.Strength,
                Endurance = userPlayer.Endurance,
                Aggression = userPlayer.Aggression,
                Humor = userPlayer.Humor,
                TeamWork = userPlayer.TeamWork,
                Rating = userPlayer.Rating,
                Height = userPlayer.Height,
                Weight = userPlayer.Weight,
                Description = userPlayer.Description,
                PictureUrl = userPlayer.PictureUrl,
                Team = userPlayer.Team
            };

            CSharpDbDAL.AddPlayers(player);

            return RedirectToAction("Search");
        }

        [Authorize]
        public ActionResult RemovePlayer(string playerId)
        {
            Player player = CSharpDbDAL.GetPlayer(playerId);

            if (player != null)
            {
                CSharpDbDAL.DeletePlayer(player);
            }

            return RedirectToAction("Search");
        }

        [Authorize]
        public ActionResult FindPlayer(string playerId)
        {

            {
                AspNetUser currentUser = ORM.AspNetUsers.Single(user => user.Email == User.Identity.Name);
                Team currentUserTeam = CSharpDbDAL.GetTeam(currentUser.Id);

                if (currentUserTeam.Players.Count >= 5)
                {
                    return RedirectToAction("Search");
                }

                Player alreadyExistingPlayer = currentUserTeam.Players.SingleOrDefault(p => p.Id == playerId);

                if (alreadyExistingPlayer != null)
                {
                    return RedirectToAction("Search");
                }

                JObject data = CsharpJamApi.GetSportPlayerId(playerId);
                JArray playerDataArray = (JArray)data["players"];
                JObject playerData = (JObject)playerDataArray[0];
                UserPlayer userPlayer = new UserPlayer(playerData)
                {
                    Team = currentUserTeam
                };

                Player player = new Player()
                {
                    Id = userPlayer.Id,
                    TeamId = userPlayer.TeamId,
                    Name = userPlayer.Name,
                    Skill = userPlayer.Skill,
                    Agility = userPlayer.Agility,
                    Strength = userPlayer.Strength,
                    Endurance = userPlayer.Endurance,
                    Aggression = userPlayer.Aggression,
                    Humor = userPlayer.Humor,
                    TeamWork = userPlayer.TeamWork,
                    Rating = userPlayer.Rating,
                    Height = userPlayer.Height,
                    Weight = userPlayer.Weight,
                    Description = userPlayer.Description,
                    PictureUrl = userPlayer.PictureUrl,
                    Team = userPlayer.Team
                };

                CSharpDbDAL.AddPlayers(player);

                return RedirectToAction("Search");
            }
        }

        //[Authorize]
        //public ActionResult RemovePlayer(string playerId)
        //{
        //    Player player = CSharpDbDAL.GetPlayer(playerId);

        //    if (player != null)
        //    {
        //        CSharpDbDAL.DeletePlayer(player);
        //    }

        //    return RedirectToAction("Search");
        //}

        //[Authorize]
        //public ActionResult FindPlayer(string name)
        //{

        //    JObject playerData = CsharpJamApi.GetSportSearchName(name);

        //    if (playerData.Count > 0)
        //    {
        //        List<UserPlayer> players = new List<UserPlayer>();

        //        foreach (JObject player in playerData["player"])
        //        {
        //            players.Add(new UserPlayer(player));
        //        }
        //        return View(players);
        //    }
        //    return RedirectToAction("Search");
        //}

        [Authorize]
        public ActionResult FindTeam(string teamName)
        {
            JObject teamData = CsharpJamApi.GetSportSearchTeam(teamName);

            if (teamData.Count > 0)
            {
                List<UserTeam> teams = new List<UserTeam>();

                foreach (JObject team in teamData["teams"])
                {
                    teams.Add(new UserTeam(team));
                }
                return View(teams);
            }
            return RedirectToAction("Search");
        }


        [Authorize]
        public ActionResult FindPlayersByTeam(string teamName)
        {
            JObject teamData = CsharpJamApi.GetSportPlayerByTeam(teamName);


            if (teamData.Count > 0)
            {
                List<UserPlayer> players = new List<UserPlayer>();

                foreach (JObject player in teamData["player"])
                {
                    players.Add(new UserPlayer(player));
                }
                return View(players);
            }
            //ViewBag.team = CSharpDbDAL.GetAllTeam();
            ViewBag.team = new List<SelectListItem>{ new SelectListItem{ Value = "1",
                Text="Anita"} };


            // ViewBag.team = new SelectList(CSharpDbDAL.GetAllTeam(),"Name");
            return RedirectToAction("Search");
        }

        [HttpGet]
        public ActionResult AddTeam()
        {


            if(Session["CurrentUser"] is null)
            {
                Session["CurrentUser"] = CSharpDbDAL.GetContextUser(User.Identity.Name);
            }
            AspNetUser user = (AspNetUser)Session["CurrentUser"];

            ViewBag.MyTeam = CSharpDbDAL.GetMyTeamAsSelectedListItem(user.Id);


            return View();
        }

        [HttpPost]
        public ActionResult AddTeam(Team team)
        {
            team.Draw = team.Lost = team.Win = 0;

            if (Session["CurrentUser"] is null)
            {
                Session["CurrentUser"] = CSharpDbDAL.GetContextUser(User.Identity.Name);
            }

            AspNetUser user = (AspNetUser)Session["CurrentUser"];
            team.OwnerId = user.Id;
            CSharpDbDAL.AddTeam(team);
            ViewBag.Message = $"Team {team.Name} added successfully!";

            return View();
        }

        public ActionResult AddFavorite(string PlayerId)
        {
           
            return RedirectToAction("Sports");
        }

        //need to fix things here

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
            AspNetUser currentUser = ORM.AspNetUsers.Single(user => user.Email == User.Identity.Name);
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

            TempData["result"] = new BattleResult(result);
            return RedirectToAction("Battle");
        }
    }
}

