
using Domain;
using MediatR;
using Persistence;

namespace Application.Users.Commands;

public class EditUser
{
    public class Command : IRequest
    {
        public required User User { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync([request.User.Id], cancellationToken) 
                ?? throw new Exception("User not found");
            user.Name = request.User.Name;
            user.Email = request.User.Email;
            user.Role = request.User.Role;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
