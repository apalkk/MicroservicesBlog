using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Feedgen.Models;
using Newtonsoft.Json;


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
            "api/follow/followers/{id}",
            "{\"followId\": 0, \"followerId\": 0, \"followedId\": 0}");
        
        return i.Result;//JsonConvert.SerializeObject(f.Posts)
     }
    
    static async Task<string> Interact(string port, string enpoint, string body)
    {
        using (HttpClient client = new HttpClient())
        {
            // Set the base URL of your API
            string baseUrl = $"http://localhost:{port}";

            // Set the API endpoint URL
            string apiUrl = enpoint;

            // Construct the request body as a JSON string
            string requestBody = body;
            //string requestBody = "{\"id\": 0, \"userName\": \"string\", \"email\": \"string\", \"password\": \"string\"}";

            // Create the HTTP request message
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUrl + apiUrl);
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
