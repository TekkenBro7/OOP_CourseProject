using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.Domain.Entities;
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
    public partial class EditTaskPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;

        [ObservableProperty]
        private Task task;

        [ObservableProperty]
        private User user;

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

            Comments = new ObservableCollection<Comment>(Task.Comments);

        }
        public EditTaskPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            User = _authManager.CurrentUser;
            
      //      Comments = new ObservableCollection<Comment>(Task.Comments);
        }

        partial void OnTaskChanged(Task value)
        {
            if (value != null)
            {
                // Ensure Task.Comments is initialized
                if (value.Comments == null)
                {
                    value.Comments = new List<Comment>();
                }
                Comments = new ObservableCollection<Comment>(value.Comments);
            }
        }

        [RelayCommand]
        async void EditTask() => Edit();
        public async void Edit()
        {
            if (string.IsNullOrWhiteSpace(Task.Title) || Task.Title.Length <= 5)
            {
                ErrorMessage = "Название задачи должно быть больше 5 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Task.Description) || Task.Description.Length <= 10)
            {
                ErrorMessage = "Описание задачи должно быть больше 10 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (SelectedPriority != null && SelectedPriority != Task.priority)
            {
                Task.priority = SelectedPriority;
            }
            if (SelectedCategory != null && SelectedCategory != Task.category)
            {
                Task.category = SelectedCategory;
            }
            if (SelectedStatus != null && SelectedStatus != Task.status)
            {
                Task.status = SelectedStatus;
            }
            await _authManager._unitOfWork.TaskRepository.UpdateAsync(Task);
            int index = User.Tasks.FindIndex(t => t.Id == Task.Id);
            if (index >= 0)
            {
                User.Tasks[index] = Task;
            }
            await _authManager.UpdateUserAsync(User);
            await Shell.Current.GoToAsync(nameof(ProfilePage));
            var request = new NotificationRequest
            {
                NotificationId = 1000,
                Title = "Изменение задачи",
                Description = $"Изменена задача {Task.Title}",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(2),
                }
            };
            await LocalNotificationCenter.Current.Show(request);
        }

        [RelayCommand]
        public async Taskk EditSubTask(SubTask subTask)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "SubTask", subTask }
            };
            await Shell.Current.GoToAsync(nameof(EditSubTaskPage), parameters);
        }

        [ObservableProperty]
        private ObservableCollection<Comment> comments;

        [ObservableProperty]
        private string newCommentContent;

        [RelayCommand]
        public async Taskk AddCommentAsync()
        {
            if (string.IsNullOrWhiteSpace(NewCommentContent))
            {
                ErrorMessage = "Комментарий не может быть пустым.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }

            var newComment = new Comment(NewCommentContent);
            Task.Comments.Add(newComment);
            Comments = new ObservableCollection<Comment>(Task.Comments);
            await _authManager._unitOfWork.TaskRepository.UpdateAsync(Task);
            int index = User.Tasks.FindIndex(t => t.Id == Task.Id);
            if (index >= 0)
            {
                User.Tasks[index] = Task;
            }
            await _authManager.UpdateUserAsync(User);
            await _authManager._unitOfWork.CommentRepository.AddAsync(newComment);
            await _authManager._unitOfWork.SaveAllAsync();
            
            NewCommentContent = string.Empty;

        }

        [RelayCommand]
        public async Taskk DeleteCommentAsync(Comment comment)
        {
            Task.Comments.Remove(comment);
            Comments = new ObservableCollection<Comment>(Task.Comments);
            await _authManager._unitOfWork.TaskRepository.UpdateAsync(Task);
            int index = User.Tasks.FindIndex(t => t.Id == Task.Id);
            if (index >= 0)
            {
                User.Tasks[index] = Task;
            }
            await _authManager.UpdateUserAsync(User);
            await _authManager._unitOfWork.CommentRepository.DeleteAsync(comment);
            await _authManager._unitOfWork.SaveAllAsync();

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
