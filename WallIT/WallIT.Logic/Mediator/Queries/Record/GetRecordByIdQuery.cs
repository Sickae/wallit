using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Queries
{
    public class GetRecordByIdQuery : IRequest<RecordDTO>
    {
        public int Id { get; set; }
    }
}
