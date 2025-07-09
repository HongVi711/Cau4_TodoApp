using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Command
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteTaskCommand(string id)
        {
            Id = id;
        }
    }
}
