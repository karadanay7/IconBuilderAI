using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.UserProfile.Commands
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, ResponseDto<string>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserProfileCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<string>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);

            if (user == null)
            {
                return new ResponseDto<string>
                {
                    Succeeded = false,
                    Message = "User not found"
                };
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.ProfileImage = request.ProfileImage;
            user.UserName  = request.Email;

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseDto<string>
            {
                Succeeded = true,
                Message = "Profile updated successfully"
            };
        }
    }
}
