using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;


namespace ShellScrape
{
    public class RequestController
    {
        public static async Task<string> GetUrlContent(string url)
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(url);
            return await response;
        }

        public static List<HtmlNode> ParseHtml(string html, string xpath)
        {
            var htmlContent = new HtmlDocument();
            htmlContent.LoadHtml(html);
            var nodes = 
                htmlContent.DocumentNode.SelectNodes(xpath).ToList();

            return nodes;

        }


    }
}