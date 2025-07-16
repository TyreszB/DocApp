using MediatR;
using Domain;
using Persistence;

namespace Application.Aircrafts.Commands;

public class CreateAircraft
{
    public class Command : IRequest<Aircraft>
    {
        public required Aircraft Aircraft { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Aircraft>
    {
        public async Task<Aircraft> Handle(Command request, CancellationToken cancellationToken)
        {
            context.Aircrafts.Add(request.Aircraft);
            await context.SaveChangesAsync(cancellationToken);
            return request.Aircraft;
        }
    }
}