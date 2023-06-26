using System.ComponentModel.DataAnnotations;

namespace SharedClasses;
   

public class Feed
{
    [Key]
    public int RequesterId { get; set; }
    public List<String> Posts { get; set; }
}

public class Follow
{
    [Key]
    public int FollowId { get; set; }
    public int FollowerId { get; set; }
    public int FollowedId { get; set; }
}

public class Profile
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    [DataType(DataType.Date)]
    public DateTime JoinDate;
}

public class Post
{
    public int postId { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public int Likes { get; set; } = 0;
    public int Creator { get; set; } = 0;
}