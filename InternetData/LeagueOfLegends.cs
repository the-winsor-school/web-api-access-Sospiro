using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace InternetData
{
    public class LeagueOfLegends
    {
        protected static readonly string key = "RGAPI-1083a310-c7e2-43e7-9c30-100af60625ac";
        //this is the api key that I got through my developer Riot account 
        //- this is the authorization I need in order to access Riot Games' api and access the api endpoints
        //it expires every two days - so I will keep needing to update this key
        //I last updated it Feb. 27 - will expire Feb. 28 ~@ 5 pm

        public static UserData GetUserData()
        {
            //string summonerId = "F6XFidKD9ItZqMX-Q8NXJvv-B06OHeBavQuUFuDx5sEth-s";
            HttpClient client = new HttpClient();
            //HttpClient - communicates http with the server
            //when you type something into the address bar, you send an http request
            //a couple lines below, we will provide the url we want to access

            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                string.Format("https://americas.api.riotgames.com/lor/ranked/v1/leaderboards?api_key={0}", key));
            //above, this is the endpoint url that I am using to access the data from this specific api endpoint
            //I'm using "{0}" and the ", key" to make the code more human-readable
            //- if I just copy and paste the key, which is a long string of digits and characters,:
            //then it will be hard for the coder to replace when the key expires
            //and it will be hard to see where the api key is being plugged into the code


            HttpResponseMessage response = client.SendAsync(request).Result;
            //when the client sends a request, you get a result
            //this result is the response message

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UserData));

            if (!response.IsSuccessStatusCode)
            {
                return new UserData();
                //will run if the response is not successful...returns empty if it's not successful
            }

            Console.WriteLine(response.Content.ReadAsStreamAsync().Result);
            return (UserData)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
            //overall, by this point, we have recieved the response message and deserialized it
        }
    }

    //below we are basically organizing our now-deserialized data we had recieved
    //from the Riot Games' api website, they list all the properties that the api endpoint returns:

    [DataContract]
    public class UserData
    {
        [DataMember]
        public List<PlayerDto> players;
        //"players" is a property of UserData that is a list of PlayerDto's (a list of objects)

    }

    [DataContract]
    public class PlayerDto
    {
        [DataMember]
        public string name;
        //this is a property of the object, PlayerDto - it holds string values and is called "name" (all the player usernames in the high-level ranked tiers of League of Legends)

        [DataMember]
        public int rank;
        //this is a property of the object, PlayerDto - it holds integer values and is called "rank"

        public override string ToString()
        {
            return string.Format("{0} --- Rank #{1}", name, rank);
            //returns the name and rank (properties) of each player (object)
        }

    }
}






















//left behind this code because I took a while to type all the properties in;
//hit a roadblock running it because Riot Games recently updated their format of summonerIDs (username codes) and there are some roadblocks to getting your summonerID code...

/*
public static UserData GetUserData()
{
    string summonerId = "F6XFidKD9ItZqMX-Q8NXJvv-B06OHeBavQuUFuDx5sEth-s";
    HttpClient client = new HttpClient();

    HttpRequestMessage request = new HttpRequestMessage(
        HttpMethod.Get,
        string.Format("https://na1.api.riotgames.com/lol/league/v4/entries/by-summoner/{0}?api_key{1}", summonerId, key));

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
*/


