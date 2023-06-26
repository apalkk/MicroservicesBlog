namespace UserManAPI.Models;

using SharedClasses;

public class Post {
    public int postId {get; set;}
    public string Title {get; set;} = "";
    public string Content {get; set;} = "";
    public int Likes {get; set;} = 0;
    public int Creator {get; set;} = 0;
}