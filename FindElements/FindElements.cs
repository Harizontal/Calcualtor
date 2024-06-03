using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindElements
{
    public class FindElement
    {
        public double FindMaxElement(double[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException("Массив пуст");
            }

            double maxElement = double.MinValue;
            foreach (double element in array)
            {
                if (element > maxElement)
                {
                    maxElement = element;
                }
            }
            return maxElement;
        }

        public double FindMinPositiveElement(double[] array)
        {
            if (array.Length == 0 || !array.Any(x => x > 0))
            {
                throw new ArgumentException("Массив пуст или не содержит положительных элементов");
            }

            double minPositiveElement = double.MaxValue;
            foreach (double element in array)
            {
                if (element > 0 && element < minPositiveElement)
                {
                    minPositiveElement = element;
                }
            }
            return minPositiveElement;
        }

        public double FindNegativeElement(double[] array)
        {
            if (array.Length == 0 || !array.Any(x => x < 0))
            {
                throw new ArgumentException("Массив пуст или не содержит отрицательных элементов");
            }

            double negativeElement = double.MaxValue;
            foreach (double element in array)
            {
                if (element < 0 && element < negativeElement)
                {
                    negativeElement = element;
                }
            }
            return negativeElement;
        }
    }
}
