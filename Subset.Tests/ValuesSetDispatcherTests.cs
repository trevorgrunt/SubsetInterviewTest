using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Xunit;
using SubsetMath;
using SubsetMath.Models;

namespace Subset.Tests
{
    public class ValuesSetDispatcherTests
    {
        [Fact]
        public void TestValuesFromJson()
        {
            ValuesSetDispatcher valuesSetDispatcher = new ValuesSetDispatcher();

            var reader = new JsonCollectionReader();
            var items = reader.ReadJsonCollection<ValuesGroup>("Resources\\ValuesGroups.json");


            int counter = 0;
            foreach (ValuesGroup item in items)
            {

                var subsets = valuesSetDispatcher.DispatcherSubsets(item);

                Assert.Equal(subsets.Count, item.Totals.Count);
                Assert.Equal(item.Totals.Sum(t => t.Value), subsets.Sum(s => s.Total.Value));
                Assert.Equal(item.Values.Sum(t => t.Value), subsets.Sum(s => s.Values.Sum(v => v.Value)));

                foreach (var group in subsets)
                {
                    var groupTotal = group.Total.Value;
                    var setSum = group.Values.Sum(v => v.Value);

                    Assert.Equal(groupTotal, setSum);
                }

                counter++;
            }
        }
    }
}

