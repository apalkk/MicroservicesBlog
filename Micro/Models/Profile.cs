namespace Micro.Models;
using System.ComponentModel.DataAnnotations;

public class Profile
{
    public int Id {get; set;}
    public string UserName {get; set;} = "";
    public string Email {get; set;} = "";
    public string Password {get; set;} = "";
    [DataType(DataType.Date)]
    public List<Profile> Followers {get;  set;} = new();
    public DateTime JoinDate;
    public void FollowProfile(Profile profile)
    {
        Followers.Add(profile);
    }

}