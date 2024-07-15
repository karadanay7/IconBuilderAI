using System.Data;
using FluentValidation;
using MextFullstackSaaS.Application.Common.FluentValidation.BaseValidators;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Features.UserProfile.Commands;


public class UpdateUserProfileCommandValidator : UserAuthValidatorBase<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator(IIdentityService identityService) : base(identityService)
    {
       
       
            // .EmailAddress().WithMessage("Email is not valid");

    
 RuleFor(x => x.ProfileImage)
            .NotEmpty().WithMessage("Image URL is required")
            .MaximumLength(255).WithMessage("Image URL must not exceed 255 characters");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

    
        
     

        // Additional rules for other fields as needed
    }

  
}
