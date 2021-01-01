using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PedModelsPageCreator
{
    class Program
    {
        static void Main(string[] args)
        {

            using (StreamReader r = new StreamReader("peds.json"))
            {
                string json = r.ReadToEnd();
                List<Ped> peds = JsonConvert.DeserializeObject<List<Ped>>(json);

                var gallery = File.CreateText("gallery.txt");
                gallery.WriteLine("<gallery>");
                foreach (var ped in peds)
                {
                    gallery.WriteLine($"Image:{ped.Name.ToLower()}.png|'''Name:'''<br><code>{ped.Name.ToLower()}</code><br>'''Hash (Hex):<br>'''<code>{ped.HexHash}</code><br>'''Type:''' <br><code>{ped.Pedtype}</code><br>'''DLC:'''<br><code>{ped.DlcName}</code>");
                }
                gallery.WriteLine("</gallery>");
                gallery.WriteLine("Created with [https://github.com/DurtyFree/gta-v-data-dumps GTA V Data Dumps from DurtyFree]");
                gallery.Close();
                Console.WriteLine($"Gallery created for {peds.Count} peds.");

                var snippets = File.CreateText("snippets.txt");
                snippets.WriteLine("==Javascript==");
                snippets.WriteLine("<syntaxhighlight lang=\"javascript\">");
                snippets.WriteLine("let PedModel {");

                int i = 0;
                foreach (var ped in peds)
                {
                    if(i < peds.Count-1)
                        snippets.WriteLine($"\t{ped.Name.ToLower()}: 0x{ped.HexHash},");
                    else
                        snippets.WriteLine($"\t{ped.Name.ToLower()}: 0x{ped.HexHash}");

                    i++;
                }
                snippets.WriteLine("};");
                snippets.WriteLine("</syntaxhighlight>");
                snippets.WriteLine("Created with [https://github.com/DurtyFree/gta-v-data-dumps GTA V Data Dumps from DurtyFree]");

                snippets.WriteLine("");
                snippets.WriteLine("==Typescript==");
                snippets.WriteLine("<syntaxhighlight lang=\"typescript\">");
                snippets.WriteLine("export enum PedModel {");

                i = 0;
                foreach (var ped in peds)
                {
                    if (i < peds.Count - 1)
                        snippets.WriteLine($"\t{ped.Name.ToLower()} = 0x{ped.HexHash},");
                    else
                        snippets.WriteLine($"\t{ped.Name.ToLower()}= 0x{ped.HexHash}");

                    i++;
                }
                snippets.WriteLine("};");
                snippets.WriteLine("</syntaxhighlight>");
                snippets.WriteLine("Created with [https://github.com/DurtyFree/gta-v-data-dumps GTA V Data Dumps from DurtyFree]");

                snippets.Close();
                Console.WriteLine($"Snippets created for {peds.Count} peds.");
            }
        }
    }
}
