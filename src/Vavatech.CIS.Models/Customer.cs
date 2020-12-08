using System;
using System.ComponentModel.DataAnnotations;
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

        [Pesel, Required, StringLength(11, MinimumLength = 11)]
        public string Pesel { get; set; }
        public decimal? Salary { get; set; }
        public bool IsRemoved { get; set; }

        public Customer Partner { get; set; }
    }
}
