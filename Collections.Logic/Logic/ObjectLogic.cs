using Collections.Data;
using Collections.Entities.Dtos.Object;
using Collections.Entities.Dtos.Transaction;
using Collections.Logic.Helpers;

namespace Collections.Logic.Logic
{
    public class ObjectLogic
    {
        Repository<Entities.Models.Object> repo;
        DtoProvider dtoProvider;

        public ObjectLogic(Repository<Entities.Models.Object> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public IEnumerable<ObjectViewDto> GetAllObjects()
        {
            return repo.GetAll().Select(o => dtoProvider.Mapper.Map<ObjectViewDto>(o));
        }

        public ObjectViewDto GetObjectById(string id)
        {
            var model = repo.GetById(id);
            return dtoProvider.Mapper.Map<ObjectViewDto>(model);
        }

        public void CreateObject(ObjectCreateUpdateDto dto, string userId)
        {
            var model = dtoProvider.Mapper.Map<Entities.Models.Object>(dto);
            model.UserId = userId;
            repo.Create(model);
        }

        public void UpdateObject(string id, ObjectCreateUpdateDto dto)
        {
            var old = repo.GetById(id);
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }

        public void DeleteObjectById(string id)
        {
            repo.DeleteById(id);
        }

        public void ChangeObjectOwnerAndCollection(TransactionViewDto dto, string collectionId)
        {
            var model = repo.GetById(dto.TypeId);
            model.UserId = dto.ReceiverUserId;
            model.CollectionId = collectionId;
            repo.Update(model);
        }
    }
}
