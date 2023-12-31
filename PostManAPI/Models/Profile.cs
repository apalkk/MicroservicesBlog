namespace PostManAPI.Models;

using SharedClasses;
using System.ComponentModel.DataAnnotations;

public class Profile
{
    [Key]
    public int Id {get; set;}
    public string UserName {get; set;} = "";
    public string Email {get; set;} = "";
    public string Password {get; set;} = "";
    [DataType(DataType.Date)]
    public DateTime JoinDate;
}