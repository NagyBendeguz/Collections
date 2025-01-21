using System.ComponentModel.DataAnnotations;

namespace Collections.Entities.Dtos.Object
{
    public class ObjectCreateUpdateDto
    {
        [MaxLength(200)]
        public required string Name { get; set; } = "";

        [MaxLength(200)]
        public required string Value { get; set; } = "";

        [MaxLength(200)]
        public required string TimeOfOrigin { get; set; } = "";

        [MaxLength(200)]
        public required string PlaceOfOrigin { get; set; } = "";

        public required bool IsItAWish { get; set; } = false;

        [MaxLength(50)]
        public required string CollectionId { get; set; } = "";
    }
}
