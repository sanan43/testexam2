using System.ComponentModel.DataAnnotations;

namespace bilet2.Areas.Admin.ViewModels
{
    public class UpdateVM
    {
        public int Id { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
    }
}
