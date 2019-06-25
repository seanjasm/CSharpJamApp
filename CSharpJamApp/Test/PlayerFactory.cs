using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSharpJamApp.Models;

namespace CSharpJamApp.Test
{
    public class PlayerFactory
    {
        public static UserPlayer GetPlayer(PlayerType type)
        {
            UserPlayer userPlayer = new UserPlayer();

            if(type == PlayerType.Strong)
            {
                userPlayer.Aggression = 60;
                userPlayer.Height = 1.5;
                userPlayer.Humor = 50;
                userPlayer.Rating = 5;
                userPlayer.Skill = 98;
                userPlayer.Strength = 70;
                userPlayer.TeamWork = 88;
                userPlayer.Weight = 170;
            }
            else if(type == PlayerType.Moderate)
            {
                userPlayer.Aggression = 80;
                userPlayer.Height = 1.5;
                userPlayer.Humor = 10;
                userPlayer.Rating = 1.5;
                userPlayer.Skill = 45;
                userPlayer.Strength = 55;
                userPlayer.TeamWork = 25;
                userPlayer.Weight = 180;
            }
            else
            {
                userPlayer.Aggression = 100;
                userPlayer.Height = 1.5;
                userPlayer.Humor = 10;
                userPlayer.Rating = 1.5;
                userPlayer.Skill = 45;
                userPlayer.Strength = 40;
                userPlayer.TeamWork = 25;
                userPlayer.Weight = 180;
            }

            userPlayer.Agility = userPlayer.GetPlayerAgility();
            return userPlayer;
        }
    }

    public enum PlayerType
    {
        Weak,
        Strong,
        Moderate
    }
}