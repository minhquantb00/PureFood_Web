using FluentValidation;
using PureFood.AccountCommands.Commands;
using PureFood.EnumDefine;

namespace PureFood.AccountManager.Validations
{
    public class AccountLoginValidator : AbstractValidator<AccountLoginCommand>
    {
        public AccountLoginValidator()
        {
            When(x => x.LoginProvider is ExternalLoginProviderEnum.Google
                    or ExternalLoginProviderEnum.Apple
                    or ExternalLoginProviderEnum.Microsoft
                    or ExternalLoginProviderEnum.Zalo
                , () =>
                {
                    RuleFor(x => x.ObjectId)
                        .NotEmpty()
                        ;
                });
            When(x => x.LoginProvider != ExternalLoginProviderEnum.Google
                      && x.LoginProvider != ExternalLoginProviderEnum.Apple
                      && x.LoginProvider != ExternalLoginProviderEnum.Microsoft
                      && x.LoginProvider != ExternalLoginProviderEnum.Zalo,
                () =>
                {
                    RuleFor(x => x.Email).NotEmpty();
                    RuleFor(x => x.Password).NotEmpty();
                });
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(AccountLoginCommand request)
        {
            var validationResult = new AccountLoginValidator().Validate(request);
            return validationResult;
        }
    }
}
