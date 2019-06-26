

namespace CSharpJamApp.Models
{
    public class PlayerFactory
    {
        public static UserPlayer GetPlayer(PlayerType type)
        {
            UserPlayer userPlayer = new UserPlayer();

            if(type == PlayerType.Strong)
            {
                userPlayer.Aggression = 60;
                userPlayer.Agility = 80;
                userPlayer.Humor = 50;
                userPlayer.Skill = 98;
                userPlayer.Strength = 70;
                userPlayer.TeamWork = 88;

                userPlayer.Endurance = 89;
                userPlayer.Agility = 80;
            }
            else if(type == PlayerType.Moderate)
            {
                userPlayer.Aggression = 80;
                userPlayer.Humor = 10;
                userPlayer.Skill = 45;
                userPlayer.Strength = 55;
                userPlayer.TeamWork = 25;


                userPlayer.Endurance = 72;
                userPlayer.Agility = 80;
            }
            else
            {
                userPlayer.Aggression = 100;
                userPlayer.Humor = 10;
                userPlayer.Skill = 45;
                userPlayer.Strength = 40;
                userPlayer.TeamWork = 25;


                userPlayer.Endurance = 69;
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
        Moderate
    }
}