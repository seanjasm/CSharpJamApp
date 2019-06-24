using Newtonsoft.Json.Linq;
using System;

namespace CSharpJamApp.Models
{
    public sealed class UserPlayer : Player, IAttack
    {
        public int Health { get;  set; }
        public float Damage { get;  set; }
        public bool Injured { get;  set; }


        public UserPlayer() { }

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


        private int GetRandomProbability(int min, int max)
        {
            return new Random().Next(min, max) + 1;
        }

        public double Attack(UserPlayer player)
        {

            return 0;
        }

        public double Defend(UserPlayer player)
        {
            throw new NotImplementedException();
        }

        public double HitProbability(UserPlayer player)
        {
            Health = 100;
            Injured = false;
            int remainingHealth = 100 - Health;

            //double hp =  Agility * Strength * TeamWork + Humor /(Weight * Math.Log(Aggression) * Aggression);
            double hp = (97 * 98 * Math.Log(98) * 98 + Math.Sqrt(20) )/ (67 * Math.Log10(98));
            if (this.Injured)
            {
                hp /= (GetRandomProbability(remainingHealth, 120)); // GetRandomProbability(1, 100);
            }
            else
            {
                hp /= (GetRandomProbability((remainingHealth < 50 ? remainingHealth  : 50), 50));//GetRandomProbability(1, 50);
            }

            return hp;
        }

        public double MissedProbability(UserPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
