using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.Domain.Entities;
using Manager.Persistense.Repository;
using Manager.UI.Pages;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Manager.UI.ViewModels
{
    public partial class RegistrationPageViewModel : ObservableObject
    {
        private readonly AuthManager _authManager;
        public RegistrationPageViewModel(AuthManager authManager)
        {
            _authManager = authManager;
        }

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string secondName;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        async Taskk Register() => await RegisterAsync();

        async Taskk RegisterAsync()
        {
            if (!IsValidLogin(Login))
            {
                ErrorMessage = "Логин должен быть не менее 5 символов.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (!IsValidPassword(Password))
            {
                ErrorMessage = "Пароль должен быть не менее 8 символов, содержать буквы и цифры, а также одну заглавную букву.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Имя не может быть пустым.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(SecondName))
            {
                ErrorMessage = "Фамилия не может быть пустой.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
            {
                ErrorMessage = "Неверный формат email.";
                await Application.Current.MainPage.DisplayAlert("Ошибка", ErrorMessage, "OK");
                return;
            }
            User newUser = new()
            {
                Login = Login,
                Password = Password,
                Name = Name,
                SecondName = SecondName,
                Email = Email
            };
            try
            {
                await _authManager.CreateUserAsync(newUser);
                ErrorMessage = string.Empty;
                await Application.Current.MainPage.DisplayAlert("Успешно", "Пользователь создан", "OK");
                await Shell.Current.GoToAsync("///LoginPage");
                var request = new NotificationRequest
                {
                    NotificationId = 1000,
                    Title = "Регистрация",
                    Subtitle = $"Пользователь {Login} успешно зарегистрировался",
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
    }
}
