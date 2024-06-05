using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.Persistense.Repository;
using Manager.UI.Pages;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.UI.ViewModels
{
    [QueryProperty(nameof(Task), nameof(Task))]
    public partial class AddSubTaskPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        User user;

        [ObservableProperty]
        Task task;
        public AddSubTaskPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            user = _authManager.CurrentUser;
        }

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
        async Taskk AddSubTaskAsync() => await AddSubTask();
        private async Taskk AddSubTask()
        {
            if (string.IsNullOrWhiteSpace(Title) || Title.Length <= 5)
            {
                ErrorMessage = "Название подзадачи должно быть больше 5 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Description) || Description.Length <= 10)
            {
                ErrorMessage = "Описание подзадачи должно быть больше 10 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (SelectedPriority == null)
            {
                ErrorMessage = "Пожалуйста, выберите приоритет подзадачи.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (SelectedCategory == null)
            {
                ErrorMessage = "Пожалуйста, выберите категорию подзадачи.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (SelectedStatus == null)
            {
                ErrorMessage = "Пожалуйста, выберите статус подзадачи.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (Task != null)
            {
                if (Task.SubTasks == null)
                {
                    Task.SubTasks = [];
                }
                var newSubTask = new SubTask
                {
                    Title = Title,
                    Description = Description,
                    CreateTime = DateTime.Now,
                    priority = SelectedPriority,
                    category = SelectedCategory,
                    status = SelectedStatus,
                };
                Task.SubTasks.Add(newSubTask);
                await _authManager._unitOfWork.SubTaskRepository.AddAsync(newSubTask);
                await _authManager.UpdateUserAsync(User);
                await _authManager._unitOfWork.SaveAllAsync();
                await Shell.Current.GoToAsync(nameof(ProfilePage));
                var request = new NotificationRequest
                {
                    NotificationId = 1000,
                    Title = "Добавлена новая подзадача",
                    Subtitle = $"Добавлена подзадача пользователю {User.Login}",
                    Description = $"Добавлена подзадача {newSubTask.Title} к задаче {Task.Title}",
                    BadgeNumber = 42,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(2),
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
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
