using Newtonsoft.Json.Linq;
using System;

namespace CSharpJamApp.Models
{
    public sealed class UserPlayer : Player, IAttack
    {
        public int Health { get;  set; }
        public int Damage { get;  set; }
        public bool Injured { get;  set; }


        public UserPlayer() {
            Injured = false;
            Health = 10;
        }

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

        public double HitProbability(UserPlayer player)
        {
            int remainingHealth = 100 - Health;

            double hp = (Agility * Rating * Math.Sqrt(Strength) * Math.Log(TeamWork) + Math.Sqrt(Humor) )/ (Agility * Math.Log10(Aggression));

            //Using the remaing health as the players energy
            //Enerrgy is used to determine if the player will produce maximum damage
            if (this.Injured)
            {
                //If player is injured the chances of getting maximum damage is reduced by 50%
                hp /= (GetRandomProbability(remainingHealth, 120));
            }
            else
            {                
                hp /= (GetRandomProbability((remainingHealth < 50 ? remainingHealth  : 50), 50));
            }

            hp *= player.GetPlayerAgility();

            return hp;
        }

        public double GetDamagePercentage()
        {
            return (Strength * Aggression) / Agility * 0.1;
        }

        public int GetDamage()
        {
            return int.Parse((Strength * Aggression / 100).ToString());
        }


        //A research done by Dr. mahesh Singh Dhapola and Dr. Bharat Verma
        //The aim of the study was to investigate the relationships of height, weight and BMI with
        //agility and speed of male university players.
        public float GetPlayerAgility()
        {
            double nWeight;
            double  BMI;
            double nHeight;
            nWeight = Weight/10000;
            nHeight = Height / 2.34;

            BMI = Weight / (Height * Height);

            if (BMI < 18.5)
            {
                Agility = BMI / (nWeight * nHeight);
            }
            if (BMI >= 18.5 && BMI <= 24.9)
            {
                Agility = 1 / (nHeight * nWeight * BMI);
            }
            if (BMI >= 25 && BMI <= 29.9)
            {
                Agility = 1 / (nHeight * nWeight * BMI); ;
            }
            if (BMI > 30)
            {
                Agility = nHeight / (nWeight * BMI); ;
            }

            return (float)Agility * 100;
        }
    }
}
