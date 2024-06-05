using Manager.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Persistense.Repository
{
    public class AuthManager
    {
        public IUnitOfWork _unitOfWork;
        public User? CurrentUser { get; private set; }
        public AuthManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CurrentUser = null;
        }
        public async System.Threading.Tasks.Task CreateUserAsync(User user)
        {
            var existingUser = await _unitOfWork.UserRepository.GetAllAsync();
            if (existingUser.Any(u => u.Login == user.Login))
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }
            await _unitOfWork.UserRepository.AddAsync(user);
        }
        public async System.Threading.Tasks.Task LogInAsync(string login, string pass)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            User? foundUser = users.FirstOrDefault(u => u.Login == login && u.Password == pass);
            if (foundUser != null)
            {
                CurrentUser = foundUser;
            }
            else
            {
                throw new ArgumentException("Неверный логин или пароль");
            }
        }
        public void LogOut()
        {
            CurrentUser = null;
        }
        public Task<User> GetCurrentUserAsync()
        {
            return System.Threading.Tasks.Task.FromResult(CurrentUser);
        }
        public async System.Threading.Tasks.Task UpdateUserAsync(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAllAsync();
            CurrentUser = user;
        }
    }
}
