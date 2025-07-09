using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Command
{
    public class GetAllTasksCommand : IRequest<IEnumerable<TaskResponseModel>>
    {
    }
}
