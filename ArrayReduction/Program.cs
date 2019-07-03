using System;
using System.Collections.Generic;

namespace ArrayReduction
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"3
3
10 40 20
5
5 10 40 30 20
42
468 335 1 170 225 479 359 463 465 206 146 282 328 462 492 496 443 980 437 392 105 403 154 293 383 422 217 219 396 448 227 272 39 370 413 168 300 36 395 204 312 323";

            input = input.Replace("\r", "");
            Test[] tests = parseInput(input);

            foreach (Test T in tests)
            {
                Console.WriteLine("----ORIGINAL----");
                T.print();
                //T.reduce();
                T.fastReduce();
                Console.WriteLine("----REDUCED-----");
                T.print();
            }
        }

        public static Test[] parseInput(string input)
        {
            string[] inputs = input.Split("\n");

            int num_of_tests = Int32.Parse(inputs[0]);
            Test[] tests = new Test[num_of_tests];

            int testNumber = 0;
            Test T = null;

            for (int i = 1; i < inputs.Length; i++)
            {
                if (i % 2 == 1) //Odd
                {
                    T = new Test(Int32.Parse(inputs[i]));
                }
                else
                {
                    if (T != null)
                    {
                        string[] numbers = inputs[i].Split(" ");
                        for (int j = 0; j < numbers.Length; j++)
                        {
                            T.numbers[j] = Int32.Parse(numbers[j]);
                        }
                    }
                    tests[testNumber] = T;
                    testNumber++;
                }
            }

            return tests;
        }
    }

    class Test
    {
        public int[] numbers;

        public Test(int size)
        {
            numbers = new int[size];
        }

        public int getLargestPosition() // O(n)
        {
            int largest = int.MinValue;
            int position = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (largest < numbers[i])
                {
                    largest = numbers[i];
                    position = i;
                }
            }
            return position;
        }

        public void reduce() // O(n^2)
        {
            int[] reduced = new int[numbers.Length];
            int remaining = numbers.Length - 1;
            for (int i = 0; i < numbers.Length; i++)
            {
                int position = getLargestPosition();
                reduced[position] = remaining;
                remaining--;
                numbers[position] = int.MinValue;
            }
            numbers = reduced;
        }

        public void fastReduce()
        {
            SortedDictionary<int, int> dictionary = new SortedDictionary<int, int>();
            int position = 0;
            foreach (int number in numbers)
            {
                dictionary.Add(number, position);
                position++;
            }

            int size = 0;
            foreach (KeyValuePair<int, int> item in dictionary)
            {
                numbers[item.Value] = size;
                size++;
            }

        }

        public void print()
        {
            string result = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                if (i == numbers.Length - 1)
                {
                    result += numbers[i];
                }
                else
                {
                    result += (numbers[i] + " ");
                }
            }
            Console.WriteLine(result);
        }
    }
}


