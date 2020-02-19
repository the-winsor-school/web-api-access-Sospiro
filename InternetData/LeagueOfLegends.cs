using System;
using System.Collections.Generic;
using System.Runtime.Serialization;L
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace InternetData
{
    public class LeagueOfLegends
    {
        protected static readonly string key = "RGAPI-d3f9cca1-0376-4ffe-9d43-efae7ec438d4";
        //protected static readonly double longitude = -71.1071909;
        //protected static readonly double latitude = 42.340993;

        public static userData GetUserData()
        {
            string summonerId = "F6XFidKD9ItZqMX-Q8NXJvv-B06OHeBavQuUFuDx5sEth-s";
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                string.Format("https://developer.riotgames.com/apis#league-v4/GET_getChallengerLeague/lol/league/v4/entries/by-summoner/{encryptedSummonerId}", summonerId));

            HttpResponseMessage response = client.SendAsync(request).Result;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(userData));

            if (!response.IsSuccessStatusCode)
            {
                return new userData();
            }

            return (userData)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
        }
    }

    [DataContract]
    public class userData
    {
        [DataMember]
        public string queueType;

        [DataMember]
        public summonerName string;

        [DataMember]
        public boolean hotStreak;

        [DataMember]
        public miniSeries MiniSeriesDTO;

        [DataMember]
        public wins int;

        [DataMember]
        public veteran bool;

        [DataMember]
        public losses int;

        [DataMember]
        public rank string;

        [DataMember]
        public leagueId string;
       
        [DataMember]
        public inactive bool;

        [DataMember]
        public freshBlood bool;

        [DataMember]
        public tier string;

        [DataMember]
        public summonerId string;

        [DataMember]
        public leaguePoints int;

    }

    [DataContract]
    public class MiniSeriesDTO
    {
        [DataMember]
        public progress string;

        [DataMember]
        public losses int;

        [DataMember]
        public target int;

        [DataMember]
        public wins int;

    }

}

}
