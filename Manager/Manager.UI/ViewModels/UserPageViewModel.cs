using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DnsClient;
using Manager.Persistense.Repository;
using Manager.UI.Pages;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Microcharts.Maui;
using Microcharts;
using System.Text.RegularExpressions;

namespace Manager.UI.ViewModels
{
    public partial class UserPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;
        public UserPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            var user = _authManager.CurrentUser;
            Username = user.Login;
            Email = user.Email;
            LoadTaskStatusChart();
            LoadTaskCategoryChart();
        }
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string newPassword;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        public async Taskk Save()
        {
            if (string.IsNullOrWhiteSpace(Username) || !IsValidLogin(Username))
            {
                ErrorMessage = "Логин должен быть не менее 5 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                if (NewPassword != ConfirmPassword)
                {
                    ErrorMessage = "Пароли не совпадают.";
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                    return;
                }

                if (!IsValidPassword(NewPassword))
                {
                    ErrorMessage = "Пароль должен быть не менее 8 символов, содержать хотя бы одну цифру и одну заглавную букву.";
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
            {
                ErrorMessage = "Некорректный формат электронной почты.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            var user = _authManager.CurrentUser;
            user.Login = Username;
            user.Email = Email;
            if (!string.IsNullOrEmpty(NewPassword))
            {
                user.Password = NewPassword;
            }
            await _authManager.UpdateUserAsync(user);
            var request = new NotificationRequest
            {
                NotificationId = 1000,
                Title = "Изменение",
                Subtitle = $"Данные успешно изменены",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(2),
                }
            };
            await LocalNotificationCenter.Current.Show(request);
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        [RelayCommand]
        public async Taskk LogOut()
        {
            _authManager.LogOut();
            await Shell.Current.GoToAsync("///LoginPage");
        }

        [ObservableProperty]
        private DonutChart taskStatusChart;
        private void LoadTaskStatusChart()
        {
            var user = _authManager.CurrentUser;
            var tasks = user.Tasks;
            var completedTasks = tasks.Count(t => t.status.TaskStatus == "Выполнено");
            var pendingTasks = tasks.Count(t => t.status.TaskStatus == "Ожидает");
            var inProgressTasks = tasks.Count(t => t.status.TaskStatus == "Выполняется");
            var overdueTasks = tasks.Count(t => t.Deadline < DateTime.Now.AddMinutes(-1) && t.status.TaskStatus != "Выполнено");
            var entries = new List<ChartEntry>
            {
                new ChartEntry(completedTasks)
                {
                    Label = "Выполнено",
                    ValueLabel = completedTasks.ToString(),
                    Color = SKColor.Parse("#2ecc71")
                },
                new ChartEntry(pendingTasks)
                {
                    Label = "Ожидает выполнения",
                    ValueLabel = pendingTasks.ToString(),
                    Color = SKColor.Parse("#f1c40f")
                },
                new ChartEntry(inProgressTasks)
                {
                    Label = "Выполняется",
                    ValueLabel = inProgressTasks.ToString(),
                    Color = SKColor.Parse("#0000FF")
                },
                new ChartEntry(overdueTasks)
                {
                    Label = "Просрочено",
                    ValueLabel = overdueTasks.ToString(),
                    Color = SKColor.Parse("#e74c3c")
                }
            };
            TaskStatusChart = new DonutChart { Entries = entries };
        }

        [ObservableProperty]
        private DonutChart taskCategoryChart;

        private void LoadTaskCategoryChart()
        {
            var user = _authManager.CurrentUser;
            var tasks = user.Tasks;
            var personalTasks = tasks.Count(t => t.category.TaskCategory == "Личное");
            var studyTasks = tasks.Count(t => t.category.TaskCategory == "Учеба");
            var workTasks = tasks.Count(t => t.category.TaskCategory == "Работа");
            var entries = new List<ChartEntry>
            {
                new ChartEntry(personalTasks)
                {
                    Label = "Личное",
                    ValueLabel = personalTasks.ToString(),
                    Color = SKColor.Parse("#3498db")
                },
                new ChartEntry(studyTasks)
                {
                    Label = "Учеба",
                    ValueLabel = studyTasks.ToString(),
                    Color = SKColor.Parse("#9b59b6")
                },
                new ChartEntry(workTasks)
                {
                    Label = "Работа",
                    ValueLabel = workTasks.ToString(),
                    Color = SKColor.Parse("#e74c3c")
                }
            };
            TaskCategoryChart = new DonutChart { Entries = entries };
        }
        private bool IsValidLogin(string login)
        {
            return !string.IsNullOrWhiteSpace(login) && login.Length >= 5;
        }
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return false;
            }
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);

            return hasUpperCase && hasLowerCase && hasDigit;
        }
        private bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-za-za-z]{2,3})+$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            return false;
        }

        [RelayCommand]
        public async Taskk GoToAddTask()
        {
            if (Shell.Current.CurrentPage is AddTaskPage)
                return;
            await Shell.Current.GoToAsync(nameof(AddTaskPage));
        }

        [RelayCommand]
        public async Taskk GoToProfile()
        {
            if (Shell.Current.CurrentPage is UserPage)
                return;
            await Shell.Current.GoToAsync(nameof(UserPage));
        }

        [RelayCommand]
        public async Taskk GoToMainPage()
        {
            if (Shell.Current.CurrentPage is MainPage)
                return;
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
