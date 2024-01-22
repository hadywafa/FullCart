using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Queries.GetReviewsByProductId
{
    public class GetReviewsByProductIdQuery : IRequest<List<ReviewDto>>
    {
        public int Id { get; set; }
    }
    public class  GetReviewsByProductIdQueryHandler : IRequestHandler<GetReviewsByProductIdQuery ,List<ReviewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewsByProductIdQueryHandler(IApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ReviewDto>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _context.Reviews.Where(x => x.Product.Id == request.Id).Include(x => x.Customer.User)
                .Include(x => x.Seller.User).ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<List<ReviewDto>>(reviews);
        }
    }
}
