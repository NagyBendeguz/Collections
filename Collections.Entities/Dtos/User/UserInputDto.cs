using System.ComponentModel.DataAnnotations;

namespace Collections.Entities.Dtos.User
{
    public class UserInputDto
    {
        [MinLength(5)]
        public required string UserName { get; set; } = "";

        [MinLength(8)]
        public required string Password { get; set; } = "";
    }
}
