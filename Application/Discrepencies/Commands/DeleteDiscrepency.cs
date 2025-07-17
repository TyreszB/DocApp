using MediatR;
using Domain;
using Persistence;

namespace Application.Discrepencies.Commands;

public class DeleteDiscrepency
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var discrepancy = await context.Discrepencies.FindAsync([request.Id], cancellationToken)
                ?? throw new Exception("Discrepency not found");

            context.Discrepencies.Remove(discrepancy);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}                   