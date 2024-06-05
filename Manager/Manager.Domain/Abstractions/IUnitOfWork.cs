using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Domain.Entities.Task> TaskRepository { get; }
        IRepository<SubTask> SubTaskRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Priority> PriorityRepository { get; }
        IRepository<Status> StatusRepository { get; }
        System.Threading.Tasks.Task SaveAllAsync();
        System.Threading.Tasks.Task DeleteDataBaseAsync();
        System.Threading.Tasks.Task CreateDataBaseAsync();
    }
}
