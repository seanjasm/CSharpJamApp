using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpJamApp.Models
{
    public class UserTeam : Team
    {
        public string Description { get; set; }
        public string League { get; set; }
        public string Id { get; set; }

        public UserTeam(JObject team)
        {
            Description = (string)team["strDescriptionEN"];
            League = (string)team["strLeague"];
            Name = (string)team["strTeam"];
            Id = (string)team["idTeam"];
        }
    }
}