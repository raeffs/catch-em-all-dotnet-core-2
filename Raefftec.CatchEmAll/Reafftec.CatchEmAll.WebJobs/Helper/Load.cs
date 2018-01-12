using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Reafftec.CatchEmAll.WebJobs.Models;

namespace Reafftec.CatchEmAll.WebJobs.Helper
{
    public static class Load
    {
        public static async Task<Query> QueryFromSourceAsync(QueryParameters parameters)
        {
            var url = Url.FromQueryParameters(parameters);
            var response = await WebRequest.Create(url).GetResponseAsync();
            var document = new HtmlDocument();
            document.Load(response.GetResponseStream());
            return new Query(document.DocumentNode);
        }
    }
}
