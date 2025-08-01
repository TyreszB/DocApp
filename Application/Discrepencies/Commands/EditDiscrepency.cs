using MediatR;
using Domain;
using Persistence;
using AutoMapper;

namespace Application.Discrepencies.Commands;

public class EditDiscrepency
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
        public required Discrepency Discrepency { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var discrepancy = await context.Discrepencies.FindAsync([request.Id], cancellationToken)
                ?? throw new Exception("Discrepency not found");

            mapper.Map(request.Discrepency, discrepancy);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}           