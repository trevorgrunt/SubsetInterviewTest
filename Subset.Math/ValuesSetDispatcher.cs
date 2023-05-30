using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubsetMath.Models;

namespace SubsetMath
{
    public class ValuesSetDispatcher
    {
        SubSetFinder _SubSetFinder = new SubSetFinder();

        public ValuesSetDispatcher() 
        {
                       
        }

        public List<SubsetGroup> DispatcherSubsets(ValuesGroup valuesGroup)
        {
            var subsetGroups = new List<SubsetGroup>();

            var totals = valuesGroup.Totals.OrderBy(d => d.Value).ToList();
            var totalsSum = totals.Sum(t => t.Value);
            var valuesSum = valuesGroup.Values.Sum(v => v.Value);
            var delta = totalsSum - valuesSum;

            if (delta != 0.0m)
            {
                subsetGroups.Clear();
                return subsetGroups;
            }
                
            var values = valuesGroup.Values.Select(v => new ValueQuantityContainer(v)).ToList();

            var globalStep = CalculateStep(values);

            for (int i = 0; i < totals.Count; i++)
            {
                ValueContainer total = totals[i];

                var localStep = CalculateStep(values);

                if (localStep < globalStep)
                    localStep = globalStep;

                var subSet = _SubSetFinder.GetSubset(values, total.Value, localStep)
                    .ToHashSet();

                var sum = subSet.Sum(t => t.Quantity);

                if (sum != total.Value)
                {
                    //// Находим элемент из subSet, чья Quantity наиболее близка к total.Value
                    var closestQuantity = subSet.OrderBy(t => Math.Abs(t.Quantity - total.Value)).First().Quantity;
                    //// Создаем новый SubsetGroup, содержащий total и все элементы subSet, у которых Quantity равна closestQuantity
                    subsetGroups.Add(new SubsetGroup(total, subSet.Where(t => t.Quantity == closestQuantity).Select(s => s.Value)));
                }
                else
                {
                    subsetGroups.Add(new SubsetGroup(total, subSet.Select(s => s.Value)));
                }

                values = values.Where(v => !subSet.Contains(v)).ToList();
            }

            return subsetGroups;
        }

        decimal CalculateStep(List<ValueQuantityContainer> values)
        {
            decimal maxValue = values.Max(v => v.Quantity);
            decimal minValue = values.Min(v => v.Quantity);
            decimal step = (maxValue - minValue) / values.Count;
            return step;
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
