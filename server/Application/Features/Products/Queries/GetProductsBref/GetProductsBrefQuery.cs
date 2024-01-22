using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetProductsBref
{

    public class GetProductsBrefQuery : IRequest<List<ProductBrefDto>>
    {
    }

    public class GetProductsBrefQueryHandler : IRequestHandler<GetProductsBrefQuery,List<ProductBrefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsBrefQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductBrefDto>> Handle(GetProductsBrefQuery request, CancellationToken cancellationToken)
        {
            var allPosts = await _context.Products.ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<ProductBrefDto>>(allPosts);
        }
    }
}
