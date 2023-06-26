using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Feedgen.Models;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;



namespace Feedgen.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedController : ControllerBase
{
    [HttpGet("{id}")]
    public string GetFeed(int id)
    {
        Feed f = new Feed
        {
            RequesterId = 1,
            Posts = new List<string>()
            {
                "hello world"
            }
        };

       var i = Interact(
          "5069",
          $"http://localhost:5069/api/Follow/Followed/{id}",
          "{\"followId\": 0, \"followerId\": 0, \"followedId\": 0}");

       string ret = "";
       foreach(int num in Reg(i.Result))
       {
           var s = Interact(
               "5173",
               $"http://localhost:5173/api/Post/specific/{num}",
               "{\"postId\": 0, \"title\": \"string\", \"content\": \"string\",\"likes\": 0, \"creator\": 0}");

           ret += s.Result;
       }
       return ret;
    }

    private int[] Reg(string jsonString)
    {
        // Define the regular expression pattern to match the "followerId" values
        string pattern = @"""followedId"":(\d+)";

        // Create a Regex object with the pattern
        Regex regex = new Regex(pattern);

        // Find all matches of the pattern in the JSON string
        MatchCollection matches = regex.Matches(jsonString);

        // Create a list to store the extracted followerIds
        List<int> followerIds = new List<int>();

        // Iterate over the matches and extract the followerId values
        foreach (Match match in matches)
        {
            if (match.Groups.Count >= 2 && int.TryParse(match.Groups[1].Value, out int followerId))
            {
                followerIds.Add(followerId);
            }
        }

        // Convert the list to an array if needed
        int[] followerIdsArray = followerIds.ToArray();

        return followerIdsArray;
    }

    static async Task<string> Interact(string port, string enpoint, string body)
    {
       using (HttpClient client = new HttpClient())
     {
        // Set the base URL of your API
        Uri baseUrl = new Uri(enpoint);

        // Construct the request body as a JSON string
        string requestBody = body;
        //string requestBody = "{\"id\": 0, \"userName\": \"string\", \"email\": \"string\", \"password\": \"string\"}";

        // Create the HTTP request message
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl);
        request.Headers.Add("Accept", "application/json");
        request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        // Send the request and get the response
        HttpResponseMessage response = await client.SendAsync(request);

        // Check the response status
        if (response.IsSuccessStatusCode)
        {
            // Request successful
            Console.WriteLine("POST request successful");
        }
        else
        {
            // Request failed
            Console.WriteLine("POST request failed with status code: " + response.StatusCode);
        }

        return await response.Content.ReadAsStringAsync();
    }

}
}
