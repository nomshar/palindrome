using System;
using System.Collections.Generic;
using System.Linq;

namespace Palindrome
{
    public class Algorithm
    {
        public class Point
        {
            public char Letter { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public Point(char letter, int x, int y)
            {
                Letter = letter;
                X = x;
                Y = y;
            }
        }
        
        public class Pair
        {
            public Point First { get; set; }
            public Point Second { get; set; }

            public double Slope => Second.X - First.X == 0
                ? 0d
                : (Second.Y - First.Y) / (Second.X - First.X);

            public Pair(Point first, Point second)
            {
                First = first;
                Second = second;
            }

            public override bool Equals(object obj)
            {
                var mirror = obj as Pair;
                if (mirror == null)
                {
                    return false;
                }

                return Math.Abs(this.Slope) == Math.Abs(mirror.Slope)
                       && First.Letter == mirror.Second.Letter
                       && Second.Letter == mirror.First.Letter;
            }

            public override int GetHashCode()
            {
                return First.GetHashCode() & Second.GetHashCode();
            }

            public override string ToString()
            {
                return string.Concat(First.Letter, Second.Letter);
            }
        }
        
        public static List<string> Search(string query)
        {
            if (query.Length < 2)
            {
                return new List<string>();
            }

            var pairs = new Pair[query.Length - 1];
            var i = 0;
            while (i < pairs.Length)
            {
                var first = new Point(query[i], i, (int)query[i]);
                var second = new Point(query[i+1], i+1, (int)query[i+1]);
                
                pairs[i] = new Pair(first, second);
                i++;
            }

            var r = FindPalindrome(pairs);

            return r;
        }

        private static List<string> FindPalindrome(Pair[] pairs)
        {
            var result = new List<string>();
            if (pairs.Length == 1)
            {
                var pair = pairs[0];
                if (pair.First.Letter == pair.Second.Letter)
                {
                    var p = BuildPalindrome(pairs, new List<Pair>(){pair}, new List<Pair>());
                    Console.WriteLine(p);
                    result.Add(p);
                    return result;
                }
            }
            
            var start = 0;
            var last = pairs.Length - 1;
            var i = 0;
            var vertex = new List<Pair>();
            vertex.Add(pairs[i]);
            i++;
            var vi = i;
            while (i < pairs.Length)
            {
                var prev = vertex[vi - 1];
                var next = pairs[i];
                if (Math.Sign(prev.Slope) != Math.Sign(next.Slope))
                {
                    // we found edge
                    // try to find mirror vertex
                    while (vertex.Count > 0)
                    {
                        var mirrorVertex = FindMirrorVertex(vertex, i, last, pairs);
                        if (mirrorVertex.Count > 0)
                        {
                            var p = BuildPalindrome(pairs, vertex, mirrorVertex);
                            Console.WriteLine(p);
                            result.Add(p);
                        }
                        else if (vertex.Count == 1 && vertex[0].First.Letter == vertex[0].Second.Letter)
                        {
                            var p = BuildPalindrome(pairs, vertex, new List<Pair>());
                            Console.WriteLine(p);
                            result.Add(p);
                        }
                        vertex.RemoveAt(0);
                    }

                    vertex.Clear();
                    vertex.Add(next);
                    i++;
                    vi = 1;
                    continue;
                }
                
                vertex.Add(next);
                i++;
                vi++;
            }

            return result;
        }

        private static List<Pair> FindMirrorVertex(List<Pair> vertex, int start, int last, Pair[] pairs)
        {
            var i = 0;
            var l = vertex[vertex.Count - 1].Second.X - vertex[0].First.X;
            
            var h = vertex[0].Slope > 0
                ? vertex[vertex.Count - 1].Second.Y - vertex[0].First.Y
                : vertex[0].First.Y - vertex[vertex.Count - 1].Second.Y;

            var mirrorPairIndex = vertex[vertex.Count-1].First.X + l;
            var result = new List<Pair>();
            while (i < vertex.Count)
            {
                var current = vertex[i];    
                
                if (mirrorPairIndex > pairs.Length - 1)
                {
                    return new List<Pair>();
                }

                var mirror = pairs[mirrorPairIndex];
                if (Math.Abs(current.Slope) == Math.Abs(mirror.Slope)
                    && current.First.Letter == mirror.Second.Letter
                    && current.Second.Letter == mirror.First.Letter    
                    )
                {
                    result.Insert(0, mirror);

                    if (current.Second.Equals(vertex[vertex.Count - 1].Second))
                    {
                        return result.Count == vertex.Count
                            ? result
                            : new List<Pair>();
                    }
                    
                    i++;
                    mirrorPairIndex--;
                    continue;
                }

                mirrorPairIndex++;
            }
            
            return new List<Pair>();
        }

        private static string BuildPalindrome(Pair[] pairs, List<Pair> firstVertex, List<Pair> lastVertex)
        {
            var s = "";
            for (int i = 0; i < firstVertex.Count; i++)
            {
                s += firstVertex[i].First.Letter;
            }

            if (lastVertex.Count == 0)
            {
                s += firstVertex[firstVertex.Count - 1].Second.Letter;
                return s;
            }
            
            if (firstVertex[firstVertex.Count - 1].Second.X != lastVertex[0].First.X)
            {
                for (int i = firstVertex[firstVertex.Count - 1].Second.X;
                    i < lastVertex[0].First.X; 
                    i++)
                {
                    s += pairs[i].First.Letter;
                }    
            }

            for (int i = 0; i < lastVertex.Count; i++)
            {
                s += lastVertex[i].First.Letter;
            }

            s += lastVertex[lastVertex.Count - 1].Second.Letter;
            return s;
        }
    }
}