using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsetMath.Models
{
    public class Solution
    {
        public int StartIndex;
        public int EndIndex;
        public decimal Sum;
        public int Length
        {
            get { return EndIndex - StartIndex + 1; }
        }
    }
}
