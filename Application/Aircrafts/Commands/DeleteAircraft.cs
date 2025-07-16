using MediatR;
using Domain;
using Persistence;

namespace Application.Aircrafts.Commands;

public class DeleteAircraft
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var aircraft = await context.Aircrafts.FindAsync([request.Id], cancellationToken)
                ?? throw new Exception("Aircraft not found");
            context.Aircrafts.Remove(aircraft);
            await context.SaveChangesAsync(cancellationToken);
            
        }
    }   
}