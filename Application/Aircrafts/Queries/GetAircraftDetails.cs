using MediatR;
using Domain;
using Persistence;

namespace Application.Aircrafts.Queries;

public class GetAircraftDetails
{
    public class Query : IRequest<Aircraft>
        {
            public required string Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, Aircraft>
        {
            public async Task<Aircraft> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Aircrafts.FindAsync([request.Id], cancellationToken)
                    ?? throw new Exception("Aircraft not found");
            }
        }
}   