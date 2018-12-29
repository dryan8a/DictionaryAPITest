using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DictionaryAPITest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_id", "e8e7ecee");
            client.DefaultRequestHeaders.Add("app_key", "610cfa0b0947d48788edd521c5542dfb");

            string word = "ring";

            string pull = await client.GetStringAsync($"https://od-api.oxforddictionaries.com:443/api/v1/entries/en/{word.ToLower()}").ConfigureAwait(false);
            var definitionObject = JsonConvert.DeserializeObject<WordDef>(pull);

            foreach (var result in definitionObject.Results)
            {
                Console.WriteLine($"Word: {result.word}");
                foreach (var lexicalEntry in result.lexicalEntries)
                {
                    Console.WriteLine($"\tLexical Category: {lexicalEntry.lexicalCategory}");
                    foreach (var entry in lexicalEntry.entries)
                    {
                        Console.WriteLine("\t\tEtymologies:");
                        foreach (var etymology in entry.etymologies)
                        {
                            Console.WriteLine($"\t\t - {etymology}");
                        }

                        Console.WriteLine();

                        Console.WriteLine("\t\tSenses:");
                        foreach (var sense in entry.senses)
                        {
                            Console.WriteLine($"\t\t - {sense.definitions[0]}");
                            Console.WriteLine($"\t\t\tShort Definition: {sense.short_definitions[0]}");
                            Console.WriteLine();
                            Console.WriteLine("\t\t\tSubsenses");
                            if (sense.subsenses == null)
                            {
                                continue;
                            }
                            foreach(var subsense in sense.subsenses)
                            {
                                if (subsense.definitions == null || subsense.short_definitions == null)
                                {
                                    continue;
                                }
                                Console.WriteLine($"\t\t\t - {subsense.definitions[0]}");
                                Console.WriteLine($"\t\t\t\tShort Definition: {subsense.short_definitions[0]}");
                            }
                        }
                    }
                }
            }

            //var x = definitionObject.Results.FirstOrDefault()?.lexicalEntries.FirstOrDefault()?.entries.FirstOrDefault()?.senses.FirstOrDefault();
            //var definitions = x.definitions != null && x.definitions.Length > 0 ? x.definitions : x.subsenses.FirstOrDefault()?.definitions;
            Console.ReadKey();
        }
    }
}
