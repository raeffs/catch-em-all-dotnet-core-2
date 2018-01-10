using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raefftec.CatchEmAll.DAL
{
    public class Result : IHasIdentifier, IHasSoftDelete
    {
        public long Id { get; set; }

        public long ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public long QueryId { get; set; }

        [ForeignKey(nameof(QueryId))]
        public virtual Query Query { get; set; }

        public bool Closed { get; set; }

        public bool Sold { get; set; }

        public bool Hidden { get; set; }

        public bool Notified { get; set; }

        public DateTimeOffset? Ends { get; set; }

        public decimal? BidPrice { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? FinalPrice { get; set; }

        public bool New { get; set; }

        public bool Favorite { get; set; }

        public bool IsDeleted { get; set; }
    }
}
