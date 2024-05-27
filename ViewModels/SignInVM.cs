using Microsoft.EntityFrameworkCore;
using Supermarket.Commands;
using Supermarket.Models.EntityLayer;
using Supermarket.Services;
using Supermarket.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using NavigationService = Supermarket.Services.NavigationService;
using Type = Supermarket.Models.EntityLayer.Type;

namespace Supermarket.ViewModels
{
    public class SignInVM
    {
        private readonly AuthenticationService _authenticationService;
        private readonly NavigationService _navigationService;
        private string _username;
        private string _password;
        private string _errorMessage;

        public List<User> Users { get; set; } = new List<User>();

        public SignInVM() 
        {
            Users = ((MainWindow)Application.Current.MainWindow)._context.Users.ToList();
            _navigationService = new NavigationService(((MainWindow)Application.Current.MainWindow).Frame, ((MainWindow)Application.Current.MainWindow)._context);
            _authenticationService = new AuthenticationService(((MainWindow)Application.Current.MainWindow)._context);

        }
        public SignInVM(NavigationService navigationService, AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            //SignInCommand = new RelayCommand(SignIn);
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand => new RelayCommand(x => SignIn());

        private void SignIn()
        {
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                var user = _authenticationService.Authenticate(Username, Password);
                if (user != null)
                {
                    ((MainWindow)Application.Current.MainWindow)._context.CurrentUser = user;
                    if (user.Role == Type.Administrator)
                    {
                        _navigationService.NavigateToPage("AdminPage");
                    }
                    else _navigationService.NavigateToPage("CashierPage");
                }
            }

            else
            {
                ErrorMessage = "Invalid username or password";
            }            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
