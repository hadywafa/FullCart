using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Application.Features.Products.Queries;
using AutoMapper;
using Domain.EFModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Categories.Queries.GetProductCategoriesTree
{
    public class GetProductCategoriesTreeQuery : IRequest<List<CategoryDto>>
    {
        public int ParentCatId { get; set; }
    }

    public class GetProductCategoriesTreeQueryHandler : IRequestHandler<GetProductCategoriesTreeQuery, List<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public GetProductCategoriesTreeQueryHandler(IApplicationDbContext context , IMapper mapper , IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<List<CategoryDto>> Handle(GetProductCategoriesTreeQuery request, CancellationToken cancellationToken)
        {
            var pathStr = await ProductSharedMethods.GetProductCategoriesTreeString(request.ParentCatId , _configuration);
            
            var pathArr = pathStr.Split(',');

            var categoriesList = new List<Category>();
            foreach (var str in pathArr)
            {
                var cat = await _context.Categories.Where(x => x.Code == str).Include(x => x.Brands).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                categoriesList.Add(cat);
            }

            return _mapper.Map<List<CategoryDto>>(categoriesList);

        }
    }
}
