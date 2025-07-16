using MediatR;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Aircrafts.Queries;

public class GetAircrafts
{
    public class Query : IRequest<List<Aircraft>> {}
 
        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Aircraft>>
        {
            public async Task<List<Aircraft>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Aircrafts.ToListAsync(cancellationToken) 
                    ?? throw new Exception("Aircrafts not found");
            }
        }
}   