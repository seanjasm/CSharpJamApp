using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CSharpJamApp.Models
{
    public class CsharpJamApi
    {
        private static readonly string apiKey = ConfigurationManager.AppSettings["apiKey"];

        public static JObject GetSportPlayerByTeam(string teamName)
        {
            return GetApi($"https://www.thesportsdb.com/api/v1/json/{apiKey}/searchplayers.php?t={teamName}");
        }
        public static JObject GetSportTeamId(string teamId)
        {
            return GetApi($"https://www.thesportsdb.com/api/v1/json/{apiKey}/lookupteam.php?id={teamId}");
        }

        public static JObject GetSportSearchTeam(string teamName)
        {         
            return GetApi($"https://www.thesportsdb.com/api/v1/json/{apiKey}/searchteams.php?t={teamName}");
        }

        public static JObject GetSportSearchName(string name)
        {
            return GetApi($"https://www.thesportsdb.com/api/v1/json/{apiKey}/searchplayers.php?p={name}");
        }

        public static JObject GetSportPlayerId(string playerId)
        {
            return GetApi($"https://www.thesportsdb.com/api/v1/json/1/lookupplayer.php?id={playerId}");
        }



        private static  JObject GetApi(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());

                return JObject.Parse(data.ReadToEnd());
            }
            return null;
        }


    }
} 