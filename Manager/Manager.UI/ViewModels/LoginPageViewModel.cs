using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.UI.Pages;
using Manager.Persistense.Repository;
using Manager.Domain.Entities;
using Plugin.LocalNotification;

namespace Manager.UI.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;
        public LoginPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
        }
        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;
  
        [RelayCommand]
        async Taskk LoginUser() => await LoginUserAsync();
        private async Taskk LoginUserAsync()
        {
            var existingCategories = await _authManager._unitOfWork.PriorityRepository.GetAllAsync();
            if (!existingCategories.Any())
            {
                await _authManager._unitOfWork.CategoryRepository.AddAsync(new Category("Личное"));
                await _authManager._unitOfWork.CategoryRepository.AddAsync(new Category("Учеба"));
                await _authManager._unitOfWork.CategoryRepository.AddAsync(new Category("Работа"));
                await _authManager._unitOfWork.PriorityRepository.AddAsync(new Priority("Высокий"));
                await _authManager._unitOfWork.PriorityRepository.AddAsync(new Priority("Средний"));
                await _authManager._unitOfWork.PriorityRepository.AddAsync(new Priority("Низкий"));
                await _authManager._unitOfWork.StatusRepository.AddAsync(new Status("Ожидает"));
                await _authManager._unitOfWork.StatusRepository.AddAsync(new Status("Выполняется"));
                await _authManager._unitOfWork.StatusRepository.AddAsync(new Status("Выполнено"));
            }
            try
            {
                ErrorMessage = string.Empty;
                if (string.IsNullOrWhiteSpace(Login))
                {
                    ErrorMessage = "Логин не может быть пустым.";
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Пароль не может быть пустым";
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                    return;
                }
                await _authManager.LogInAsync(Login, Password);
                await Shell.Current.GoToAsync(nameof(MainPage));
                var request = new NotificationRequest
                {
                    NotificationId = 1000,
                    Title = "Вход в аккаунт",
                    Subtitle = $"Пользователь {Login} успешно вошел в аккаунт",
                    BadgeNumber = 42,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(2),
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
            }
        }

        [RelayCommand]
        private async Taskk GoToRegisterAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }

        [RelayCommand]
        public void ClearLoginFields()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}
