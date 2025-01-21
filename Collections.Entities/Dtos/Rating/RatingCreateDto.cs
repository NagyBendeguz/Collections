using System.ComponentModel.DataAnnotations;

namespace Collections.Entities.Dtos.Rating
{
    public class RatingCreateDto
    {
        [MaxLength(50)]
        public required string ObjectId { get; set; } = "";

        [MaxLength(200)]
        public required string Description { get; set; } = "";

        [Range(1, 10)]
        public required int Rate { get; set; }
    }
}
