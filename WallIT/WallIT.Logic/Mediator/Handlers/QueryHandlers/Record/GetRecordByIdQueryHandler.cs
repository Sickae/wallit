using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Logic.Mediator.Queries;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Handlers.QueryHandlers
{
    public class GetRecordByIdQueryHandler : IRequestHandler<GetRecordByIdQuery, RecordDTO>
    {
        private readonly IRecordRepository _recordrepository; 
        public GetRecordByIdQueryHandler(IRecordRepository recordrepository)
        {
            _recordrepository = recordrepository;
        }

        public Task<RecordDTO> Handle(GetRecordByIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var record = _recordrepository.Get(request.Id);

            return Task.FromResult(record);
        }
    }
}
