using MediatR;
using Domain;

namespace Application.Users.Queries;


public class GetUserList
{
    public class Query : IRequest<List<User>> {}

    public class Handler(AppContext context) : IRequestHandler<Query, List<User>>
    {
        public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<User>());
        }
    }
}
