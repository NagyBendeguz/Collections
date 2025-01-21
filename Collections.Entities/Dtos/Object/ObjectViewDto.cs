using Collections.Entities.Dtos.Rating;

namespace Collections.Entities.Dtos.Object
{
    public class ObjectViewDto
    {
        public string Id { get; set; } = "";

        public string Name { get; set; } = "";

        public string Value { get; set; } = "";

        public string TimeOfOrigin { get; set; } = "";

        public string PlaceOfOrigin { get; set; } = "";

        public bool IsItAWish { get; set; } = false;

        public required IEnumerable<RatingViewDto> Ratings { get; set; }

        public string CollectionId { get; set; } = "";

        public string UserId { get; set; } = "";

        public int RatingCount => Ratings?.Count() ?? 0;

        public double AverageRating => Ratings?.Count() > 0 ? Ratings.Average(r => r.Rate) : 0;
    }
}
