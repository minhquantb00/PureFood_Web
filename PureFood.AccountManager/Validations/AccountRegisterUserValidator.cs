using FluentValidation;
using PureFood.AccountCommands.Commands;
using PureFood.Common;

namespace PureFood.AccountManager.Validations
{
    public class AccountRegisterUserValidator : AbstractValidator<AccountRegisterUserCommand>
    {
        public AccountRegisterUserValidator()
        {
            RuleFor(x => x.EmailAddress).EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailAddress)).WithMessage("Invalid email address format.");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            When(x => !string.IsNullOrEmpty(x.PhoneNumber),
            () =>
            {
                RuleFor(x => x.PhoneNumber).NotEmpty()
                    .Must(phoneNumber => CommonUtility.IsMobile(ref phoneNumber))
                    .WithMessage("phone number is not valid");
            });
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(AccountRegisterUserCommand request)
        {
            var validationResult = new AccountRegisterUserValidator().Validate(request);
            return validationResult;
        }
    }
}
