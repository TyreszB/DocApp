
using Domain;
using MediatR;
using Persistence;
using AutoMapper;

namespace Application.Users.Commands;

public class EditUser
{
    public class Command : IRequest
    {
        public required User User { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync([request.User.Id], cancellationToken) 
                ?? throw new Exception("User not found");

            mapper.Map(request.User, user);
            user.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
