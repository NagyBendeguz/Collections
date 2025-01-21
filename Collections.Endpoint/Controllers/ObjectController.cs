using Collections.Entities.Dtos.Object;
using Collections.Logic.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectController : ControllerBase
    {
        ObjectLogic objectLogic;
        CollectionLogic collectionLogic;
        UserManager<IdentityUser> userManager;

        public ObjectController(ObjectLogic logic, CollectionLogic collectionLogic, UserManager<IdentityUser> userManager)
        {
            this.objectLogic = logic;
            this.collectionLogic = collectionLogic;
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<ObjectViewDto> GetAllObjects()
        {
            return objectLogic.GetAllObjects();
        }

        [HttpGet("{id}")]
        public ObjectViewDto GetObjectById(string id)
        {
            return objectLogic.GetObjectById(id);
        }

        [HttpPost]
        [Authorize]
        public async Task CreateObject(ObjectCreateUpdateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            var collection = collectionLogic.GetCollectionById(dto.CollectionId);

            if (collection.UserId == user.Id)
            {
                objectLogic.CreateObject(dto, user.Id);
            }
            else
            {
                throw new ArgumentException("You are not allowed to create the object in someone else's collection!");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task UpdateObject(string id, [FromBody] ObjectCreateUpdateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            var currentObject = objectLogic.GetObjectById(id);

            if (currentObject.UserId == user.Id)
            {
                objectLogic.UpdateObject(id, dto);
            }
            else
            {
                throw new ArgumentException("You are not allowed to update someone else's object!");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteObject(string id)
        {
            var user = await userManager.GetUserAsync(User);

            var currentObject = objectLogic.GetObjectById(id);

            if (currentObject.UserId == user.Id)
            {
                objectLogic.DeleteObjectById(id);
            }
            else
            {
                throw new ArgumentException("You are not allowed to delete someone else's object!");
            }
        }
    }
}
