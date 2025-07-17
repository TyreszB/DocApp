using MediatR;
using Domain;
using Persistence;

namespace Application.Discrepencies.Queries;

public class GetDiscrepencyDetails
{
    public class Query : IRequest<Discrepency>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Discrepency>
    {
        public async Task<Discrepency> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Discrepencies.FindAsync([request.Id], cancellationToken)
                ?? throw new Exception("Discrepency not found");
        }
    }
}       