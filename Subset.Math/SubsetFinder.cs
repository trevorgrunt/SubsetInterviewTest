using SubsetMath.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsetMath
{
    public class SubSetFinder
    {

        /// <summary>
        /// Returns list of elements for the <paramref name="inputSet"/> whose sum 
        /// is equal to  <paramref name="value"/> or most close to it.
        /// </summary>
        /// <remarks>
        /// If the desired <paramref name="value"/> is not found, than either the nearest sum or an empty list will be returned.
        /// </remarks>
        /// <typeparam name="T">The type of the elements in the input <paramref name="inputSet"/></typeparam>
        /// <param name="inputSet">The initial collection in which the search will occur.</param>
        /// <param name="value"> desired sum.</param>
        /// <param name="step">The step by which the values will change while searching for a suitable amount</param>
        /// <returns>Returns found collection of elements form <paramref name="inputSet"/>.</returns>
        public List<T> GetSubset<T>(List<T> inputSet, decimal value, decimal step) where T : IQuantity
        {
            Solution bestSolution = new Solution { StartIndex = 0, EndIndex = -1, Sum = 0 };
            decimal bestError = Math.Abs(value);
            Solution currentSolution = new Solution { StartIndex = 0, Sum = 0 };

            for (int i = 0; i < inputSet.Count; i++)
            {
                currentSolution.EndIndex = i;
                currentSolution.Sum += inputSet[i].Quantity;

                // Удаляем элементы из начала текущего решения, пока сумма не станет меньше или равной value
                while (inputSet[currentSolution.StartIndex].Quantity <= currentSolution.Sum - value)
                {
                    currentSolution.Sum -= inputSet[currentSolution.StartIndex].Quantity;
                    ++currentSolution.StartIndex;
                }
                // Вычисляем погрешность текущего решения
                decimal currentError = Math.Abs(currentSolution.Sum - value);
                if (currentError < bestError || currentError == bestError && currentSolution.Length < bestSolution.Length)
                {
                    bestError = currentError;
                    bestSolution.Sum = currentSolution.Sum;
                    bestSolution.StartIndex = currentSolution.StartIndex;
                    bestSolution.EndIndex = currentSolution.EndIndex;
                }
            }

            return inputSet.GetRange(bestSolution.StartIndex, bestSolution.Length);
        }
    }
}
