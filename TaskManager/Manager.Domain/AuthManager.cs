using System.Reflection.Metadata;

namespace Manager.Domain
{
    public class AuthManager
    {
        public List<User> users;
        public User? CurrentUser { get; private set; }
        public AuthManager()
        {
            users = new List<User>();
            CurrentUser = null;
        }
        public void CreateUser(User user)
        {
            if (users.Any(u => u.Login == user.Login)) 
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }
            users.Add(user);
        }
        public void LogIn(string login, string pass)
        {
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
    }
}
