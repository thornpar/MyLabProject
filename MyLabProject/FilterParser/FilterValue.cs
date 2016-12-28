using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLabProject.FilterParser
{
    public class FilterValue
    {
        public string ComparisonOperator { get; set; }
        public string Value { get; set; }
        public string FieldName { get; set; }
        public string LogicalOperator { get; set; }
    }
}