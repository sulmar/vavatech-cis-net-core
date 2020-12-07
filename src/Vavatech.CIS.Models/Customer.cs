using System;

namespace Vavatech.CIS.Models
{

    public class Customer : Base
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public bool IsRemoved { get; set; }
    }
}
