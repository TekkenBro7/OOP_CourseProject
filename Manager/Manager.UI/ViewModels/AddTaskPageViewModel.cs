using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.Domain.Entities;
using Manager.Persistense.Repository;
using Manager.UI.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace Manager.UI.ViewModels
{
    public partial class AddTaskPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;
        public AddTaskPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            user = authManager.CurrentUser;
            todayDate = DateTime.Now;
        }
        [ObservableProperty]
        private User user;

        [ObservableProperty]
        private string newTaskTitle;

        [ObservableProperty]
        private string newTaskDescription;

        [ObservableProperty]
        private ObservableCollection<Priority> priorities;

        [ObservableProperty]
        private ObservableCollection<Category> categories;

        [ObservableProperty]
        private ObservableCollection<Status> statuses;

        [ObservableProperty]
        private Priority selectedPriority;

        [ObservableProperty]
        private Category selectedCategory;

        [ObservableProperty]
        private Status selectedStatus;

        [ObservableProperty]
        private DateTime selectedDeadline;

        [ObservableProperty]
        private DateTime todayDate;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        public async Taskk LoadPriorities()
        {
            var priorityList = await _authManager._unitOfWork.PriorityRepository.GetAllAsync();
            Priorities = new ObservableCollection<Priority>(priorityList);

            var categoryList = await _authManager._unitOfWork.CategoryRepository.GetAllAsync();
            Categories = new ObservableCollection<Category>(categoryList);

            var statusList = await _authManager._unitOfWork.StatusRepository.GetAllAsync();
            Statuses = new ObservableCollection<Status>(statusList);
        }

        [RelayCommand]
        async Taskk AddTask() => await AddNewTask();
        public async Taskk AddNewTask() 
        {
            if (string.IsNullOrWhiteSpace(NewTaskTitle) || NewTaskTitle.Length <= 5)
            {
                ErrorMessage = "Название задачи должно быть больше 5 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(NewTaskDescription) || NewTaskDescription.Length <= 10)
            {
                ErrorMessage = "Описание задачи должно быть больше 10 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (SelectedPriority == null)
            {
                ErrorMessage = "Пожалуйста, выберите приоритет задачи.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }

            if (SelectedCategory == null)
            {
                ErrorMessage = "Пожалуйста, выберите категорию задачи.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }

            if (SelectedStatus == null)
            {
                ErrorMessage = "Пожалуйста, выберите статус задачи.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (SelectedDeadline == default)
            {
                SelectedDeadline = DateTime.Now;
            }
            if (User.Tasks == null)
            {
                User.Tasks = [];
            }
            var newTask = new Domain.Entities.Task
            {
                Title = NewTaskTitle,
                Description = NewTaskDescription,
                priority = SelectedPriority,
                status = SelectedStatus,
                category = SelectedCategory,
                Deadline = SelectedDeadline,
            };
            User.Tasks.Add(newTask);
            await _authManager.UpdateUserAsync(User);
            await _authManager._unitOfWork.TaskRepository.AddAsync(newTask);
            await Shell.Current.GoToAsync(nameof(MainPage));

            var request = new NotificationRequest
            {
                NotificationId = 1000,
                Title = "Добавлена новая задача",
                Subtitle = $"Добавлена задача пользователю {User.Login}",
                Description = $"Добавлена задача {newTask.Title}",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(2),
                }
            };
            await LocalNotificationCenter.Current.Show(request);
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