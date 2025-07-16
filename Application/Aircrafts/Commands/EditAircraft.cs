using MediatR;
using Domain;
using Persistence;
using AutoMapper;

namespace Application.Aircrafts.Commands;

public class EditAircraft
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
        public required Aircraft Aircraft { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var aircraft = await context.Aircrafts.FindAsync([request.Id], cancellationToken)
                ?? throw new Exception("Aircraft not found");

            mapper.Map(request.Aircraft, aircraft);

            await context.SaveChangesAsync(cancellationToken);
        }
}

}