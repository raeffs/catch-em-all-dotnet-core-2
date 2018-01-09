namespace Raefftec.CatchEmAll.Models
{
    public class QueryOverview
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public long Results { get; set; }

        public long OpenResults { get; set; }

        public long NewResults { get; set; }
    }
}
