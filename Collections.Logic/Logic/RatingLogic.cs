using Collections.Data;
using Collections.Entities.Dtos.Rating;
using Collections.Entities.Models;
using Collections.Logic.Helpers;

namespace Collections.Logic.Logic
{
    public class RatingLogic
    {
        Repository<Rating> repo;
        DtoProvider dtoProvider;

        public RatingLogic(Repository<Rating> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public RatingViewDto GetRatingById(string id)
        {
            var model = repo.GetById(id);
            return dtoProvider.Mapper.Map<RatingViewDto>(model);
        }

        public bool HasCurrentUserHasRatingForCurrentObject(RatingCreateDto dto, string userId)
        {
            var model = dtoProvider.Mapper.Map<Rating>(dto);

            return repo.GetAll().Any(r => r.UserId == userId && r.ObjectId == dto.ObjectId);
        }

        public void AddRating(RatingCreateDto dto, string userId)
        {
            var model = dtoProvider.Mapper.Map<Rating>(dto);
            model.UserId = userId;
            repo.Create(model);
        }
    }
}
