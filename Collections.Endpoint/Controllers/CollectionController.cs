using Collections.Entities.Dtos.Collection;
using Collections.Logic.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        CollectionLogic logic;
        UserManager<IdentityUser> userManager;

        public CollectionController(CollectionLogic logic, UserManager<IdentityUser> userManager)
        {
            this.logic = logic;
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<CollectionViewDto> GetAllCollections()
        {
            return logic.GetAllCollections();
        }

        [HttpGet("{id}")]
        public CollectionViewDto GetCollectionById(string id)
        {
            return logic.GetCollectionById(id);
        }

        [HttpPost]
        [Authorize]
        public async Task CreateCollection(CollectionCreateUpdateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            logic.CreateCollection(dto, user.Id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task UpdateCollection(string id, [FromBody] CollectionCreateUpdateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            var collection = logic.GetCollectionById(id);

            if (collection.UserId == user.Id)
            {
                logic.UpdateCollection(id, dto);
            }
            else
            {
                throw new ArgumentException("You are not allowed to update someone else's collection!");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteCollection(string id)
        {
            var user = await userManager.GetUserAsync(User);

            var collection = logic.GetCollectionById(id);

            if (collection.UserId == user.Id)
            {
                logic.DeleteCollectionById(id);
            }
            else
            {
                throw new ArgumentException("You are not allowed to delete someone else's collection!");
            }
        }

        [HttpGet("search/{query}")]
        public IEnumerable<CollectionSearchViewDto> GetCollectionsBySearch(string query)
        {
            return logic.GetCollectionsBySearch(query);
        }

        [HttpGet("categorysearch/{query}")]
        public IEnumerable<CollectionSearchViewDto> GetCollectionsByCategorySearch(string query)
        {
            return logic.GetCollectionsByCategorySearch(query);
        }
    }
}
