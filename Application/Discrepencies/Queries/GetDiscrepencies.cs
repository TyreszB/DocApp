using MediatR;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Discrepencies.Queries;

public class GetDiscrepencies
{
    public class Query : IRequest<List<Discrepency>>
    {
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Discrepency>>
    {
        public async Task<List<Discrepency>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Discrepencies.ToListAsync(cancellationToken)
                ?? throw new Exception("Discrepencies not found");
        }
    }
}               