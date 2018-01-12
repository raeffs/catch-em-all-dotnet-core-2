namespace Reafftec.CatchEmAll.WebJobs.Models
{
    public class QueryParameters
    {
        public long Id { get; set; }

        public bool UseDescription { get; set; }

        public string WithAllTheseWords { get; set; }

        public string WithOneOfTheseWords { get; set; }

        public string WithExactlyTheseWords { get; set; }

        public string WithNoneOfTheseWords { get; set; }

        public int? Category { get; set; }

        public bool AutoFilterDeletedDuplicates { get; set; }
    }
}
