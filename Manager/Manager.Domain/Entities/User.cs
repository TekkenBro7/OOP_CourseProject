using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Manager.Domain.Entities
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public List<Task> Tasks { get; set; }
        public User() { Tasks = []; }
        public User(string login, string password, string name, string secondName, string email)
        {
            Login = login;
            Password = password;
            Name = name;
            SecondName = secondName;
            Email = email;
            Tasks = [];
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
        public Task GetTaskByTitle(string titel)
        {
            return Tasks.Find(task => task.Title == titel);
        }
    }
}
