using System.Collections.Generic;
using TestUWP.Models;

namespace TestUWP.Services
{
    public interface IFakeDataService
    {
        List<Person> GeneratePeople();
    }
}