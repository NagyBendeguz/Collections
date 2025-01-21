using Collections.Data;
using Collections.Entities.Dtos.Category;
using Collections.Entities.Dtos.Collection;
using Collections.Entities.Dtos.Transaction;
using Collections.Entities.Models;
using Collections.Logic.Helpers;

namespace Collections.Logic.Logic
{
    public class CollectionLogic
    {
        Repository<Collection> repo;
        DtoProvider dtoProvider;

        public CollectionLogic(Repository<Collection> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public IEnumerable<CollectionViewDto> GetAllCollections()
        {
            return repo.GetAll().Select(c => dtoProvider.Mapper.Map<CollectionViewDto>(c));
        }

        public CollectionViewDto GetCollectionById(string id)
        {
            var model = repo.GetById(id);
            return dtoProvider.Mapper.Map<CollectionViewDto>(model);
        }

        public void CreateCollection(CollectionCreateUpdateDto dto, string userId)
        {
            var model = dtoProvider.Mapper.Map<Collection>(dto);
            model.UserId = userId;
            repo.Create(model);
        }

        public void UpdateCollection(string collectionId, CollectionCreateUpdateDto dto)
        {
            var old = repo.GetById(collectionId);
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }

        public void DeleteCollectionById(string id)
        {
            repo.DeleteById(id);
        }

        public void ChangeCollectionAndItsObjectsOwner(TransactionViewDto dto)
        {
            var model = repo.GetById(dto.TypeId);
            model.UserId = dto.ReceiverUserId;
            model.Objects?.ForEach(o => o.UserId = dto.ReceiverUserId);
            repo.Update(model);
        }

        public IEnumerable<CollectionSearchViewDto> GetCollectionsBySearch(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Enumerable.Empty<CollectionSearchViewDto>();
            }

            query = query.ToLower().Trim();

            var collections = repo.GetAll()
                .Where(c => c.Name.ToLower().Contains(query) || c.Description.ToLower().Contains(query))
                .Select(c => new CollectionSearchViewDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Category = new CategoryViewDto
                    {
                        Id = c.Category.Id,
                        Name = c.Category.Name
                    },
                    Description = c.Description,
                    UserId = c.UserId
                })
                .ToList();

            return collections;
        }

        public IEnumerable<CollectionSearchViewDto> GetCollectionsByCategorySearch(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Enumerable.Empty<CollectionSearchViewDto>();
            }

            query = query.ToLower().Trim();

            var collections = repo.GetAll()
                .Where(c => c.Category.Name.Contains(query))
                .Select(c => new CollectionSearchViewDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Category = new CategoryViewDto
                    {
                        Id = c.Category.Id,
                        Name = c.Category.Name
                    },
                    Description = c.Description,
                    UserId = c.UserId
                })
                .ToList();

            return collections;
        }
    }
}
