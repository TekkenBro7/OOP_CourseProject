using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain
{
    public class User
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string SecondName { get; private set; }
        public string Email { get; private set; }
        public List<Task> Tasks { get; private set; }
        public User(string login, string password, string name, string secondName, string email)
        {
            Login = login;
            Password = password;
            Name = name;
            SecondName = secondName;
            Email = email;
            Tasks = new List<Task>();
        }
        public void AddDailyTask(Task task)
        {
            Tasks.Add(task);
        }
        public void RemoveDailyTask(Task task)
        {
            Tasks.Remove(task);
        }
        public List<Task> GetListsOfDailyTasks(DateTime date)
        {
            return Tasks.FindAll(task => task.CreateTime.Date == date.Date);
        }
        public Task zxc(string tite)
        {
            return Tasks.Find(task => task.Title == tite);
        }
    }
}
