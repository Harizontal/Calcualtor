using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_library
{
    public class Calculator
    {
        private readonly ILogger logger;
        private readonly bool logOperations;

        public Calculator(ILogger logger = null, bool logOperations = true)
        {
            this.logger = logger;
            this.logOperations = logOperations;
        }

        public double Add(double a, double b)
        {
            LogOperation($"Add: {a} + {b}");
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            LogOperation($"Subtract: {a} - {b}");
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            LogOperation($"Multiply: {a} * {b}");
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль не допускается");

            LogOperation($"Divide: {a} / {b}");
            return a / b;
        }

        public void LogOperation(string operation)
        {
            if (logOperations && logger != null)
                logger.Write($"Метод {operation}");
        }

    }

    public interface ILogger
    {
        void Write(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }


}

