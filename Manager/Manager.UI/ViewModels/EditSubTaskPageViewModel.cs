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
using System.Xml.Linq;

namespace Manager.UI.ViewModels
{
    [QueryProperty(nameof(SubTask), nameof(SubTask))]
    public partial class EditSubTaskPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;

        public EditSubTaskPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
            User = _authManager.CurrentUser;
        }

        [ObservableProperty]
        private User user;

        [ObservableProperty]
        private SubTask subTask;

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

        [ObservableProperty]
        private ObservableCollection<Comment> comments;

        [RelayCommand]
        public async Taskk LoadPriorities()
        {
            var priorityList = await _authManager._unitOfWork.PriorityRepository.GetAllAsync();
            Priorities = new ObservableCollection<Priority>(priorityList);

            var categoryList = await _authManager._unitOfWork.CategoryRepository.GetAllAsync();
            Categories = new ObservableCollection<Category>(categoryList);

            var statusList = await _authManager._unitOfWork.StatusRepository.GetAllAsync();
            Statuses = new ObservableCollection<Status>(statusList);

            Comments = new ObservableCollection<Comment>(SubTask.Comments);
        }

        [RelayCommand]
        public async Taskk SaveSubTask()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SubTask.Title) || SubTask.Title.Length <= 5)
                {
                    ErrorMessage = "Название задачи должно быть больше 5 символов.";
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                    return;
                }
                if (string.IsNullOrWhiteSpace(SubTask.Description) || SubTask.Description.Length <= 10)
                {
                    ErrorMessage = "Описание задачи должно быть больше 10 символов.";
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                    return;
                }
                if (SelectedPriority != null && SelectedPriority != SubTask.priority)
                {
                    SubTask.priority = SelectedPriority;
                }

                if (SelectedCategory != null && SelectedCategory != SubTask.category)
                {
                    SubTask.category = SelectedCategory;
                }

                if (SelectedStatus != null && SelectedStatus != SubTask.status)
                {
                    SubTask.status = SelectedStatus;
                }
                await _authManager._unitOfWork.SubTaskRepository.UpdateAsync(SubTask);
                var task = User.Tasks.FirstOrDefault(t => t.SubTasks.Any(st => st.Id == SubTask.Id));
                if (task != null)
                {
                    var subTaskIndex = task.SubTasks.FindIndex(st => st.Id == SubTask.Id);
                    if (subTaskIndex >= 0)
                    {
                        task.SubTasks[subTaskIndex] = SubTask;
                    }
                    await _authManager._unitOfWork.TaskRepository.UpdateAsync(task);
                    var taskIndex = User.Tasks.FindIndex(t => t.Id == task.Id);
                    if (taskIndex >= 0)
                    {
                        User.Tasks[taskIndex] = task;
                    }
                    await _authManager.UpdateUserAsync(User);
                }
                await _authManager._unitOfWork.SaveAllAsync();
                await Shell.Current.GoToAsync(nameof(ProfilePage));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ex.Message}");
            }
        }

        [RelayCommand]
        public async Taskk DeleteSubTask()
        {            
            var task = User.Tasks.FirstOrDefault(t => t.SubTasks != null && t.SubTasks.Any(st => st.Id == SubTask.Id));
            if (task != null)
            {
                var subTaskIndex = task.SubTasks.FindIndex(st => st.Id == SubTask.Id);
                if (subTaskIndex >= 0)
                {
                    task.SubTasks.RemoveAt(subTaskIndex);
                }
                await _authManager._unitOfWork.TaskRepository.UpdateAsync(task);
                var taskIndex = User.Tasks.FindIndex(t => t.Id == task.Id);
                if (taskIndex >= 0)
                {
                    User.Tasks[taskIndex] = task;
                }
            }
            await _authManager._unitOfWork.SubTaskRepository.DeleteAsync(SubTask);
            await _authManager.UpdateUserAsync(User);
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

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
            SubTask.Comments.Add(newComment);
            Comments = new ObservableCollection<Comment>(SubTask.Comments);
            await _authManager._unitOfWork.SubTaskRepository.UpdateAsync(SubTask);
            var task = User.Tasks.FirstOrDefault(t => t.SubTasks.Any(st => st.Id == SubTask.Id));
            if (task != null)
            {
                var subTaskIndex = task.SubTasks.FindIndex(st => st.Id == SubTask.Id);
                if (subTaskIndex >= 0)
                {
                    task.SubTasks[subTaskIndex] = SubTask;
                }
                await _authManager._unitOfWork.TaskRepository.UpdateAsync(task);
                var taskIndex = User.Tasks.FindIndex(t => t.Id == task.Id);
                if (taskIndex >= 0)
                {
                    User.Tasks[taskIndex] = task;
                }
                await _authManager.UpdateUserAsync(User);
            }
            await _authManager._unitOfWork.CommentRepository.AddAsync(newComment);
            await _authManager._unitOfWork.SaveAllAsync();
            NewCommentContent = string.Empty;
        }

        [RelayCommand]
        public async Taskk DeleteCommentAsync(Comment comment)
        {
            SubTask.Comments.Remove(comment);
            Comments = new ObservableCollection<Comment>(SubTask.Comments);
            await _authManager._unitOfWork.SubTaskRepository.UpdateAsync(SubTask);
            var task = User.Tasks.FirstOrDefault(t => t.SubTasks.Any(st => st.Id == SubTask.Id));
            if (task != null)
            {
                var subTaskIndex = task.SubTasks.FindIndex(st => st.Id == SubTask.Id);
                if (subTaskIndex >= 0)
                {
                    task.SubTasks[subTaskIndex] = SubTask;
                }
                await _authManager._unitOfWork.TaskRepository.UpdateAsync(task);
                var taskIndex = User.Tasks.FindIndex(t => t.Id == task.Id);
                if (taskIndex >= 0)
                {
                    User.Tasks[taskIndex] = task;
                }
                await _authManager.UpdateUserAsync(User);
            }
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
