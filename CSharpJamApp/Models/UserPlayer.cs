using Newtonsoft.Json.Linq;
using System;

namespace CSharpJamApp.Models
{
    public class UserPlayer : Player
    {
        public UserPlayer(JObject player)
        {
            try
            {
                Id = (string)player["idPlayer"];
                Name = (string)player["strPlayer"];
                PictureUrl = (string)player["strThumb"];

                string wage = (string)player["strWage"];

                if (wage.Length > 12)
                {
                    Rating = 5;
                }
                else if (wage.Length > 9)
                {
                    Rating = 3.5;
                }
                else
                {
                    Rating = 1.5;
                }

                if (float.TryParse((string)player["strHeight"], out float height))
                {
                    Height = height;
                }
                else
                {
                    Height = 1.5;
                }

                if (float.TryParse((string)player["strWeight"], out float weight))
                {
                    Weight = weight;
                }
                else
                {
                    Weight = 66.3;
                }

                Description = (string)player["strDescriptionEN"];
                TeamId = (string)player["idTeam"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
