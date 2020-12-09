using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Validators.Polish;

namespace Vavatech.CIS.Models.ValidationAttributes
{

    // dotnet add package PolishValidators
    public class PeselAttribute : ValidationAttribute
    {
        private readonly PeselValidator peselValidator;

        public PeselAttribute()
        {
            this.peselValidator = new PeselValidator();
        }

        public override bool IsValid(object value)
        {
            try
            {
                return peselValidator.IsValid(value.ToString());
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
