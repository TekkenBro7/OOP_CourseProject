using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.Domain.Entities;
using Manager.Persistense.Repository;
using Manager.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Manager.UI.ViewModels
{
    [QueryProperty(nameof(Task), nameof(Task))]
    public partial class DetaliPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;

        [ObservableProperty]
        Task task;

        [ObservableProperty]
        User user;

        public DetaliPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            User = _authManager.CurrentUser;
        }

        [RelayCommand]
        async Taskk Edit() => await GotoEditObjectPage();

        //   async void Edit() => await GotoEditObjectPage();
        public async Taskk GotoEditObjectPage()
        {
            IDictionary<string, object> parametrs = new Dictionary<string, object>()
            {
                { "Task", Task }
            };
            await Shell.Current.GoToAsync(nameof(EditTaskPage), parametrs);
        }

        [RelayCommand]
        async Taskk TaskList() => await LoadTasksAsync();
        private async Taskk LoadTasksAsync()
        {
            if (User.Tasks != null)
            {
                var taskCopy = new List<Task>(User.Tasks);
                User.Tasks.Clear();
                foreach (var task in taskCopy)
                {
                    User.Tasks.Add(task);
                }
            }
        }

        [RelayCommand]
        public void LoadTaskData()
        {
            if (User != null && User.Tasks != null)
            {
                var updatedTask = User.Tasks.FirstOrDefault(t => t.Id == Task.Id);
                if (updatedTask != null)
                {
                    Task = updatedTask;
                }
            }
        }

        [RelayCommand]
        async Taskk DeleteTask() => await Delete();
        public async Taskk Delete()
        {
            if (Task != null && User.Tasks != null)
            {
                User.Tasks.Remove(Task);
                _authManager._unitOfWork.TaskRepository.Delete(Task);
                await _authManager._unitOfWork.SaveAllAsync();
                await _authManager.UpdateUserAsync(User);
                await Shell.Current.GoToAsync(nameof(ProfilePage));
            }
        }

        [RelayCommand]
        async Taskk AddSubTask() => await GoToSubTaskPage();
        public async Taskk GoToSubTaskPage()
        {
            IDictionary<string, object> parametrs = new Dictionary<string, object>()
            {
                { "Task", Task }
            };
            await Shell.Current.GoToAsync(nameof(AddSubTaskPage), parametrs);
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
