using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raefftec.CatchEmAll.DAL
{
    public class Query : IHasIdentifier, IHasSoftDelete
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool UseDescription { get; set; }

        public string WithAllTheseWords { get; set; }

        public string WithOneOfTheseWords { get; set; }

        public string WithExactlyTheseWords { get; set; }

        public string WithNoneOfTheseWords { get; set; }

        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public NotificationMode NotificationMode { get; set; }

        public bool AutoFilterDeletedDuplicates { get; set; }

        public decimal? DesiredPrice { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Result> Results { get; set; } = new HashSet<Result>();
    }
}
