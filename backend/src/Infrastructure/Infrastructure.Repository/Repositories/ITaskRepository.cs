using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task CreateAsync(TaskItem task);
        Task DeleteAsync(string id);
        Task UpdateAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(string id);
    }
}
