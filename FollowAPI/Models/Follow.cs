using System.ComponentModel.DataAnnotations;

namespace FollowAPI.Models;

public class Follow {
    [Key]
    public int FollowId {get; set;}
    public int FollowerId {get; set;}
    public int FollowedId {get; set;}
}