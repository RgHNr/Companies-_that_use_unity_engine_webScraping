using System;
using HtmlAgilityPack;
using CsvHelper;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var web = new HtmlWeb();
            

            List<GameInfo> GamesList = new();

            for (int i = 1; i <= 100; i++)
            {

                var document = web.Load($"https://1337x.to/FitGirl-torrents/{i}/");

                var link = document.DocumentNode.SelectNodes("//td[1]/a[@href]");

                foreach (var node in link)
                {
                    var href = node.Attributes["href"];
                    if (href != null && href.Value.Contains('a'))
                    {
                        //Console.WriteLine(href.Value);
                        string newUrl = $"https://1337x.to{href.Value}";

                        var newWeb = new HtmlWeb();
                        var newDocument = newWeb.Load(newUrl);
                        try
                        {
                            GameInfo gameInfo = new GameInfo()
                            {

                                GameName = newDocument.DocumentNode.SelectSingleNode("//*[@id='description']/p[5]/span[1]/strong").InnerText,
                                ReleaseDate = newDocument.DocumentNode.SelectSingleNode("//*[@id='description']/p[5]/text()[3]").InnerText,
                                Developer = newDocument.DocumentNode.SelectSingleNode("//*[@id='description']/p[5]/text()[5]").InnerText,
                                Publisher = newDocument.DocumentNode.SelectSingleNode("//*[@id='description']/p[5]/text()[6]").InnerText,
                                Engine = newDocument.DocumentNode.SelectSingleNode("//*[@id='description']/p[5]/text()[8]").InnerText,

                            };
                           
                            if (gameInfo.Engine == " Unity ")
                            {
                                GamesList.Add(gameInfo);
                                Console.WriteLine(gameInfo.ToString());

                            }
                        }
                        catch(Exception ex)
                        {
                            continue;
                        }                       
                    }
                }
            }

             using (var writer = new StreamWriter("C:\\Users\\PC\\Desktop\\WebScrapping\\WebScrapping\\CompaniesList.csv"))
              using(var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
              {
                Console.WriteLine("writing to file");
                  csv.WriteRecords(GamesList);
              }

        }
    }

    public class GameInfo
    {
        public string GameName { get; set; }
        public string ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Engine { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"{GameName} : is developed by {Developer} using {Engine} Engine, published by {Publisher} on {ReleaseDate}");
            return builder.ToString();
        }
    }
  
}
