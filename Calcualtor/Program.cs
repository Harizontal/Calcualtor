using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculatorApp
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
            LogOperation("Сложение");
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            LogOperation("Вычитание");
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            LogOperation("Умножение");
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль не допускается");

            LogOperation("Деление");
            return a / b;
        }

        private void LogOperation(string operation)
        {
            if (logOperations && logger != null)
                logger.Write($"{operation} метод был вызван");
        }

    }
    public class FindElements
    {

        public double FindMaxElement(double[] array)
        {
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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите режим:");
            Console.WriteLine("1. Кальулятор");
            Console.WriteLine("2. Поиск значении в массиве");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch(keyInfo.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("\nВыберите метод:");
                    Console.WriteLine("1. Пошаговый");
                    Console.WriteLine("2. Ввод выражения");

                    int mode;
                    while (!int.TryParse(Console.ReadLine(), out mode) || mode < 1 || mode > 2)
                    {
                        Console.WriteLine("Неверный выбор. Пожалуйста, введите 1 или 2.");
                    }

                    Calculator calculator = new Calculator(new ConsoleLogger());

                    Console.WriteLine("Хотите ли вы вести журнал операций? (y/n)");
                    bool logOperations = Console.ReadLine().ToLower() == "y";

                    if (logOperations)
                        calculator = new Calculator(new ConsoleLogger(), logOperations);
                    else
                        calculator = new Calculator(null, logOperations);

                    switch (mode)
                    {
                        case 1:
                            StepByStepMode(calculator);
                            break;
                        case 2:
                            ExpressionMode(calculator);
                            break;
                    }
                break;
                case ConsoleKey.D2:
                    FindElements findElements = new FindElements();
                    double[] array = { 5, -2, 3, 0, -7, 1, 8 };
                    double maxElement = findElements.FindMaxElement(array);
                    double minPositiveElement = findElements.FindMinPositiveElement(array);
                    double negativeElement = findElements.FindNegativeElement(array);
                    Console.WriteLine($"\nМаксимальный элемент: {maxElement}");
                    Console.WriteLine($"Минимальный положительный элемент: {minPositiveElement}");
                    Console.WriteLine($"Минимальный отрицательный элемент: {negativeElement}");
                    Console.ReadKey();
                break;
            }
        }

        private static void StepByStepMode(Calculator calculator)
        {
            double a, b;
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите первое число:");
                    a = double.Parse(Console.ReadLine());

                    Console.WriteLine("Введите второе число:");
                    b = double.Parse(Console.ReadLine());

                    Console.WriteLine("Выберите операцию (+, -, *, /):");
                    char operation = Console.ReadLine()[0];

                    double result;
                    switch (operation)
                    {
                        case '+':
                            result = calculator.Add(a, b);
                            break;
                        case '-':
                            result = calculator.Subtract(a, b);
                            break;
                        case '*':
                            result = calculator.Multiply(a, b);
                            break;
                        case '/':
                            result = calculator.Divide(a, b);
                            break;
                        default:
                            throw new ArgumentException("Неверная операция");
                    }

                    Console.WriteLine($"Результат: {Math.Round(result, 4)}");
                    Console.ReadKey();

                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверное число. Пожалуйста, введите корректное число.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void ExpressionMode(Calculator calculator)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите выражение:");
                    string expression = Console.ReadLine();

                    if (!IsValidExpression(expression))
                    {
                        Console.WriteLine("Неверное выражение. Пожалуйста, введите корректное выражение.");
                        continue;
                    }

                    DataTable table = new DataTable();
                    double result = Convert.ToDouble(table.Compute(expression, ""));
                    Console.WriteLine($"Результат: {Math.Round(result, 4)}");
                    Console.ReadKey();

                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверное выражение. Пожалуйста, введите корректное выражение.");
                }
            }
        }

        private static bool IsValidExpression(string expression)
        {
            return Regex.IsMatch(expression, @"^\d+(\s*\d+)*\s*([-+*/]\s*\d+)*$");
        }
    }
}