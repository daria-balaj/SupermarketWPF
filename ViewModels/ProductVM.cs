using Supermarket.Commands;
using Supermarket.Models.BusinessLogicLayer;
using Supermarket.Models.EntityLayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Supermarket.ViewModels
{
    public class ProductVM
    {

        public ProductVM()
        {
            productService = new ProductService();
            SearchResults = new ObservableCollection<Product>();
            Categories = ((MainWindow)Application.Current.MainWindow)._context.Categories.ToList();
            Producers = ((MainWindow)Application.Current.MainWindow)._context.Producers.ToList();

        }

        public ProductService productService { get; set; }
        public int SearchID { get; set; }
        public string SearchName { get; set; }
        public string SearchBarcode { get; set; }
        public Category SearchCategory { get; set; }
        public string SearchProducer { get; set; }
        public DateOnly? SearchExpDate { get; set; }

        public Product SelectedProduct { get; set; }

        private ObservableCollection<Product> _searchResults;
        public ObservableCollection<Product> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        public List<Category> Categories { get; set; }
        public List<Producer> Producers { get; set; }

        public ICommand SearchByIDCommand => new RelayCommand(parameter =>
        {
            SearchResults.Clear();
            var p = productService.GetProductByID((int)parameter);
            if (p != null)
                SearchResults.Add(p);
        });
        public ICommand SearchByNameCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductByName((string)parameter); if (p != null) SearchResults.Add(p); });
        public ICommand SearchByBarcodeCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductByBarcode((string)parameter); if (p != null) SearchResults.Add(p); });
        public ICommand SearchByProducerCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductsByProducer((string)parameter); if (p != null) foreach (var result in p) SearchResults.Add(result); });
        public ICommand SearchByCategoryCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductsByCategory((Category)parameter); if (p != null) foreach (var result in p) SearchResults.Add(result); });
        public ICommand SearchByExpDateCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductsByExpDate((DateOnly)parameter); if (p != null) foreach (var result in p) SearchResults.Add(result); });



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
