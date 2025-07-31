using System;
using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

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
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
            
            if (user == null)
                throw new Exception("User not found");
                
            return user;
        }
    }
}
