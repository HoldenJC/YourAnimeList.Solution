using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YourAnimeList.Models
{
  [Table("UserAnime")]
  public class Anime
  {
    [Key]
    public virtual int DbId { get;set; }
    public int UserId { get; set; }
    public int Mal_Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Synopsis { get; set; }
    public string Image_Url { get; set; }
    public float Score { get; set; }
    


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
    public int Mal_Id { get; set; }
    public string Image_Url { get; set; }
    public string Trailer_Url { get; set; }
    public string Title { get; set; }
    public string Title_English { get; set; }
    public string Title_Japanese { get; set; }
    public string Synopsis { get; set; }
    public int Episodes { get; set; }
    public float Score { get; set; }
    public int Scored_By { get; set; }
    public int Popularity { get; set; }
    public string Premiered { get; set; }
    public string Rank { get; set; }
    public string Status { get; set; }
    public JObject Related { get; set; }
    

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
  public class AnimeRecommends
  {
    public JArray Recommendations { get; set; }

    public static AnimeRecommends GetAnimeRecommends(string id)
    {
      var client = new RestClient("https://api.jikan.moe/v3");
      var request = new RestRequest("anime/" + id + "/recommendations", Method.GET);
      var response = new RestResponse();
      Task.Run(async () =>
      {
        response = await GetResponseContentAsync(client, request) as RestResponse;
      }).Wait();
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
      var animeRecommends = JsonConvert.DeserializeObject<AnimeRecommends>(jsonResponse.ToString());
      return animeRecommends;
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