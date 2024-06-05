using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.Persistense.Repository;
using Manager.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.UI.ViewModels
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;

        [ObservableProperty]
        private User user;
        public ProfilePageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            User = _authManager.CurrentUser;
        }
        
        [RelayCommand]
        async Taskk ShowDetails(Task task) => await GotoDetailsPage(task);
        private async Taskk GotoDetailsPage(Task task)
        {
            IDictionary<string, object> parametrs = new Dictionary<string, object>()
            {
                {"Task", task }
            };
            await Shell.Current.GoToAsync(nameof(DetaliPage), parametrs);
        }

        [RelayCommand]
        async Taskk TaskList() => await LoadTasksAsync();
        private async Taskk LoadTasksAsync()
        {
            if (User?.Tasks != null && User.Tasks.Count != 0)
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
        async Taskk ShowBack() => await GoBack();
        private async Taskk GoBack()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        [RelayCommand]
        public async Taskk GoToProfilePage()
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
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
