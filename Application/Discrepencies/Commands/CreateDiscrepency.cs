using MediatR;
using Domain;
using Persistence;

namespace Application.Discrepencies.Commands;

public class CreateDiscrepency
{
    public class Command : IRequest
    {
        public required Discrepency Discrepency { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            context.Discrepencies.Add(request.Discrepency);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}   