using Collections.Entities.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collections.Entities.Models
{
    public class Object : IIdEntity
    {
        public Object(string name, string value, string timeOfOrigin, string placeOfOrigin, bool isItAWish, string collectionId)
        {
            Id = Guid.NewGuid().ToString(); ;
            Name = name;
            Value = value;
            TimeOfOrigin = timeOfOrigin;
            PlaceOfOrigin = placeOfOrigin;
            IsItAWish = isItAWish;
            CollectionId = collectionId;
        }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Value { get; set; }

        [StringLength(200)]
        public string TimeOfOrigin {  get; set; }

        [StringLength(200)]
        public string PlaceOfOrigin { get; set; }

        public bool IsItAWish { get; set; }

        [NotMapped]
        public virtual ICollection<Rating>? Ratings { get; set; }

        public string CollectionId { get; set; }

        [NotMapped]
        public virtual Collection? Collection { get; set; }

        [StringLength(50)]
        public required string UserId { get; set; }
    }
}
