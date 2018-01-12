using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Reafftec.CatchEmAll.WebJobs.Models
{
    public class Query
    {
        private static readonly string ResultsQuery = ".//ul[contains(concat(' ', normalize-space(@class), ' '), ' ric-normal-offers ')][1]/li[contains(concat(' ', normalize-space(@class), ' '), ' container-fluid ')]";

        private readonly HtmlNode rootNode;

        public Query(HtmlNode rootNode)
        {
            this.rootNode = rootNode;
        }

        public IEnumerable<ResultSummary> Results
        {
            get { return this.rootNode.SelectNodes(ResultsQuery)?.Select(n => new ResultSummary(n)) ?? new ResultSummary[] { }; }
        }
    }
}
