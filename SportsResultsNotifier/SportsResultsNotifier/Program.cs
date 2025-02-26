using HtmlAgilityPack;

namespace SportsResultsNotifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sports Results Notifier");
            var html = @"https://www.basketball-reference.com/boxscores/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            // Get collection of nodes that contain results.
            var resultsNodes = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div[*]/table[1]/tbody");
            
            foreach(var node in resultsNodes)
            {
                Console.WriteLine(node.InnerText);
            }

            

 
        }
    }
}
