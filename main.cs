using System;

class Calculator
{
    private double memory = 0;
    private double currentValue = 0;
    private string lastOperation = "";

    public void Run()
    {
        Console.WriteLine("Калькулятор запущен. Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, M+, M-, MR, C (очистка), EXIT");
        
        while (true)
        {
            Console.Write($"Текущее значение: {currentValue} > ");
            string input = Console.ReadLine().Trim().ToUpper();
            
            if (input == "EXIT") break;
            if (input == "C") 
            {
                currentValue = 0;
                continue;
            }
            
            ProcessInput(input);
        }
    }

    private void ProcessInput(string input)
    {
        try
        {
            switch (input)
            {
                case "+": case "-": case "*": case "/": case "%":
                    lastOperation = input;
                    Console.Write("Введите число: ");
                    double number = double.Parse(Console.ReadLine());
                    PerformOperation(number);
                    break;
                    
                case "1/X": case "1/x":
                    if (currentValue == 0) throw new DivideByZeroException();
                    currentValue = 1 / currentValue;
                    break;
                    
                case "X^2": case "x^2":
                    currentValue *= currentValue;
                    break;
                    
                case "SQRT":
                    if (currentValue < 0) throw new ArgumentException("Невозможно извлечь корень из отрицательного числа");
                    currentValue = Math.Sqrt(currentValue);
                    break;
                    
                case "M+":
                    memory += currentValue;
                    Console.WriteLine($"Значение в памяти: {memory}");
                    break;
                    
                case "M-":
                    memory -= currentValue;
                    Console.WriteLine($"Значение в памяти: {memory}");
                    break;
                    
                case "MR":
                    currentValue = memory;
                    break;
                    
                default:
                    if (double.TryParse(input, out double value))
                    {
                        currentValue = value;
                    }
                    else
                    {
                        Console.WriteLine("Неизвестная операция");
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private void PerformOperation(double number)
    {
        switch (lastOperation)
        {
            case "+": currentValue += number; break;
            case "-": currentValue -= number; break;
            case "*": currentValue *= number; break;
            case "/": 
                if (number == 0) throw new DivideByZeroException();
                currentValue /= number; 
                break;
            case "%": currentValue %= number; break;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        calculator.Run();
    }
}