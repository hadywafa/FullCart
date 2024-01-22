using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Application.Features.Products.Queries.GetProductDetailsById;
using Application.Features.Products.Queries.GetProductsBref;
using AutoMapper;
using Domain.EFModels;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<string>
    {
        public ProductBrefDto ProductBrefDto { get; set; }
    }

    class CreateProductCommandHandler : IRequestHandler<CreateProductCommand , string>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler( IApplicationDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //var productTemp = new Product();
            var product = _mapper.Map<Product>(request.ProductBrefDto);

             _dbContext.Products.Add(product);

             await  _dbContext.SaveChangesAsync(cancellationToken);

            return "Product is Added Successfully";
        }
    }
}
