using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using HiraganeoCore;

namespace HiraganeoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var part in (Enum.GetValues(typeof(Hiraganeo.HiraganaParts)) as Hiraganeo.HiraganaParts[]))
            {
                Hiraganeo.EnabledSyllables.Add(part, true);
            }

        MENU:
            Console.Clear();
            Console.WriteLine("MENU:");
            Console.WriteLine("1. Hiragana basic");
            Console.WriteLine("2. Hiragana voiced");
            Console.WriteLine("3. Hiragana basic + voiced");
            Console.WriteLine("4. Options");
            Console.WriteLine("5. Quit");

            ConsoleKeyInfo cki = Console.ReadKey(true);

            switch (cki.Key)
            {
                case ConsoleKey.D1:
                    Console.Write(Hiraganeo.GenerateText(Hiraganeo.HiraganaBasic));
                    break;
                case ConsoleKey.D2:
                    Console.Write(Hiraganeo.GenerateText(Hiraganeo.HiraganaVoiced));
                    break;
                case ConsoleKey.D3:
                    Console.Write(Hiraganeo.GenerateText(Hiraganeo.HiraganaVoiced.Concat(Hiraganeo.HiraganaBasic)));
                    break;
                case ConsoleKey.D4:
                SUBMENU1:
                    Console.Clear();
                    char[] choices = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
                    Dictionary<char, Hiraganeo.HiraganaParts> choicesBinds = new Dictionary<char, Hiraganeo.HiraganaParts>();
                    var choiceNo = 0;
                    
                    Console.WriteLine("SYLLABELS:");
                    foreach (var part in (Hiraganeo.HiraganaParts[])Enum.GetValues(typeof(Hiraganeo.HiraganaParts)))
                    {
                        char choiceChar = choices[choiceNo++];
                        choicesBinds[choiceChar] = part;
                        Hiraganeo.EnabledSyllables.TryAdd(part, false);
                        Console.WriteLine($"{choiceChar}. {(Hiraganeo.EnabledSyllables[part] ? "+" : "-")}{part}");
                    }
                    char quitChoice = choices[choiceNo];
                    Console.WriteLine($"{quitChoice}. Quit");
                    ConsoleKeyInfo choice = Console.ReadKey(true);

                    if (choice.KeyChar == quitChoice)
                    {
                        goto MENU;
                    }

                    if (choicesBinds.ContainsKey(choice.KeyChar) && Hiraganeo.EnabledSyllables.ContainsKey(choicesBinds[choice.KeyChar]))
                    {
                        Hiraganeo.EnabledSyllables[choicesBinds[choice.KeyChar]] ^= true;
                    }
                    goto SUBMENU1;
                case ConsoleKey.D5:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

            Console.ReadKey(true);
            Console.Clear();

            goto MENU;
        }        
    }
}
