﻿using System;

namespace Opdracht1 {
    class Program {
        static void Main(string[] args) {
            int numberCount = 0;
            int numberTotal = 0;

            int inputNumber;

            do {
                Console.Write("Geef getal {0}: ", numberCount + 1);
                inputNumber = int.Parse(Console.ReadLine());

                if (inputNumber != 0) {
                    numberCount++;
                    numberTotal += inputNumber;
                }
            } while (inputNumber != 0);

            double average = numberTotal / numberCount;

            Console.WriteLine("Het gemiddelde is {0}", average.ToString());
            Console.ReadKey();
        }
    }
}
