using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YourAnimeList.Models
{
  public class Anime
  {
    public int Mal_Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Synopsis { get; set; }
    public string Image_Url {get; set; }

    public static List<Anime> GetAnimes(string query)
    {
      var client = new RestClient("https://api.jikan.moe/v3");
      var request = new RestRequest("search/anime?q=" + query + "&limit=9", Method.GET);
      var response = new RestResponse();
      Task.Run(async () =>
      {
        response = await GetResponseContentAsync(client, request) as RestResponse;
      }).Wait();
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
      var animeList = JsonConvert.DeserializeObject<List<Anime>>(jsonResponse["results"].ToString());
      return animeList;
    }

    public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
    {
      var tcs = new TaskCompletionSource<IRestResponse>();
      theClient.ExecuteAsync(theRequest, response =>
      {
        tcs.SetResult(response);
      });
      return tcs.Task;
    }
  }

  public class AnimeFull
  {
    public string Image_Url {get; set; }
    public string Trailer_Url { get; set; }
    public string Title { get; set; }
    public string Title_English { get; set; }
    public string Title_Japanese { get; set; }
    public string Synopsis { get; set; }

    public static AnimeFull GetAnimeDetails(string id)
    {
      var client = new RestClient("https://api.jikan.moe/v3");
      var request = new RestRequest("anime/" + id, Method.GET);
      var response = new RestResponse();
      Task.Run(async () =>
      {
        response = await GetResponseContentAsync(client, request) as RestResponse;
      }).Wait();
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
      var animeDetails = JsonConvert.DeserializeObject<AnimeFull>(jsonResponse.ToString());
      return animeDetails;
    }

    public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
    {
      var tcs = new TaskCompletionSource<IRestResponse>();
      theClient.ExecuteAsync(theRequest, response =>
      {
        tcs.SetResult(response);
      });
      return tcs.Task;
    }
  }
}