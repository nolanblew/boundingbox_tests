using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUWP.Models;

namespace TestUWP.Services
{
    public class FakeDataService : IFakeDataService
    {
        public List<Person> GeneratePeople()
        {
            return new List<Person>
            {
                new Person
                {
                    FirstName = "John",
                    LastName = "Avaloke",
                    HairColor = "Black",
                    Specialty = "Product"
                },
                new Person
                {
                    FirstName = "Rebecca",
                    LastName = "Cowell",
                    HairColor = "Brown",
                    Specialty = "Engineering"
                },
                new Person
                {
                    FirstName = "Jessica",
                    LastName = "Markenson",
                    HairColor = "Brown",
                    Specialty = "Software"
                },
                new Person
                {
                    FirstName = "Alex",
                    LastName = "Tyler",
                    HairColor = "Golden Brown",
                    Specialty = "Finance"
                }
            };
        }
    }
}
