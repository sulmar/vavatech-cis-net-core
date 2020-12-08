using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.CIS.Models
{
    public class Report : Base
    {
        public DateTime CreateDate { get; set; }
        public Period Period { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ReportDetail> Details { get; set; }

        public Report()
        {
            Period = new Period();
            Details = new List<ReportDetail>();
        }

    }
}
