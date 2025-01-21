using Collections.Entities.Dtos.Category;
using System.ComponentModel.DataAnnotations;

namespace Collections.Entities.Dtos.Collection
{
    public class CollectionCreateUpdateDto
    {
        [MaxLength(200)]
        public required string Name { get; set; } = "";

        [MaxLength(200)]
        public required string Description { get; set; } = "";

        [MaxLength(50)]
        public required string CategoryId { get; set; } = "";
    }
}
