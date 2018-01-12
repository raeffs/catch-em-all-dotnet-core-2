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

        public bool IsClosed { get; set; }

        public bool IsSold { get; set; }

        public bool IsHidden { get; set; }

        public bool IsNotified { get; set; }

        public DateTimeOffset? Ends { get; set; }

        public decimal? BidPrice { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? FinalPrice { get; set; }

        public bool IsNew { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset Updated { get; set; }
    }
}
