using Core.Domain.Entities;
using Infrastructure.Repository.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Command
{
    public class UpdateTaskCommand : UpdateTaskRequestModel,IRequest<TaskResponseModel>
    {
    
    }
}
