using Collections.Entities.Dtos.Rating;
using Collections.Logic.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingController : ControllerBase
    {
        RatingLogic logic;
        UserManager<IdentityUser> userManager;

        public RatingController(RatingLogic logic, UserManager<IdentityUser> userManager)
        {
            this.logic = logic;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task AddRating(RatingCreateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            if (!logic.HasCurrentUserHasRatingForCurrentObject(dto, user.Id))
            {
                logic.AddRating(dto, user.Id);
            }
            else
            {
                throw new ArgumentException("You are not allowed to comment multiple times on the same object!");
            }
        }
    }
}
