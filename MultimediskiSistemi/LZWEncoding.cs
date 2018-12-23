using System;
using System.Collections.Generic;
using System.Linq;
namespace MultimediskiSistemi
{
    class LZWEncoding
    {
        static Dictionary<string, int> dictionary = new Dictionary<string, int>();
        public static void Solve()
        {
            
            string input = Console.ReadLine();
            int i = 1;

            foreach (var c in input)
            {
                if (!dictionary.ContainsKey(c.ToString()))
                    dictionary.Add(c.ToString(), i++);
            }


            int start = i;
            int strl = 2;
            for (int j = 0; j + strl <= input.Length;)
            {
                var str = input.Substring(j, strl);

                if (dictionary.ContainsKey(str))
                    strl++;
                else
                {
                    j += strl - 1;
                    dictionary.Add(str, i++);
                    strl = 2;
                }
            }

            while (start <= dictionary.Count)
                Console.Write(GetIndex(start++) + " ");

            //last character of last string
            var last = dictionary.First(x => x.Value == start - 1).Key;
            last = last.Substring(last.Length - 1);
            Console.Write(dictionary[last]);

            Console.ReadKey();

        }

        private static int GetIndex(int i)
        {
            var str = dictionary.First(x => x.Value == i).Key;
            str = str.Substring(0, str.Length - 1);

            return dictionary[str];
        }

    }
}
