

namespace CSharpJamApp.Models
{
    public class PlayerFactory
    {        
        public static UserPlayer GetPlayer(PlayerType type)
        {
            UserPlayer userPlayer = new UserPlayer();

            if(type == PlayerType.Strong)
            {
                userPlayer.Aggression = 50;
                userPlayer.Agility = 80;
                userPlayer.Humor = 50;
                userPlayer.Skill = 98;
                userPlayer.Strength = 90;
                userPlayer.TeamWork = 88;
                userPlayer.Endurance = 89;
                userPlayer.Agility = 80;
            }
            else if(type == PlayerType.Normal)
            {
                userPlayer.Aggression = 90;
                userPlayer.Humor = 50;
                userPlayer.Skill = 55;
                userPlayer.Strength = 85;
                userPlayer.TeamWork = 80;
                userPlayer.Endurance = 72;
                userPlayer.Agility = 80;
            }
            else
            {
                userPlayer.Aggression = 100;
                userPlayer.Humor = 70;
                userPlayer.Skill = 55;
                userPlayer.Strength = 60;
                userPlayer.TeamWork = 84;
                userPlayer.Endurance = 90;
                userPlayer.Agility = 70;
            }

            userPlayer.Agility = userPlayer.GetPlayerAgility();
            return userPlayer;
        }
    }


    public enum PlayerType
    {
        Weak,
        Strong,
        Normal
    }
}