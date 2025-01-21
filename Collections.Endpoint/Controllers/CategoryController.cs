using Collections.Entities.Dtos.Category;
using Collections.Entities.Dtos.Collection;
using Collections.Logic.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryLogic categoryLogic;
        CollectionLogic collectionLogic;

        public CategoryController(CategoryLogic categoryLogic, CollectionLogic collectionLogic)
        {
            this.categoryLogic = categoryLogic;
            this.collectionLogic = collectionLogic;
        }

        [HttpGet]
        public IEnumerable<CategoryViewDto> GetAllCategories()
        {
            return categoryLogic.GetAllCategories();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void CreateCategory(CategoryCreateUpdateDto dto)
        {
            var isFirst = categoryLogic.GetAllCategories().Count() == 0;

            if (isFirst)
            {
                categoryLogic.CreateCategory(new CategoryCreateUpdateDto { Name = "Default" });
            }

            categoryLogic.CreateCategory(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public void UpdateCategory(string id, CategoryCreateUpdateDto dto)
        {
            categoryLogic.UpdateCategory(id, dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteCategory(string id)
        {
            var collectionsWithThisCategory = collectionLogic.GetAllCollections().Where(c => c.Category.Id == id);

            var defaultCategoryId = categoryLogic.GetAllCategories().Where(c => c.Name == "Default").First().Id;

            if (defaultCategoryId == id)
            {
                throw new ArgumentException("You are not allowed to delete the 'Default' category!");
            }

            foreach (var collection in collectionsWithThisCategory)
            {
                collectionLogic.UpdateCollection(collection.Id, new CollectionCreateUpdateDto { CategoryId = defaultCategoryId, Description = collection.Description, Name = collection.Name });
            }

            categoryLogic.DeleteCategory(id);
        }
    }
}
