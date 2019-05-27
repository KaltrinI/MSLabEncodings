using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MultimediskiSistemi
{
    class LZWEncoding
    {
        static Dictionary<string, int> dictionary = new Dictionary<string, int>();
        public static void Solve()
        {
            
            //string input = Console.ReadLine();
            string input = File.ReadAllText(Console.ReadLine());
            int i = 1;
            /*
            foreach (var c in input)
            {
                if (!dictionary.ContainsKey(c.ToString()))
                    dictionary.Add(c.ToString(), i++);
            }
            */
            dictionary.Add("/", i++);
            dictionary.Add("a", i++);
            dictionary.Add("b", i++);
            dictionary.Add("o", i++);
            dictionary.Add("w", i++);
            //https://www.youtube.com/watch?v=I7u43zqrROM
            int start = i;
            int strl = 2;
            for (int j = 0; j + strl <= input.Length;)
            {
                var str = input.Substring(j, strl);


                if (dictionary.ContainsKey(str))
                {
                    if (j + strl == input.Length)
                    {
                        dictionary.Add("str\n", i++);
                        break;
                    }
                    strl++;
                }
                    
                else
                {
                    j += strl - 1;
                    dictionary.Add(str, i++);
                    strl = 2;
                }
            }
            StringBuilder stringBuilder=new StringBuilder();
            while (start <= dictionary.Count)
            {
                var ind = GetIndex(start++);
                Console.Write(ind + " ");
                stringBuilder.Append(ind + " ");
            }
            //last character of last string
            var last = dictionary.First(x => x.Value == start - 1).Key;
            last = last.Substring(last.Length - 1);
            Console.WriteLine(dictionary[last]);
            stringBuilder.Append(dictionary[last]);

            PrintDictionary();
            CompressionRatio(input,stringBuilder.ToString());
            Console.ReadKey();

        }

        private static void CompressionRatio(string input,string output)
        {
            Console.WriteLine("Compression ratio:" + input.Length * 1.0 / output.Length);
        }

        private static void PrintDictionary()
        {
            foreach (var item in dictionary)
                Console.WriteLine(item.Value + " " + item.Key);
        }

        private static int GetIndex(int i)
        {
            var str = dictionary.First(x => x.Value == i).Key;
            str = str.Substring(0, str.Length - 1);

            return dictionary[str];
        }

    }
}
