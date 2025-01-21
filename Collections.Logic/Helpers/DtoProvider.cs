using AutoMapper;
using Collections.Entities.Dtos.Category;
using Collections.Entities.Dtos.Collection;
using Collections.Entities.Dtos.Object;
using Collections.Entities.Dtos.Rating;
using Collections.Entities.Dtos.Transaction;
using Collections.Entities.Dtos.User;
using Collections.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Collections.Logic.Helpers
{
    public class DtoProvider
    {
        UserManager<IdentityUser> userManager;

        public Mapper Mapper { get; }

        public DtoProvider(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IdentityUser, UserViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.IsAdmin = userManager.IsInRoleAsync(src, "Admin").Result;
                });

                cfg.CreateMap<Category, CategoryViewDto>();

                cfg.CreateMap<CategoryCreateUpdateDto, Category>();

                cfg.CreateMap<Collection, CollectionViewDto>();

                cfg.CreateMap<Collection, CollectionSearchViewDto>();

                cfg.CreateMap<CollectionCreateUpdateDto, Collection>();

                cfg.CreateMap<Entities.Models.Object, ObjectViewDto>();

                cfg.CreateMap<ObjectCreateUpdateDto, Entities.Models.Object>();

                cfg.CreateMap<Rating, RatingViewDto>();

                cfg.CreateMap<RatingCreateDto, Rating>();

                cfg.CreateMap<Transaction, TransactionViewDto>();

                cfg.CreateMap<TransactionCreateDto, Transaction>();
            });

            Mapper = new Mapper(config);
        }
    }
}
