using MediatR;
using System.Collections.Generic;

namespace WallIT.Logic.Mediator.Queries
{
    public class IdentityOptionsQuery : IRequest<IList<string>>
    { }
}
