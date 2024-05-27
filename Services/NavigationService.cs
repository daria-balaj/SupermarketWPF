using Supermarket.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Supermarket.ViewModels;
using System.Windows;
using Supermarket.Data;

namespace Supermarket.Services
{
    public class NavigationService
    {
        private Frame _frame;
        private readonly DataContext _context;

        public NavigationService(Frame frame, DataContext context)
        {
            _frame = frame;
            _context = context;
        }

        public void NavigateToPage(string page)
        {
            switch (page)
            {
                case "AdminPage":
                    _frame.Content = new AdminPage(_frame, ((MainWindow)Application.Current.MainWindow)._context);
                    break;
                case "CashierPage":
                    _frame.Content = new CashierPage();
                    break;
            }
        }

    }
}
