namespace Feedgen.Models;

using SharedClasses;
using System.ComponentModel.DataAnnotations;

public class Feed {
    [Key]
    public int RequesterId {get; set;}
    public List<String> Posts {get; set;}
}