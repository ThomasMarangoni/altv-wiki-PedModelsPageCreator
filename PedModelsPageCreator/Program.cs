using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PedModelsPageCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("gtav-data-dumps/peds.json"))
            {
                Console.WriteLine("Couldn't find peds.json, you need to clone the git submodule before compiling.");
                Environment.Exit(1);
            }

            using var r = new StreamReader("peds.json");
            var json = r.ReadToEnd();
            var peds = JsonConvert.DeserializeObject<List<Ped>>(json);

            var gallery = File.CreateText("gallery.txt");
            gallery.WriteLine("<hovergallery>");
            foreach (var ped in peds)
            {
                gallery.WriteLine($"Image:{ped.Name.ToLower()}.png|'''Name:'''<br><code>{ped.Name.ToLower()}</code><br>'''Hash (Hex):<br>'''<code>0x{ped.HexHash}</code><br>'''Type:''' <br><code>{ped.Pedtype.ToLower()}</code><br>'''DLC:'''<br><code>{ped.DlcName.ToLower()}</code>");
            }
            gallery.WriteLine("</hovergallery>");
            gallery.WriteLine("Created with [https://github.com/DurtyFree/gta-v-data-dumps GTA V Data Dumps from DurtyFree]");
            gallery.Close();
            Console.WriteLine($"gallery.txt created for {peds.Count} peds.");


            var snippets = File.CreateText("snippets.txt");
            snippets.WriteLine("==Javascript==");
            snippets.WriteLine("<syntaxhighlight lang=\"javascript\">");
            snippets.WriteLine("let PedModel {");

            var i = 0;
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
            Console.WriteLine($"snippets.txt created for {peds.Count} peds.");

            Console.WriteLine("This tool is using data files from https://github.com/DurtyFree/gta-v-data-dumps");
        }
    }
}
