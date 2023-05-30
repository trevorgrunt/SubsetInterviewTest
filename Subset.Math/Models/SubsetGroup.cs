using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsetMath.Models
{
    public class SubsetGroup
    {
        public ValueContainer Total { get; private set; }

        public List<ValueContainer> Values { get; private set; }

        public SubsetGroup(ValueContainer total, IEnumerable<ValueContainer> values)
        {
            Total = total;

            Values = values.ToList();
        }
    }
}
