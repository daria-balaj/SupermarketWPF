using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using Supermarket.Services;
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
using NavigationService = Supermarket.Services.NavigationService;

namespace Supermarket.Views
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public NavigationService NavigationService { get; }
        public AuthenticationService AuthenticationService { get; }
        public AdminPageVM AdminVM = new AdminPageVM();
        public StockVM StockVM = new StockVM();
        //public DataContext _context { get; }
        public AdminPage(Frame frame, DataContext context)
        {
            InitializeComponent();
            //_context = context;
            NavigationService = new NavigationService(((MainWindow)Application.Current.MainWindow).Frame, context);
            AuthenticationService = new AuthenticationService(context);
            SignInVM vm = new SignInVM(NavigationService, AuthenticationService);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProducersTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AdminVM.ProducerVM != null)
                AdminVM.ProducerVM.SelectedProducer = null;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdminVM.ProducerVM.SelectedProducer = (Producer)ViewProducersDataGrid.SelectedItem;
            AdminVM.ViewProductsFromProducer();
        }
    }
}
