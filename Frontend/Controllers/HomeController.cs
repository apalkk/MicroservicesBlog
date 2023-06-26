using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;


namespace Frontend.Controllers;

public class HomeController : Controller
{
    public static int User_Id = 0;
    public static string User = "";
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User != "")
        {
            ViewData["username"] = User;
        }
        return View();
    }

    public IActionResult Privacy()
    {
        if (User != "")
        {
            ViewData["username"] = User;
        }
        return View();
    }

    public IActionResult Feed()
    {
        if (User != "")
        {
            List<string> arr = new List<string>();
            var jsonString = Interact("5195", $"http://localhost:5195/Feed/{User_Id}", "").Result;
            ViewData["username"] = User;
            ViewData["feed"] = jsonString;
        }
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Follow()
    {
        return View();
    }

    public IActionResult Follow(int followedId)
    {
        var s = InteractPost("5069",
            $"http://localhost:5069/api/Follow",
            $"{{\"followId\": 0, \"followerId\": {User_Id}, \"followedId\": {followedId}}}");
        return RedirectToAction("Index");
    }

    public IActionResult Post()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Post(string title, string content)
    {
        if (User_Id == 0)
        {
            return View();
        }
        var s = InteractPost("5173",
                       $"http://localhost:5173/api/Post/",
                       $"{{\"postId\": 0, \"title\": \"{title}\", \"content\": \"{content}\",\"likes\": 0, \"creator\": {User_Id}}}");
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var s = Interact(
            "5173",
            $"http://localhost:5011/specific/{username}",
            "{\"id\": 0, \"userName\": \"string\", \"email\": \"string\",\"password\": \"string\"}");

        string pattern = "\"password\"\\s*:\\s*\"([^\"]+)\"";
        Match match = Regex.Match(s.Result, pattern);

        string pattern1 = "\"id\"\\s*:\\s*(\\d+)";
        Match id_match = Regex.Match(s.Result, pattern1);

        if (match.Success && match.Groups[1].Value == password)
        {
            User_Id = int.Parse(id_match.Groups[1].Value);
            User = username;
            return RedirectToAction("Index");
        }
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    static async Task<string> InteractPost(string port, string enpoint, string body)
    {
        using (HttpClient client = new HttpClient())
        {
            // Set the base URL of your API
            Uri baseUrl = new Uri(enpoint);

            // Construct the request body as a JSON string
            string requestBody = body;
            //string requestBody = "{\"id\": 0, \"userName\": \"string\", \"email\": \"string\", \"password\": \"string\"}";

            // Create the HTTP request message
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
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
