using Collections.Entities.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collections.Entities.Models
{
    public class Rating : IIdEntity
    {
        public Rating(string objectId, string description, int rate)
        {
            Id = Guid.NewGuid().ToString();
            ObjectId = objectId;
            Description = description;
            Rate = rate;
        }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Range(1, 10)]
        public int Rate { get; set; }

        [StringLength(50)]
        public string ObjectId { get; set; }

        [NotMapped]
        public virtual Object? Object { get; set; }

        [StringLength(50)]
        public required string UserId { get; set; }
    }
}
