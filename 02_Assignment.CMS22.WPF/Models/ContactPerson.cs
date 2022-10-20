using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Assignment.CMS22.WPF.Models
{
    public class ContactPerson // detta är en property. get; innebär hämta/läs och set; spara/skriva
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";
    }
}
