﻿using System;

namespace Vavatech.CIS.Models
{

    public class Customer : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Pesel { get; set; }
        public decimal? Salary { get; set; }
        public bool IsRemoved { get; set; }

        public Customer Partner { get; set; }
    }
}
