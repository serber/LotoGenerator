using Loto.Core;
using Loto.Core.Random;
using System;
using System.Diagnostics;

namespace Loto.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var randomGenerator = new RandomGenerator();

            var gameGenerator = new RussianLotoGameGeneratorV2(randomGenerator);

            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 1; i++)
            {
                var data = gameGenerator.Generate();

                Print(data);
            }

            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
        }

        private static void Print(int[] data)
        {
            var ticket = new int[27];

            for(int i = 0; i < 5; i++)
            {
                ticket[GetIndex(data[i])] = data[i];
            }

            for (int i = 5; i < 10; i++)
            {
                ticket[GetIndex(data[i]) + 9] = data[i];
            }

            for (int i = 10; i < 15; i++)
            {
                ticket[GetIndex(data[i]) + 18] = data[i];
            }

            for(int i = 0; i < ticket.Length; i++)
            {
                Console.Write($"{ticket[i]}\t");

                if (i == 8 || i == 17)
                {
                    Console.WriteLine();
                }
            }
        }

        private static int GetIndex(int n)
        {
            var ten = n / 10;

            if (ten == 9)
            {
                return 8;
            }

            return ten;
        }
    }
}