using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raefftec.CatchEmAll.DAL
{
    public class Category : IHasIdentifier, IHasSoftDelete
    {
        public long Id { get; set; }

        public int? Number { get; set; }

        [Required]
        public string Name { get; set; }

        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual ICollection<Query> Queries { get; set; } = new HashSet<Query>();

        public bool IsDeleted { get; set; }
    }
}
