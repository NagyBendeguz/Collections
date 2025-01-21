using System.ComponentModel.DataAnnotations;

namespace Collections.Entities.Dtos.Category
{
    public class CategoryCreateUpdateDto
    {
        [MaxLength(200)]
        public required string Name { get; set; } = "";
    }
}
