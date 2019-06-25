using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CSharpJamApp.Models
{
    public class CSharpDbDAL
    {
        private static readonly csharpjamDBEntities ORM = new csharpjamDBEntities();

        public static AspNetUser GetContextUser(string username)
        {
            //We saved our user details in ASPNetUser it helps us to get aspnetuser 
            //using username
            return ORM.AspNetUsers.FirstOrDefault(u => u.UserName == username);
        }



        public static IEnumerable<SelectListItem> GetMyTeamAsSelectedListItem(string userId)
        {
            //This is going to display selectlistitem by using userid.
            List<SelectListItem> teams = new List<SelectListItem>();

            foreach (Team team in ORM.Teams.Where(t => t.OwnerId == userId).ToList())
            {
                teams.Add(new SelectListItem { Text = team.Name, Value = team.Name });
            }
            return teams;
        }



        public static List<Team> GetAllTeam()
        {
            return ORM.Teams.ToList();
        }

        public static bool AddTeam(Team team)
        {
            try
            {
                ORM.Teams.Add(team);
                ORM.SaveChanges();
                return true;
            }

            catch(Exception e)
            {
                return false;
            }
        }

        public static Team GetTeam(string ownerId)
        {
            return ORM.Teams.FirstOrDefault(team => team.OwnerId == ownerId);
        }

        public static void DeleteTeam(int ownerId)
        {
            Team found = ORM.Teams.Find(ownerId);
            ORM.Teams.Remove(found);
            ORM.SaveChanges();
        }

        public static void UpdateTeam(Team updatedteam)
        {
            Team originalTeam = ORM.Teams.Find(updatedteam.OwnerId);
            if (originalTeam != null)
            {
                originalTeam.Name = updatedteam.Name;
                originalTeam.Win = updatedteam.Win;
                originalTeam.Lost = updatedteam.Lost;
                originalTeam.Draw = updatedteam.Draw;
                originalTeam.Location = updatedteam.Location;

                ORM.SaveChanges();
            }
            //else
            //{

            //}
        }

        //public static List<Player> GetAllPlayers()
        //{
        //    return ORM.Players.ToList();
        //}

        public static void AddPlayers(Player player)
        {
            ORM.Players.Add(player);
            ORM.SaveChanges();
        }

        public static Player GetPlayer(string playerId)
        {
            return ORM.Players.SingleOrDefault(player => player.Id == playerId);
        }

        public static Player GetUserPlayer(string userId, string playerId)
        {
            try
            {
                return ORM.Teams.SingleOrDefault(o => o.OwnerId == userId)
                    .Players.FirstOrDefault(p => p.Id == playerId);
            }
            catch(Exception)
            {
                
            }
            return null;
        }

        public static List<Player> GetMyPlayers(string teamId)
        {
            return ORM.Players.Where(p => p.TeamId == teamId).ToList();                
        }

        public static void DeletePlayer(Player player)
        {
            ORM.Players.Remove(player);
            ORM.SaveChanges();
        }

        //public static float GetPlayerAgility(float Weight, float Height)
        //{  
        //    nWeight float;
        //    BMI float;
        //    nHeight float;
        //    nWeight = Weight / 100;
        //    nHeight = Height / 2.34;

        //    BMI = Weight / (Height * Height);

        //    if(BMI<18.5)
        //    {
        //        strength = nHeight * nWeight * BMI;
        //    }
        //    if (BMI >=18.5 && BMI <= 24.9)
        //    {
        //        strength = (nHeight * nWeight)/ BMI;
        //    }
        //    if (BMI >= 25 && BMI <= 29.9)
        //    {
        //        strength = (nHeight /( nWeight* BMI);
        //    }
        //    if (BMI > 30)
        //    {
        //        strength = (nHeight / (nWeight * BMI);
        //    }

        //}

       






        public static void UpdatePlayer(Player updatedPlayer)
        {
            Player originalTeam = ORM.Players.FirstOrDefault(p => p.Id == updatedPlayer.Id && p.TeamId == updatedPlayer.TeamId);

            if (originalTeam != null)
            {

                //originalTeam.Name = updatedPlayer.Name;
                originalTeam.Strength = updatedPlayer.Strength;
                originalTeam.Skill = updatedPlayer.Skill;
                originalTeam.Agility = updatedPlayer.Agility;
                originalTeam.Endurance = updatedPlayer.Endurance;
                originalTeam.Humor = updatedPlayer.Humor;
                originalTeam.Aggression = updatedPlayer.Aggression;
                originalTeam.TeamWork = updatedPlayer.TeamWork;
                originalTeam.Rating = updatedPlayer.Rating;
                originalTeam.Height = updatedPlayer.Height;
                originalTeam.Weight = updatedPlayer.Weight;
                originalTeam.Description = updatedPlayer.Description;
                ORM.SaveChanges();
            }
        }

        public static List<Match> GetMatch()
        {
            return ORM.Matches.ToList();
        }

        public static void AddMatch(Match match)
        {
            ORM.Matches.Add(match);
            ORM.SaveChanges();
        }

        public static void DeleteMatch(int Id)
        {
            Match found = ORM.Matches.Find(Id);
            ORM.Matches.Remove(found);
            ORM.SaveChanges();
        }

        public static void UpdateMatch(Match updatedMatch)
        {
            Match originalmatch = ORM.Matches.Find(updatedMatch.Id);
            if (originalmatch != null)
            {
                originalmatch.Location = updatedMatch.Location;
                originalmatch.Time = updatedMatch.Time;
                originalmatch.Weather = updatedMatch.Weather;
            }
        }
     
    }
}
