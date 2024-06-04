using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateInt
{
    public class Generate
    {
        private Random random = new Random();
        public int GeneratePositive() => random.Next(0, 1001);
        public int GenerateNegative() => random.Next(-1001, 0);
        public int GenerateEven()
        {
            int value;
            while (true)
            {
                value = random.Next(0, 1001);
                if (value % 2 == 0)
                    break;
            }
            return value;
        }
        public int GenerateOdd()
        {
            int value;
            while (true)
            {
                value = random.Next(0, 1001);
                if (value % 2 == 1)
                    break;
            }
            return value;
        }
        public double GenerateNumber(int type)
        {
            switch (type)
            {
                case 1:
                    return GeneratePositive();
                case 2:
                    return GenerateNegative();
                case 3:
                    return GenerateEven();
                case 4:
                    return GenerateOdd();
                default:
                    throw new ArgumentException("Неверный тип числа");

            }
        }
    }

    public static class Extention
    {
        public static int GetRandom(this int value)
        {
            return 10;

         }
    }

}
