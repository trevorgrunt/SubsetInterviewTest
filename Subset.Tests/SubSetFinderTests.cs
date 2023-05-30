using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Xunit;
using Newtonsoft.Json;
using SubsetMath;
using SubsetMath.Models;

namespace Subset.Tests
{
    public class SubSetFinderTests
    {
        [Fact]
        public void TestSmallGroupSet()
        {
            var valuesGroup = JsonConvert.DeserializeObject<ValuesGroup>(File.ReadAllText("Resources\\ValuesGroup_10717.json"))!;
            ValueContainer total = valuesGroup.Totals.First();

            var values = valuesGroup.Values.Select(v => new ValueQuantityContainer(v)).ToList();

            var localStep = 0.000001m;

            SubSetFinder subSetFinder = new SubSetFinder();
            var subSet = subSetFinder.GetSubset(values, total.Value, localStep);

            Assert.Equal(total.Value, subSet.Sum(s => s.Quantity));
        }

        [Fact]
        public void TestBigGroupSet()
        {
            var valuesGroup = JsonConvert.DeserializeObject<ValuesGroup>(File.ReadAllText("Resources\\ValuesGroup_6083.json"))!;
            var values = valuesGroup.Values.Select(v => new ValueQuantityContainer(v)).ToList();

            ValueContainer minTotal = valuesGroup.Totals.MinBy(t => t.Value)!;
            ValueContainer maxTotal = valuesGroup.Totals.MaxBy(t => t.Value)!;

            var localStep = 0.22m;

            SubSetFinder subSetFinder = new SubSetFinder();

            var minSubSet = subSetFinder.GetSubset(values, minTotal.Value, localStep);
            Assert.Equal(minTotal.Value, minSubSet.Sum(s => s.Quantity));

            var maxSubSet = subSetFinder.GetSubset(values, maxTotal.Value, localStep);
            Assert.Equal(maxTotal.Value, maxSubSet.Sum(s => s.Quantity));
        }

        class ValueQuantityContainer : IQuantity
        {
            public ValueContainer Value { get; private set; }

            public decimal Quantity { get; private set; }

            public ValueQuantityContainer(ValueContainer val)
            {
                this.Value = val;
                this.Quantity = val.Value;
            }
        }
    }
}
