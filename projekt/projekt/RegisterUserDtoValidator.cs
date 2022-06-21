using FluentValidation;
using projekt.Entity;

namespace projekt
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(LibraryDbContext dbcontext)
        {

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInuse = dbcontext.Users.Any(u => u.Email == value);
                if (emailInuse)
                {
                    context.AddFailure("Email", "Email jest już zajęty");
                }

            });

        }
    }
}

