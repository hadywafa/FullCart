using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Products.Queries.GetProductsBref;
using FluentValidation;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductValidator :AbstractValidator<ProductBrefDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(5);
        }
    }
}
