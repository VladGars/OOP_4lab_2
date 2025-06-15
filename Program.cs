using System;
using System.Collections.Generic;
using System.Linq;

public abstract class TNumber
{
    public virtual int SumOfDigits() => 0;
    public virtual int GetFirstDigit() => 0;
    public virtual int GetLastDigit() => 0;
}

public class TIntNumber : TNumber
{
    private int _number;

    public TIntNumber(int number)
    {
        _number = Math.Abs(number);
    }

    public override int SumOfDigits()
    {
        int sum = 0;
        int num = _number;
        while (num > 0)
        {
            sum += num % 10;
            num /= 10;
        }
        return sum;
    }

    public override int GetFirstDigit()
    {
        if (_number == 0) return 0;
        int num = _number;
        while (num >= 10)
        {
            num /= 10;
        }
        return num;
    }

    public override int GetLastDigit()
    {
        return _number % 10;
    }

    public override string ToString()
    {
        return _number.ToString();
    }
}

public class TRealNumber : TNumber
{
    private double _number;

    public TRealNumber(double number)
    {
        _number = Math.Abs(number);
    }

    private string GetDigitsAsString()
    {
        return _number.ToString("F2").Replace(",", "").Replace(".", "");
    }

    public override int SumOfDigits()
    {
        return GetDigitsAsString().Sum(c => (int)Char.GetNumericValue(c));
    }

    public override int GetFirstDigit()
    {
        string digits = GetDigitsAsString();
        if (string.IsNullOrEmpty(digits)) return 0;
        return (int)Char.GetNumericValue(digits[0]);
    }

    public override int GetLastDigit()
    {
        string digits = GetDigitsAsString();
        if (string.IsNullOrEmpty(digits)) return 0;
        return (int)Char.GetNumericValue(digits.Last());
    }

    public override string ToString()
    {
        return _number.ToString("F2");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Random random = new Random();
        var intNumbers = new List<TIntNumber>();
        var realNumbers = new List<TRealNumber>();

        for (int i = 0; i < 5; i++)
        {
            intNumbers.Add(new TIntNumber(random.Next(10, 1000)));
        }

        for (int i = 0; i < 7; i++)
        {
            realNumbers.Add(new TRealNumber(random.NextDouble() * 1000));
        }

        Console.WriteLine($"Цілі числа: {string.Join(", ", intNumbers)}");
        Console.WriteLine($"Дійсні числа: {string.Join(", ", realNumbers)}");

        int sumOfFirstDigits = intNumbers.Sum(num => num.GetFirstDigit());
        Console.WriteLine($"\nСума перших цифр цілих чисел: {sumOfFirstDigits}");

        int sumOfLastDigits = realNumbers.Sum(num => num.GetLastDigit());
        Console.WriteLine($"Сума останніх цифр дійсних чисел: {sumOfLastDigits}");
    }
}
