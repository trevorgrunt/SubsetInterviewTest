using System;
using System.Collections.Generic;

namespace SubsetMath.Models
{
    public class ValuesGroup
    {
        public long GroupId { get; set; }

        public List<ValueContainer> Totals { get; set; } = null!;

        public List<ValueContainer> Values { get; set; } = null!;
    }
}
