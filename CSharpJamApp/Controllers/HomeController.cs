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

        public ActionResult Index()
        {
            // create their team
            AspNetUser currentUser = CSharpDbDAL.GetContextUser(User.Identity.Name);

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
        public ActionResult TeamManagement()
        {
            AspNetUser currentUser = CSharpDbDAL.GetContextUser(User.Identity.Name);
            Team team = CSharpDbDAL.GetTeam(currentUser.Id);            

            return View(team);
        }


        [Authorize]
        public ActionResult PlayerStats(string playerId)
        {
            AspNetUser currentUser = CSharpDbDAL.GetContextUser(User.Identity.Name);
            Player player = CSharpDbDAL.GetUserPlayer(currentUser.Id, playerId);

            return View(player);
        }

        [Authorize]
        [HttpPost]
        public ActionResult PlayerStats(Player player)
        {
            CSharpDbDAL.UpdatePlayer(player);
            ViewBag.Message = $"{player.Name} updated!";
            return View(player);
        }

        [Authorize]
        public ActionResult AddPlayer(string playerId)
        {
            AspNetUser currentUser = ORM.AspNetUsers.Single(user => user.Email == User.Identity.Name);
            Team currentUserTeam = CSharpDbDAL.GetTeam(currentUser.Id);

            if (currentUserTeam.Players.Count >= 5)
            {
                TempData["Message"] = "Only 5 players allowed.";
                return RedirectToAction("TeamManagement");
            }

            Player alreadyExistingPlayer = currentUserTeam.Players.SingleOrDefault(p => p.Id == playerId);

            if (alreadyExistingPlayer != null)
            {
                TempData["Message"] = $"{alreadyExistingPlayer.Name} already belongs to team.";
                return RedirectToAction("TeamManagement");
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
            TempData["Message"] = $"{player.Name} added to team.";
            return RedirectToAction("TeamManagement");
        }

        [Authorize]
        public ActionResult RemovePlayer(string playerId)
        {

            Player player = CSharpDbDAL.GetPlayer(playerId, 
                CSharpDbDAL.GetContextUser(User.Identity.Name).Id);

            if (player != null)
            {
                CSharpDbDAL.DeletePlayer(player);
            }

            return RedirectToAction("TeamManagement");
        }

        [Authorize]
        public ActionResult FindPlayer(string name)
        {
            JObject playerData = CsharpJamApi.GetSportSearchName(name);

            if (playerData.Count > 0)
            {
                List<UserPlayer> players = new List<UserPlayer>();

                //Building the player from api data
                foreach (JObject player in playerData["player"])
                {
                    UserPlayer userPlayer = new UserPlayer(player);
                    
                    //Exempted players with no data for eiither rating, height or weight
                    if (userPlayer.Rating > 0 || userPlayer.Height > 0 || userPlayer.Weight > 0)
                    {
                        players.Add(userPlayer);
                    }
                }
                return View(players);
            }
            return RedirectToAction("TeamManagement");
        }

        [Authorize]
        public ActionResult FindTeam(string teamName)
        {
            JObject teamData = CsharpJamApi.GetSportSearchTeam(teamName);

            if (teamData.Count > 0)
            {
                List<UserTeam> teams = new List<UserTeam>();

                //Building the team from api data
                foreach (JObject team in teamData["teams"])
                {
                    teams.Add(new UserTeam(team));
                }
                if (teams.Count > 1)
                {
                    return View(teams);
                }
                else if (teams.Count == 1)
                {
                    return RedirectToAction("FindPlayersByTeam", routeValues: new { teamName = teams[0].Name});
                }
            }
            return RedirectToAction("TeamManagement");
        }

        //Finds all the player on a team and displays the list
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

            return RedirectToAction("TeamManagement");
        }

        [HttpGet]
        public ActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTeam(Team team)
        {
            team.Draw = team.Lost = team.Win = 0;
            team.Location = "Earth";

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

        
    }
}

