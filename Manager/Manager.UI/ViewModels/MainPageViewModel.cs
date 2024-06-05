using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Domain;
using Manager.UI.Pages;
using Manager.Domain.Entities;
using Manager.Persistense.Repository;


namespace Manager.UI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;
        public ObservableCollection<Domain.Entities.Task> Tasks { get; set; } = [];

        [ObservableProperty]
        private User user;

        [ObservableProperty]
        private string newTaskTitle;

        [ObservableProperty]
        private string newTaskDescription;

        [ObservableProperty]
        private ObservableCollection<string> sortCriteria;

        public MainPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            User = _authManager.CurrentUser;
            Tasks = new ObservableCollection<Domain.Entities.Task>(User.Tasks);
            SortCriteria = new ObservableCollection<string>
            {
                "Название",
                "Дата создания",
                "Дедлайн"
            };
        }

        [RelayCommand]
        public async Taskk GoToAddTask()
        {
            if (Shell.Current.CurrentPage is AddTaskPage)
                return;
            await Shell.Current.GoToAsync(nameof(AddTaskPage));
        }

        [RelayCommand]
        public async Taskk GoToProfilePage()
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
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

        [RelayCommand]
        async void AddTask() => await AddNewTask();
        public async Taskk AddNewTask()
        {
            if (User.Tasks == null)
            {
                User.Tasks = [];
            }
            var newTask = new Domain.Entities.Task
            {
                Title = NewTaskTitle,
                Description = NewTaskDescription,
            };
            User.Tasks.Add(newTask);
            Tasks.Add(newTask);
            await _authManager.UpdateUserAsync(User);
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
            var priorityList = await _authManager._unitOfWork.PriorityRepository.GetAllAsync();
            Priorities = new ObservableCollection<Priority>(priorityList);

            var categoryList = await _authManager._unitOfWork.CategoryRepository.GetAllAsync();
            Categories = new ObservableCollection<Category>(categoryList);

            var statusList = await _authManager._unitOfWork.StatusRepository.GetAllAsync();
            Statuses = new ObservableCollection<Status>(statusList);
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
        private bool isSortAscending = true;

        [ObservableProperty]
        private string selectedSortCriterion;

        [RelayCommand]
        public void ApplyFilters()
        {
            var filteredTasks = User.Tasks.AsEnumerable();

            if (SelectedPriority != null)
            {
                filteredTasks = filteredTasks.Where(t => t.priority.PriorityLevel == SelectedPriority.PriorityLevel);
            }

            if (SelectedStatus != null)
            {
                filteredTasks = filteredTasks.Where(t => t.status.TaskStatus == SelectedStatus.TaskStatus);
            }

            if (SelectedCategory != null)
            {
                filteredTasks = filteredTasks.Where(t => t.category.TaskCategory == SelectedCategory.TaskCategory);
            }
            switch (SelectedSortCriterion)
            {
                case "Title":
                    filteredTasks = IsSortAscending
                        ? filteredTasks.OrderBy(t => t.Title)
                        : filteredTasks.OrderByDescending(t => t.Title);
                    break;
                case "CreateTime":
                    filteredTasks = IsSortAscending
                        ? filteredTasks.OrderBy(t => t.CreateTime.Date).ThenBy(t => t.CreateTime.TimeOfDay)
                        : filteredTasks.OrderByDescending(t => t.CreateTime.Date).ThenByDescending(t => t.CreateTime.TimeOfDay);
                    break;
                case "Deadline":
                    filteredTasks = IsSortAscending
                        ? filteredTasks.OrderBy(t => t.Deadline.Date).ThenBy(t => t.Deadline.TimeOfDay)
                        : filteredTasks.OrderByDescending(t => t.Deadline.Date).ThenByDescending(t => t.Deadline.TimeOfDay);
                    break;
                default:
                    break;
            }
            Tasks.Clear();
            foreach (var task in filteredTasks)
            {
                Tasks.Add(task);
            }
            OnPropertyChanged(nameof(Tasks));
        }
        [RelayCommand]
        public async Taskk GoToUserPage()
        {
            await Shell.Current.GoToAsync(nameof(UserPage));
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

    }
}
