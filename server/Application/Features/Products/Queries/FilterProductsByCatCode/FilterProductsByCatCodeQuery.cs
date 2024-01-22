using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Application.Features.Products.Queries.GetProductsBref;
using AutoMapper;
using Domain.EFModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Products.Queries.FilterProductsByCatCode
{
    public class FilterProductsByCatCodeQuery : IRequest<List<ProductBrefDto>>
    {
        public string CatCode { get; set; }
    }
    
    public class FilterProductsByCatCodeQueryHandler : IRequestHandler<FilterProductsByCatCodeQuery ,List<ProductBrefDto> >
    {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IConfiguration _configuration;


            public FilterProductsByCatCodeQueryHandler(IApplicationDbContext context, IMapper mapper , IConfiguration configuration)
            {
                _context = context;
                _mapper = mapper;
                _configuration = configuration;
            }
        public async Task<List<ProductBrefDto>> Handle(FilterProductsByCatCodeQuery request, CancellationToken cancellationToken)
        {
            
            await _context.Categories.Where(x => x.Code == request.CatCode).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var proAll = await _context.Products
                .Include(x => x.ImagesGallery).Include(x => x.Brand).Include(x => x.Category)
                .Include(x => x.Seller).Include(x => x.Seller.User).Include(x => x.ProductHighlights)
                .Include(x => x.Specifications).Include(x => x.Orders).ToListAsync(cancellationToken: cancellationToken);

            List<Product> productLists = new List<Product>();
            foreach (Product pro in proAll)
            {
                var pathStr = await ProductSharedMethods.GetProductCategoriesTreeString(pro.Category.Id , _configuration);
                var pathArr = pathStr.Split(',').ToList();
                foreach (var str in pathArr)
                {
                    if (str == request.CatCode)
                    {
                        productLists.Add(pro);
                    }
                }
            }

            return _mapper.Map<List<ProductBrefDto>>( productLists);
        }

    }
}
