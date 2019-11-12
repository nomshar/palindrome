using System;
using System.Linq;

namespace Palindrome
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //var query = "abcddcbah";
            //var query = "abcdbacefeh";
            //var query = "abcedffdecba";
            //var query = "aa";
            //var query = "aba";
            //var query = "abcedffdecbakllkgg";    
            //var query = "aaaabbaa";
            //var query = "iftiqtntfsrptugtiznorvonzj";
            //var query = "vnrtysfrzrmzlygfv";
            //var query = "wwwsdthzmlmbh";
            var query = "rfduvouaghh";
            var r = Algorithm.Search(query);
            if (r.Count > 0)
            {
                var longest = r.OrderByDescending(x => x.Length).First();
                Console.WriteLine(longest);
            }
        }
    }
}