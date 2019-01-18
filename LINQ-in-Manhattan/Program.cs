using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace LINQ_in_Manhattan
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ("../../../../data.json");

            DedupeNeighborhoods(path);
            GetNeighborhoodsInOneStep(path);
            Console.ReadLine();

        }

        /// <summary>
        /// reads in a json file and converts contents to a JObject
        /// </summary>
        /// <param name="path"> path to source json file </param>
        /// <returns> JSON object made from data file contents </returns>
        static JObject ReadFile(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));

                return o;
            }
        }

        /// <summary>
        /// gets a JSON object (from ReadFile), queries it for all neighborhoods, puts them into an array, and prints them to console
        /// </summary>
        /// <param name="path"> path to data file </param>
        /// <returns> array of neighborhoods </returns>
        static string[] GetAllNeighborhoods(string path)
        {
            JObject data = ReadFile(path);
            var results = from feature in data["features"]
                          select (string)feature["properties"]["neighborhood"];
            string[] neighborhoods = new string[50];
            int counter = 0;
            Console.WriteLine("ALL NEIGHBORHOODS - UNFILTERED");
            Console.WriteLine();
            foreach (var item in results)
            {
                if(counter > neighborhoods.Length-2)
                {
                    Array.Resize(ref neighborhoods, neighborhoods.Length +10);
                }
                neighborhoods[counter] = item;
                counter++;
                Console.WriteLine($"{item}");

            }
            Console.WriteLine();
            Console.WriteLine(" ==================================================================== ");
            Console.WriteLine();
            return neighborhoods;
        }

        /// <summary>
        /// gets array of neighborhoods, queries it for entries that aren't empty, puts them into an array, and prints them to console
        /// </summary>
        /// <param name="path"> path to data file </param>
        /// <returns> array of neighborhoods </returns>
        static string[] NeighborhoodsNoBlanks(string path)
        {
            string[] neighborhoods = GetAllNeighborhoods(path);
            var results = from item in neighborhoods
                          where item != ""
                          select item;

            string[] noBlanks = new string[neighborhoods.Length];
            int counter = 0;
            Console.WriteLine("ALL NEIGHBORHOODS - FILTERED FOR BLANKS");
            Console.WriteLine();
            foreach (var item in results)
            {
                noBlanks[counter] = item;
                counter++;
                Console.WriteLine($"{item}");

            }
            Console.WriteLine();
            Console.WriteLine(" ==================================================================== ");
            Console.WriteLine(); return noBlanks;
        }

        /// <summary>
        /// gets array of non-blank neighborhoods, queries it for entries that aren't duplicates, puts them into an array, and prints them to console
        /// </summary>
        /// <param name="path"> path to data file </param>
        /// <returns> array of neighborhoods </returns>
        static void DedupeNeighborhoods(string path)
        {
            string[] neighborhoods = NeighborhoodsNoBlanks(path);

            var results = from item in neighborhoods
                          group item by item;

            int counter = 0;
            string[] noDupes = new string[neighborhoods.Length];
            Console.WriteLine("ALL NEIGHBORHOODS - FILTERED FOR DUPLICATES");
            Console.WriteLine();
            foreach (var neighborhood in results)
            {
                neighborhoods[counter] = neighborhood.Key;
                counter++;
                Console.WriteLine($"{neighborhood.Key}");
            }
            Console.WriteLine();
            Console.WriteLine(" ==================================================================== ");
            Console.WriteLine();
        }

        /// <summary>
        /// gets a JSON object (from ReadFile), queries it for all neighborhoods that aren't blanks or dupes, and prints them to console
        /// </summary>
        /// <param name="path"> path to data file </param>
        static void GetNeighborhoodsInOneStep(string path)
        {
            JObject data = ReadFile(path);

            var results = from feature in data["features"]
                          where (string)feature["properties"]["neighborhood"] != ""
                          group feature by feature["properties"]["neighborhood"]
                          into neighborhoods
                          select neighborhoods;
            Console.WriteLine();
            Console.WriteLine("ALL IN ONE RESULT");
            Console.WriteLine();
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Key}");
            }

        }

        static void PrintAll(string[] strings)
        {
            foreach (string item in strings)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
