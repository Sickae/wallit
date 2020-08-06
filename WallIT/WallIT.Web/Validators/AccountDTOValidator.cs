using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallIT.Shared.DTOs;

namespace WallIT.Web.Validators
{
    public class AccountDTOValidator : AbstractValidator<AccountDTO>
    {
        public AccountDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required!");
            RuleFor(x => x.Balance)
                .NotEmpty().WithMessage("Balance is required!")
                .Custom((Balance, context) => {
                    if (Balance <= 0)
                    {
                        context.AddFailure("The balance too small!");
                    }
                });
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required!");
            RuleFor(x => x.AccountType)
                .NotEmpty().WithMessage("AccountType is required!");
        }
    }
}
