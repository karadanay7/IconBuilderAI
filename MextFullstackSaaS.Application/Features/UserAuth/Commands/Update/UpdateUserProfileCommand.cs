using MediatR;
using MextFullstackSaaS.Application.Common.Models;

namespace MextFullstackSaaS.Application.Features.UserProfile.Commands
{
    public class UpdateUserProfileCommand : IRequest<ResponseDto<string>>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage{ get; set; }

        public UpdateUserProfileCommand(string userId, string firstName, string lastName, string email, string profileImage)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ProfileImage = profileImage;
        }
       
    }
}
