using Collections.Entities.Dtos.Category;

namespace Collections.Entities.Dtos.Collection
{
    public class CollectionSearchViewDto
    {
        public string Id { get; set; } = "";

        public string Name { get; set; } = "";

        public required CategoryViewDto Category { get; set; }

        public string Description { get; set; } = "";

        public string UserId { get; set; } = "";
    }
}
