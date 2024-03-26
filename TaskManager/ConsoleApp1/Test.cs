using Manager.Domain;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Test
    {
        static void Main(string[] args)
        {
            AuthManager authManager = new AuthManager();
            List<Category> categories = new List<Category>()
            {
                new Category("личное"),
                new Category("учеба"),
                new Category("работа")
            };
            List<Priority> priorityList = new List<Priority>()
            {
                new Priority("Высокий"),
                new Priority("Средний"),
                new Priority("Низкий"),
                new Priority("Не определено")
            };
            List<Status> statusList = new List<Status>()
            {
                new Status("Ожидает выполнения"),
                new Status("Выполняется"),
                new Status("Выполнен")
            };
            while (true) 
            {
                Console.WriteLine("1 - пользователь создать, 2 - вывести всех, 3 - выбрать пользователя");
                int num = int.Parse(Console.ReadLine());
                if (num == 1) 
                {
                    authManager.CreateUser(new User
                    (
                        "zxc",
                        "123",
                        "John",
                        "Doe",
                        "john.doe@example.com"
                    ));
                }
                else if (num == 2) 
                {
                    foreach (User user in authManager.users)
                        Console.WriteLine(user.Name);
                }
                else if (num == 3)
                {
                    string a = Console.ReadLine();
                    string b = Console.ReadLine();
                    authManager.LogIn(a, b);
                    while (true)
                    {
                        Console.WriteLine("1 - список задач, 2 - создать задачу, ");
                        int n = int.Parse(Console.ReadLine());
                        if (n == 1)
                        {
                            foreach(Manager.Domain.Task task in authManager.CurrentUser.Tasks)
                            { 
                                Console.WriteLine(task.Title);
                                Console.WriteLine(task.Description);
                            }
                        }
                        else if (n == 2)
                        {
                            Manager.Domain.Task task = new Manager.Domain.Task("дота", "для задрота");
                            authManager.CurrentUser.Tasks.Add(task);
                            
                        }
                        else if (n == 3)
                        {
                            Manager.Domain.Task task = authManager.CurrentUser.zxc(Console.ReadLine());
                            task.AddSubTask(new SubTask("да", "купить амулет встать на базе"));
                            task.AddSubTask(new SubTask("нет", "удалить игру"));
                            for (int i = 0; i < task.SubTasks.Count; i++) 
                            {
                                Console.WriteLine(task.SubTasks[i].Title, task.SubTasks[i].Description);
                            }
                        }
                    }
                    
                }
            }
        }
    }
}
