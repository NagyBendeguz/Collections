using Collections.Entities.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collections.Entities.Models
{
    public class Collection : IIdEntity
    {
        public Collection(string name, string description, string categoryId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
            CategoryId = categoryId;
        }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {  get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        [NotMapped]
        public virtual List<Object>? Objects { get; set; }

        [StringLength(50)]
        public required string UserId { get; set; }
    }
}
