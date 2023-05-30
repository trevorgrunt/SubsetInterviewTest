using SubsetMath;
using SubsetMath.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubsetConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(SubsetConsole)} Started!");
            var startTime = DateTime.Now;

            string path = @"Resources\ValuesGroups.json";

            ValuesSetDispatcher valuesSetDispatcher = new ValuesSetDispatcher();

            var reader = new JsonCollectionReader();
            var items = reader.ReadJsonCollection<ValuesGroup>(path);

            var blackList = new List<KeyValuePair<long, string>>();

            int counter = 0;
            int total = items.Count();

            DateTime lastWriteTime = DateTime.Now;

            foreach (ValuesGroup item in items)
            {
                try
                {
                    counter++;

                    var itemStartTime = DateTime.Now;

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

                    Console.WriteLine($"Processed {counter} {nameof(ValuesGroup)}s of {total}; " +
                        $"{nameof(ValuesGroup)}.{nameof(ValuesGroup.GroupId)}={item.GroupId}; " +
                        $"{nameof(ValuesGroup)} processing time: {DateTime.Now - itemStartTime};");

                    if (DateTime.Now - lastWriteTime > TimeSpan.FromSeconds(30))
                    {
                        Console.WriteLine($"Time passed since start:{DateTime.Now - startTime}");
                        lastWriteTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    blackList.Add(new KeyValuePair<long, string>(item.GroupId, ex.ToString()));
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            File.WriteAllText("blackList.json", Newtonsoft.Json.JsonConvert.SerializeObject(blackList));

            Console.WriteLine("Program completed.");
            Console.WriteLine("Total time: {0}", (DateTime.Now - startTime).ToString());
        }
    }

    static class Assert
    {
        public static void Equal(decimal t1, decimal t2)
        {
            if (t1 != t2)
                throw new Exception($"t1 != t2; t1:{t1} t2:{t2}");
        }
    }
}