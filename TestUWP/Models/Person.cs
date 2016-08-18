using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.AppModel;
using PropertyChanged;

namespace TestUWP.Models
{
    [ImplementPropertyChanged]
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string HairColor { get; set; }

        public string Specialty { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Initials => $"{FirstName.First()}{LastName.First()}".ToUpper();
    }
}
