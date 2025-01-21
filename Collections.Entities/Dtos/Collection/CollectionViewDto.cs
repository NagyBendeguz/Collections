using Collections.Entities.Dtos.Category;
using Collections.Entities.Dtos.Object;

namespace Collections.Entities.Dtos.Collection
{
    public class CollectionViewDto
    {
        public string Id { get; set; } = "";

        public string Name { get; set; } = "";

        public required CategoryViewDto Category { get; set; }

        public string Description { get; set; } = "";

        public string UserId { get; set; } = "";

        public IEnumerable<ObjectViewDto>? Objects { get; set; }

        public int ObjectCount => Objects?.Count() ?? 0;

        public double CollectionRating => Objects?.Count() > 0 ? Objects.Average(o => o.AverageRating) : 0;
    }
}
