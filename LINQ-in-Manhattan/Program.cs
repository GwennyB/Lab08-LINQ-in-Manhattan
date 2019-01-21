using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using LINQ_in_Manhattan.Classes;

namespace LINQ_in_Manhattan
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ("../../../../data.json");
            Feature[] all = ReadFile(path);
            // use separate filtering queries
            GetAllNeighborhoods(all);
            Console.ReadLine();
            // use single LINQ query
            GetNeighborhoodsInOneQuery(all);
            Console.ReadLine();
            // use single query with lambda expressions
            GetNeighborhoodsUsingLambda(all);
            Console.ReadLine();

        }

        /// <summary>
        /// reads in a json file and converts contents to a JObject
        /// </summary>
        /// <param name="data"> 'features' list from json data </param>
        /// <returns> JSON object made from data file contents </returns>
        static Feature[] ReadFile(string path)
        {
            string source = File.ReadAllText(path);
            Manhattan data = JsonConvert.DeserializeObject<Manhattan>(source);
            return data.features;
        }

        /// <summary>
        /// gets a JSON object (from ReadFile), queries it for all neighborhoods, puts them into an array, and prints them to console
        /// filters results for blank neighborhoods, and re-prints them
        /// filters filtered results for duplicates, and re-prints them
        /// </summary>
        /// <param name="data"> 'features' list from json data </param>
        static void GetAllNeighborhoods(Feature[] data)
        {
            var results = from feature in data
                          select feature.properties.neighborhood;

            Console.WriteLine("ALL NEIGHBORHOODS - UNFILTERED");
            Console.WriteLine();
            foreach (var item in results)
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine();
            Console.WriteLine(" ==================================================================== ");


            var results2 = from neighborhood in results
                           where neighborhood != ""
                           select neighborhood;

            Console.WriteLine("ALL NEIGHBORHOODS - FILTERED FOR BLANKS");
            Console.WriteLine();
            foreach (var item in results2)
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine();
            Console.WriteLine(" ==================================================================== ");


            var results3 = from neighborhood in results2
                           group neighborhood by neighborhood
                           into g
                           select g.Key;

            Console.WriteLine("ALL NEIGHBORHOODS - FILTERED FOR DUPLICATES");
            Console.WriteLine();
            foreach (var item in results3)
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine();
            Console.WriteLine(" ==================================================================== ");

        }


        /// <summary>
        /// gets a JSON object (from ReadFile), queries it for all neighborhoods that aren't blanks or dupes, and prints them to console
        /// uses a single LINQ query to do all filtering
        /// </summary>
        /// <param name="data"> 'features' list from json data </param>
        static void GetNeighborhoodsInOneQuery(Feature[] data)
        {
            var results = from feature in data
                          where feature.properties.neighborhood != ""
                          group feature by feature.properties.neighborhood
                          into neighborhoods
                          select neighborhoods;
            Console.WriteLine();
            Console.WriteLine("ALL IN ONE QUERY USING LINQ");
            Console.WriteLine();
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Key}");
            }

        }

        /// <summary>
        /// gets a JSON object (from ReadFile), queries it for all neighborhoods that aren't blanks or dupes, and prints them to console
        /// uses a single query built from lambda expressions to do all filtering
        /// </summary>
        /// <param name="data"> 'features' list from json data </param>
        static void GetNeighborhoodsUsingLambda(Feature[] data)
        {
            var results = data.Where(nameof => nameof.properties.neighborhood.Length > 0)
                        .GroupBy(global => global.properties.neighborhood)
                        .Select(s => s.Key);
            Console.WriteLine();
            Console.WriteLine("ALL IN ONE QUERY USING LAMBDA EXPRESSIONS");
            Console.WriteLine();
            foreach (var item in results)
            {
                Console.WriteLine($"{item}");
            }
        }

    }
}
