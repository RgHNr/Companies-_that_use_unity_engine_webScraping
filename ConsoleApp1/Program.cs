using System;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var web = new HtmlWeb(); 
            var document=web.Load("https://en.wikipedia.org/wiki/List_of_SpongeBob_SquarePants_episodes");

            var nodes = document.DocumentNode.SelectNodes("//*[@id='mw-content-text']/div[1]/table[1]/tbody/tr[2]");
            Console.WriteLine(nodes[0].InnerText.ToString());


        }   

        


        
    }


   
}
