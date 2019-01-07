using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DictionaryAPITest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //try
            //{
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_id", "e8e7ecee");
            client.DefaultRequestHeaders.Add("app_key", "610cfa0b0947d48788edd521c5542dfb");

            string word = "king";

            string pull = await client.GetStringAsync($"https://od-api.oxforddictionaries.com:443/api/v1/entries/en/{word.ToLower()}").ConfigureAwait(false);
            var definitionObject = JsonConvert.DeserializeObject<WordDef>(pull);
            List<string> definitions = new List<string>();
            if (definitionObject.Results != null)
            {
                foreach (var result in definitionObject.Results)
                {
                    if (result.lexicalEntries != null)
                    {
                        foreach (var lexicalEntry in result.lexicalEntries)
                        {
                            if (lexicalEntry.entries != null)
                            {
                                foreach (var entry in lexicalEntry.entries)
                                {
                                    if (entry.senses != null)
                                    {
                                        foreach (var sense in entry.senses)
                                        {                                            
                                            if (sense.definitions != null && sense.definitions.Length > 0 && sense.definitions[0] != null)
                                            {
                                                definitions.Add(sense.definitions[0]);
                                                Console.WriteLine(sense.definitions[0]);
                                                Console.WriteLine("");
                                            }
                                            if (sense.subsenses != null)
                                            {
                                                foreach (var subsense in sense.subsenses)
                                                {
                                                    if (subsense.definitions != null && subsense.definitions.Length > 0 && subsense.definitions[0] != null)
                                                    {
                                                        definitions.Add(subsense.definitions[0]);
                                                        Console.WriteLine(subsense.definitions[0]);
                                                        Console.WriteLine("");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //foreach (var result in definitionObject.Results)
            //{
            //    Console.WriteLine($"Word: {result.word}");
            //    foreach (var lexicalEntry in result.lexicalEntries)
            //    {
            //        Console.WriteLine($"\tLexical Category: {lexicalEntry.lexicalCategory}");
            //        
            //        {
            //            Console.WriteLine("\t\tEtymologies:");
            //            if (entry.etymologies != null)
            //            {
            //                foreach (var etymology in entry.etymologies)
            //                {
            //                    Console.WriteLine($"\t\t - {etymology}");
            //                }
            //            }

            //            Console.WriteLine();
            //            Console.WriteLine("\t\tSenses:");
            //            if (entry.senses == null)
            //            {
            //                continue;
            //            }
            //            foreach (var sense in entry.senses)
            //            {
            //                if (sense.definitions[0] != null)
            //                {
            //                    Console.WriteLine($"\t\t - {sense.definitions[0]}");
            //                }
            //                if (sense.short_definitions[0] != null)
            //                {
            //                    Console.WriteLine($"\t\t\tShort Definition: {sense.short_definitions[0]}");
            //                }

            //                Console.WriteLine();
            //                Console.WriteLine("\t\t\tSubsenses");
            //                if (sense.subsenses == null)
            //                {
            //                    continue;
            //                }
            //                foreach (var subsense in sense.subsenses)
            //                {
            //                    if (subsense.definitions[0] != null)
            //                    {
            //                        Console.WriteLine($"\t\t\t - {subsense.definitions[0]}");
            //                    }
            //                    if (subsense.short_definitions[0] != null)
            //                    {
            //                        Console.WriteLine($"\t\t\t\tShort Definition: {subsense.short_definitions[0]}");
            //                    }
            //                }
            //            }

            //        }
            //    }
            //}
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //var x = definitionObject.Results.FirstOrDefault()?.lexicalEntries.FirstOrDefault()?.entries.FirstOrDefault()?.senses.FirstOrDefault();
            //var definitions = x.definitions != null && x.definitions.Length > 0 ? x.definitions : x.subsenses.FirstOrDefault()?.definitions;
            Console.ReadKey();
        }
    }
}
