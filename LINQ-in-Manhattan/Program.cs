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
            PrintNeighborhoods(path);
            Console.ReadLine();

        }

        
        static JObject ReadFile(string path)
        {
            using(StreamReader reader = File.OpenText(path))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
        }

        static void PrintNeighborhoods(string path)
        {
            JObject o = ReadFile(path);
            var results = from p in o["features"]
                          select (string)p["properties"]["neighborhood"];
            string[] neighborhoods = new string[50];
            int counter = 0;
            foreach (var item in results)
            {
                if (item != "" && !neighborhoods.Contains(item))
                {
                    if (neighborhoods.Length - counter < 2)
                    {
                        Array.Resize(ref neighborhoods, neighborhoods.Length + 10);
                    }
                    neighborhoods[counter] = item;
                    counter++;
                    Console.WriteLine(item);
                }
            }
        }
    }

    //string[] lines = File.ReadAllLines(path);

    //JObject o = JObject.Parse(lines[0]);
    //string neighborhood = (string)o["features"][0]["properties"]["neighborhoods"];
    //Console.WriteLine($"{neighborhood}");


    //JObject rss = JObject.Parse(json);

    //string rssTitle = (string)rss["channel"]["title"];
    //// James Newton-King

    //string itemTitle = (string)rss["channel"]["item"][0]["title"];
    //// Json.NET 1.3 + New license + Now on CodePlex

    //JArray categories = (JArray)rss["channel"]["item"][0]["categories"];
    //// ["Json.NET", "CodePlex"]

    //IList<string> categoriesText = categories.Select(c => (string)c).ToList();
    //// Json.NET
    //// CodePlex


}
