using System;
using System.ComponentModel.DataAnnotations;

namespace BeautifulRestApi.Models
{
    public class PersonCreateModel
    {
        [Required]
        [Display(Name = "firstName")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "lastName")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "birthDate")]
        public DateTimeOffset? BirthDate { get; set; }
    }
}
