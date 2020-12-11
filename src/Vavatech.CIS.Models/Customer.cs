using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Vavatech.CIS.Models.ValidationAttributes;

namespace Vavatech.CIS.Models
{
    // Data Annotations
    // dotnet add package System.ComponentModel.Annotations
    public class Customer : Base
    {
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }

        [Pesel, Required, StringLength(11, MinimumLength = 11)]
        public string Pesel { get; set; }
        public decimal? Salary { get; set; }
        public bool IsRemoved { get; set; }

        [XmlIgnore]
        public Customer Partner { get; set; }
    }
}
