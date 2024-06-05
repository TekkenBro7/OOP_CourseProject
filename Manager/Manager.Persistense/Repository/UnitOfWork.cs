using Manager.Domain;
using Manager.Domain.Abstractions;
using Manager.Persistense.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Persistense.Repository
{
    public class MongoUnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext _context;
        private readonly Lazy<IRepository<User>> _userRepository;
        private readonly Lazy<IRepository<Domain.Entities.Task>> _taskRepository;
        private readonly Lazy<IRepository<SubTask>> _subTaskRepository;
        private readonly Lazy<IRepository<Comment>> _commentRepository;
        private readonly Lazy<IRepository<Category>> _categoryRepository;
        private readonly Lazy<IRepository<Priority>> _priorityRepository;
        private readonly Lazy<IRepository<Status>> _statusRepository;
        public MongoUnitOfWork(MongoDbContext context)
        {
            _context = context;
            _userRepository = new Lazy<IRepository<User>>(() => new MongoRepository<User>(_context.Users));
            _taskRepository = new Lazy<IRepository<Domain.Entities.Task>>(() => new MongoRepository<Domain.Entities.Task>(_context.Tasks));
            _subTaskRepository = new Lazy<IRepository<SubTask>>(() => new MongoRepository<SubTask>(_context.SubTasks));
            _commentRepository = new Lazy<IRepository<Comment>>(() => new MongoRepository<Comment>(_context.Comments));
            _categoryRepository = new Lazy<IRepository<Category>>(() => new MongoRepository<Category>(_context.Categories));
            _priorityRepository = new Lazy<IRepository<Priority>>(() => new MongoRepository<Priority>(_context.Priorities));
            _statusRepository = new Lazy<IRepository<Status>>(() => new MongoRepository<Status>(_context.Statuses));
        }
        public IRepository<User> UserRepository => _userRepository.Value;
        public IRepository<Domain.Entities.Task> TaskRepository => _taskRepository.Value;
        public IRepository<SubTask> SubTaskRepository => _subTaskRepository.Value;
        public IRepository<Comment> CommentRepository => _commentRepository.Value;
        public IRepository<Category> CategoryRepository => _categoryRepository.Value;
        public IRepository<Priority> PriorityRepository => _priorityRepository.Value;
        public IRepository<Status> StatusRepository => _statusRepository.Value;
        public System.Threading.Tasks.Task CreateDataBaseAsync()
        {
            // MongoDB создает базу данных автоматически при первом доступе к коллекции.
            return System.Threading.Tasks.Task.CompletedTask;
        }
        public System.Threading.Tasks.Task DeleteDataBaseAsync()
        {
            return _context._database.Client.DropDatabaseAsync(_context._database.DatabaseNamespace.DatabaseName);
        }
        public System.Threading.Tasks.Task SaveAllAsync()
        {
            // MongoDB автоматически сохраняет изменения, поэтому этот метод не требуется.
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
