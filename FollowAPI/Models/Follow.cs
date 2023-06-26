namespace FollowAPI.Models;

using System.ComponentModel.DataAnnotations;
using SharedClasses;

public class Follow {
    [Key]
    public int FollowId {get; set;}
    public int FollowerId {get; set;}
    public int FollowedId {get; set;}
}