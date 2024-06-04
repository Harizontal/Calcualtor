using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Calculator_library;
using FindElements;
using GenerateInt;

namespace CalculatorApp
{
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
                    FindElement findElement = new FindElement();
                    double[] array = new double[10];
                    Console.WriteLine("\nВведите пошагово 10 элементов массива:");
                    for (int i = 0; i < array.Length; i++)
                    {
                        while (true)
                        {
                            try
                            {
                                array[i] = double.Parse(Console.ReadLine());
                                break;

                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                    }
                    Console.WriteLine($"\nМаксимальный элемент: {findElement.FindMaxElement(array)}");
                    Console.WriteLine($"Минимальный положительный элемент: {findElement.FindMinPositiveElement(array)}");
                    Console.WriteLine($"Минимальный отрицательный элемент: {findElement.FindNegativeElement(array)}");
                    Console.ReadKey();
                break;
            }
        }
        private static void StepByStepMode(Calculator calculator)
        {
            double a, b;
            Console.WriteLine("Сгенерировать числа? (y/n):");
            Generate generate = new Generate();
            bool checkGenerate = Console.ReadLine().ToLower() == "y";
            while (true)
            {
                try
                {
                    if (checkGenerate)
                    {
                        int typeGenerate;
                        Console.WriteLine("Для первого числа: \n 1.Положительный \n 2. Отрицательный \n 3. Четный \n 4. Нечетный");
                        typeGenerate = int.Parse(Console.ReadLine());
                        a = generate.GenerateNumber(typeGenerate);

                        Console.WriteLine("Для второго числа: \n 1.Положительный \n 2. Отрицательный \n 3. Четный \n 4. Нечетный");
                        typeGenerate = int.Parse(Console.ReadLine());
                        b = generate.GenerateNumber(typeGenerate);

                    }
                    else
                    {
                        Console.WriteLine("Введите первое число:");
                        a = double.Parse(Console.ReadLine());

                        Console.WriteLine("Введите второе число:");
                        b = double.Parse(Console.ReadLine());
                    }
                    

                    Console.WriteLine("Выберите операцию (+, -, *, /):");
                    char operation = char.Parse(Console.ReadLine());

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
            calculator.LogOperation("ExpressionMode");
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