using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetProductDetailsById
{
    public class GetProductDetailsByIdQuery : IRequest<ProductDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ProductDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductDetailsByIdQueryHandler(IApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(x => x.Id == request.Id)
                .Include(x => x.ImagesGallery).Include(x => x.Brand).Include(x => x.Category)
                .Include(x => x.Seller).Include(x => x.Seller.User).Include(x => x.ProductHighlights)
                .Include(x => x.Specifications).Include(x => x.Orders).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return _mapper.Map<ProductDetailsDto>(product);
        }
    }

}