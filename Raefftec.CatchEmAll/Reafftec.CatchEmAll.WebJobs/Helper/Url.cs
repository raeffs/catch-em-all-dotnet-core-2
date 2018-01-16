using System;
using System.Collections.Generic;
using System.Linq;
using Reafftec.CatchEmAll.WebJobs.Models;

namespace Reafftec.CatchEmAll.WebJobs.Helper
{
    public static class Url
    {
        public static string FromQueryParameters(QueryParameters parameters)
        {
            var values = new Dictionary<string, string>();
            values.Add(UseDescriptionKey, parameters.UseDescription ? bool.TrueString : bool.FalseString);
            values.Add(WithAllTheseWordsKey, parameters.WithAllTheseWords);
            values.Add(WithOneOfTheseWordsKey, parameters.WithOneOfTheseWords);
            values.Add(WithExactlyTheseWordsKey, parameters.WithExactlyTheseWords);
            values.Add(WithNoneOfTheseWordsKey, parameters.WithNoneOfTheseWords);

            if (parameters.Category.HasValue)
            {
                values.Add(CategoryKey, parameters.Category.Value.ToString());
            }

            var parts = values
                .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
                .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}");

            var queryString = string.Join("&", parts);

            return string.Format(QueryUrlPattern, queryString);
        }

        public static string FromResultParameters(ResultParameters parameters)
        {
            return string.Format(ResultUrlPattern, parameters.ExternalId);
        }

        private static readonly string QueryUrlPattern = "https://www.ricardo.ch/search/index/?SortingType=1&PageSize=120&{0}";
        private static readonly string ResultUrlPattern = "https://www.ricardo.ch/v/an{0}/";

        private static readonly string UseDescriptionKey = "UseDescription";
        private static readonly string WithAllTheseWordsKey = "SearchSentence";
        private static readonly string WithOneOfTheseWordsKey = "SearchOneOf";
        private static readonly string WithExactlyTheseWordsKey = "SearchFullMatch";
        private static readonly string WithNoneOfTheseWordsKey = "SearchExclude";
        private static readonly string CategoryKey = "CategoryNr";
    }
}
