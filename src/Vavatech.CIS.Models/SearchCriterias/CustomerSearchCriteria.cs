using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.CIS.Models.SearchCriterias
{
    public abstract class SearchCriteria : Base
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public string FirstName { get; set; }
        public decimal? From { get; set; }
        public decimal? To { get; set; }
    }
}
