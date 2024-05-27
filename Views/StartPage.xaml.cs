using Supermarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Supermarket.Views
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public SignInVM SignInVM = new SignInVM();
        public StartPage()
        {
            InitializeComponent();
            Keyboard.Focus(usernameBox);
            Keyboard.Focus(passwordBox);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as SignInVM).Password = passwordBox.Password;
        }
    }
}
