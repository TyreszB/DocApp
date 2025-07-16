using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users.Queries;

public class GetUserDetail
{
    public class Query : IRequest<User> {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, User>
    {
        public async Task<User> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Users.FindAsync([request.Id], cancellationToken)
                ?? throw new Exception("User not found");
        }
    }
}
