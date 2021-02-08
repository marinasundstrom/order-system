using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Application.Orders.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            //RuleFor(x => x.ListId)
            //    .NotNull()
            //    .NotEmpty().WithMessage("ListId is required.");

            //RuleFor(x => x.PageNumber)
            //    .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            //RuleFor(x => x.PageSize)
            //    .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
