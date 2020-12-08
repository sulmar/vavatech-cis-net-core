using FluentValidation;
using System;
using System.Linq;

namespace Vavatech.CIS.Models.Validators
{

    // dotnet add package FluentValidation
    public class ReportValidator : AbstractValidator<Report>
    {
        public ReportValidator()
        {
            RuleFor(p => p.Title).NotEmpty().Length(3, 20);
            RuleFor(p => p.Period.From).LessThanOrEqualTo(p => p.Period.To);

            RuleFor(p => p.Details).NotEmpty();
        }
    }
}
