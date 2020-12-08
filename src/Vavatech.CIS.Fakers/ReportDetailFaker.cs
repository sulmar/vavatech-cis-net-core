using Bogus;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Fakers
{
    public class ReportDetailFaker : Faker<ReportDetail>
    {
        public ReportDetailFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Lorem.Sentence());
            RuleFor(p => p.Value, f => f.Random.Int(100, 500));
        }
    }
}
