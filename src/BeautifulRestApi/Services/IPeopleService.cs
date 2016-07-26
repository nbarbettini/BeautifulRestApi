using System.Collections.Generic;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Services
{
    public interface IPeopleService
    {
        IEnumerable<PersonResponse> GetAll();
    }
}