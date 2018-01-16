using System.ComponentModel.DataAnnotations;

namespace Raefftec.CatchEmAll.Models
{
    public class Category
    {
        public long Id { get; set; }

        public int? Number { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
