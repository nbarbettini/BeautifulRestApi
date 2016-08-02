using System.ComponentModel.DataAnnotations;

namespace BeautifulRestApi.Models
{
    public class PostCreateModel
    {
        [Required]
        [Display(Name = "userId")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "content")]
        [MaxLength(140)]
        public string Content { get; set; }
    }
}
