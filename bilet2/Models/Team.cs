using System.ComponentModel.DataAnnotations;

namespace bilet2.Models;

public class Team
{
    public int Id { get; set; }
    [Required]
    public string Imagepath { get; set; }
    [Required,MaxLength(255)]
    public string Title { get; set; }
    [Required, MaxLength(255)]
    public string Description { get; set; }
   
}
