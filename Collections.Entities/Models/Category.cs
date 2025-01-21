using Collections.Entities.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collections.Entities.Models
{
    public class Category : IIdEntity
    {
        public Category(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<Collection>? Collections { get; set; }
    }
}
