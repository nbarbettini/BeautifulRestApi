using System;
using System.ComponentModel.DataAnnotations;

namespace BeautifulRestApi.Dal.DbModels
{
    public class Person
    {
        [Key]
        public string Href { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset BirthDate { get; set; }
    }
}