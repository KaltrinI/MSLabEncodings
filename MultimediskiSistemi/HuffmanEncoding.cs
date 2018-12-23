using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Collections;

namespace MultimediskiSistemi
{
    class HuffmanEncoding
    {
        private static List<Node> nodes = new List<Node>();
        public static Node Root { get; set; }
        public static Dictionary<char, decimal> Frequencies = new Dictionary<char, decimal>();

        public static void Solve()
        {
            string source;

            while ((source=Console.ReadLine())!="done")
            {
                string[] parts = source.Split(" ");
                Frequencies.Add(parts[0][0], Decimal.Parse(parts[1], CultureInfo.InvariantCulture));
            }

            
            foreach (var symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    // Create a parent node by combining the frequencies
                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                Root = nodes.FirstOrDefault();

            }

            TraverseTree(Root);
            Console.ReadKey();
        }


        private static void TraverseTree(Node node)
        {
            if (node.Left == null && node.Right == null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (bool b in Encode(node.Symbol))
                    sb.Append(b ? 1:0);
                Console.WriteLine(node.Symbol + " " + sb.ToString());
            }
            else
            {
                TraverseTree(node.Left);
                TraverseTree(node.Right);
            }
                
        }


        public static  BitArray Encode(char c)
        {
            List<bool> encodedSource = new List<bool>();

            
                List<bool> encodedSymbol = Root.Traverse(c, new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }
    }


    public class Node
    {
        public char Symbol { get; set; }
        public decimal Frequency { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }

        public List<bool> Traverse(char symbol, List<bool> data)
        {
            // Leaf
            if (Right == null && Left == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = Left.Traverse(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
