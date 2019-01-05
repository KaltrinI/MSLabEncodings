using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using System.IO;

namespace MultimediskiSistemi
{
    class ArithmeticEncoding
    {
        static string input;
        static decimal start = 0;
        static decimal lowerBound=0;
        static decimal upperBound = 1;
        static decimal domain;
        static Dictionary<char, (decimal lower,decimal upper,decimal probability)> dictionary = new Dictionary<char, (decimal lower,decimal upper, decimal probability)>();

        public static void Solve()
        {
            Console.WriteLine("Enter filename to encode");
            input = File.ReadAllText(Console.ReadLine());
            Dictionary<char, decimal> probability = new Dictionary<char, decimal>();

            Console.WriteLine("Enter character and probability of characters in format :\nchar probability\nWhen finished write done");
            string inp;
            while ((inp = Console.ReadLine()) != "done"){
                string[] parts = inp.Split(" ");
                probability.Add(parts[0].ElementAt(0), Decimal.Parse(parts[1],CultureInfo.InvariantCulture));
            }
            //initializing
            string sortedInput;
            StringBuilder sb = new StringBuilder();
            foreach (var item in probability.OrderByDescending(x => x.Value).ThenByDescending(x => x.Key))
                sb.Append(item.Key);

            sortedInput = sb.ToString();


            foreach (var c in sortedInput)
                if (!dictionary.ContainsKey(c))
                {
                    var pos = start + probability[c];
                    dictionary.Add(c, (start,pos,probability[c]));
                    start = pos;
                }
            
            foreach (var c in input)
            {
                if (c == input.Last())
                    break;

                lowerBound = dictionary[c].lower;
                upperBound = dictionary[c].upper;
                domain = upperBound - lowerBound;
                start = lowerBound;

                foreach (var t in sortedInput) {

                    dictionary[t] = (start, start+ domain * dictionary[t].probability,dictionary[t].probability);
                    start = dictionary[t].upper ;

                }
            }

            Console.WriteLine(dictionary[input.Last()].lower + " " + dictionary[input.Last()].upper);
            Console.ReadKey();

        }
        
    }
}
