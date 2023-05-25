using System.ComponentModel.DataAnnotations;

namespace bilet2.Areas.Admin.ViewModels
{
    public class CreateVM
    {
        
        [Required, MaxLength(255)]
        public string Title { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}

