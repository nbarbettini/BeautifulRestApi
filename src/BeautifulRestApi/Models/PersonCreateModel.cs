using System;
using System.ComponentModel.DataAnnotations;

namespace BeautifulRestApi.Models
{
    public class PersonCreateModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }
    }
}
