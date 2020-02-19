using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace InternetData
{
    public class LeagueOfLegends
    {
        protected static readonly string key = "RGAPI-d3f9cca1-0376-4ffe-9d43-efae7ec438d4";

        public static UserData GetUserData()
        {
            string summonerId = "F6XFidKD9ItZqMX-Q8NXJvv-B06OHeBavQuUFuDx5sEth-s";
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                string.Format("https://na1.api.riotgames.com/api/lol/league/v4/entries/by-summoner/{0}?api_key={1}", summonerId, key));

            HttpResponseMessage response = client.SendAsync(request).Result;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UserData));

            if (!response.IsSuccessStatusCode)
            {
                return new UserData();
            }
            Console.WriteLine(response.Content.ReadAsStreamAsync().Result);
            return (UserData)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
        }
    }

    [DataContract]
    public class UserData
    {
        [DataMember]
        public string queueType;

        [DataMember]
        public string summonerName;

        [DataMember]
        public bool hotStreak;

        [DataMember]
        public MiniSeriesDTO miniSeries;

        [DataMember]
        public int wins;

        [DataMember]
        public bool veteran;

        [DataMember]
        public int losses;

        [DataMember]
        public string rank;

        [DataMember]
        public string leagueId;
       
        [DataMember]
        public bool inactive;

        [DataMember]
        public bool freshBlood;

        [DataMember]
        public string tier;

        [DataMember]
        public string summonerId;

        [DataMember]
        public int leaguePoints;

    }

    [DataContract]
    public class MiniSeriesDTO
    {
        [DataMember]
        public string progress;

        [DataMember]
        public int losses;

        [DataMember]
        public int target;

        [DataMember]
        public int wins;

    }
   

}

