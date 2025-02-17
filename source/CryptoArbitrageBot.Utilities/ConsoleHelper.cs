﻿using System.Text.RegularExpressions;

namespace CryptoArbitrageBot.Utilities;

public static class ConsoleHelper
{
    private const string Border = "\u2551";
    private const int Max = 20;

    public static void LoadingBar(int sec, string text = "updating data", int x = 1, int y = 7)
    {
        var thrSleep = sec * 1000 / Max;
        var empty = new string(' ', Max);
        
        Console.CursorVisible = false;
        Console.SetCursorPosition(1, 1);
        
        for (var i = 0; i < Max; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;// bar color
            Console.SetCursorPosition(x, y);
        
            for (var j = 0; j < i; j++)
            {
                Console.Write(Border);
            }
            
            Console.Write(empty + (i + 1) + $" / {Max} {text}...");
            empty = empty.Remove(empty.Length - 1);
            Thread.Sleep(thrSleep);
        }

        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void BeautifyWrite(string line, params int[] indices)
    {
        var matches = Regex.Matches(line, @"[\w\d_]+", RegexOptions.Singleline);

        for (var i = 0; i < matches.Count; i++)
        {
            var isPrint = false;
            foreach (var index in indices)
            {
                if (index != i) continue;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{matches[i]} ");
                isPrint = true;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            if(isPrint) continue;
            Console.Write($"{matches[i]} ");
        }
    }

    public static void Write(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    
    public static void WriteLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    
    public static string ReadSecretString()
    {
        var result = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && result.Length > 0)
            {
                Console.Write("\b \b");
                result = result[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                result += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        
        return result;
    }
}