using Bogus;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Fakers
{
    public class PeriodFaker : Faker<Period>
    {
        public PeriodFaker()
        {
            RuleFor(p => p.From, f => f.Date.Recent(30));
            RuleFor(p => p.To, (f, period) => f.Date.Recent(14, period.From));
        }
    }
}
