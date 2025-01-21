using Collections.Data;
using Collections.Entities.Dtos.Category;
using Collections.Entities.Models;
using Collections.Logic.Helpers;

namespace Collections.Logic.Logic
{
    public class CategoryLogic
    {
        Repository<Category> repo;
        DtoProvider dtoProvider;

        public CategoryLogic(Repository<Category> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public IEnumerable<CategoryViewDto> GetAllCategories()
        {
            return repo.GetAll().Select(c => dtoProvider.Mapper.Map<CategoryViewDto>(c));
        }

        public void CreateCategory(CategoryCreateUpdateDto dto)
        {
            var model = dtoProvider.Mapper.Map<Category>(dto);
            repo.Create(model);
        }

        public void UpdateCategory(string categoryId, CategoryCreateUpdateDto dto)
        {
            var old = repo.GetById(categoryId);
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }

        public void DeleteCategory(string categoryId)
        {
            repo.DeleteById(categoryId);
        }
    }
}
