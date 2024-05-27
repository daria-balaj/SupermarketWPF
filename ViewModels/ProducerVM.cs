using Supermarket.Commands;
using Supermarket.Models.BusinessLogicLayer;
using Supermarket.Models.EntityLayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Supermarket.ViewModels
{
    public class ProducerVM : INotifyPropertyChanged
    {
        public ProducerVM()
        {
            producerService = new ProducerService();
            SearchResults = new ObservableCollection<Producer>();
            InitializeProducerList();
        }

        public ProducerService producerService;

        private ObservableCollection<Producer> producers = new ObservableCollection<Producer>();

        private void InitializeProducerList()
        {
            var list = producerService.GetAllProducers(); 
            if (list != null)
            {
                foreach (var item in list)
                    Producers.Add(item);
            }
        }

        public ObservableCollection<Producer> Producers
        {
            get { return producers; }
            set
            {
                producers = value;
                OnPropertyChanged(nameof(producers));
            }
        }

        public Producer SelectedProducer { get; set; }

        public string SearchName { get; set; }

        public string ProducerName { get; set; }   
        public string ProducerOrigin { get; set; }

        private ObservableCollection<Producer> _searchResults;
        public ObservableCollection<Producer> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        public ICommand SearchByNameCommand => new RelayCommand(parameter =>
        {
            SearchResults.Clear();
            var p = producerService.GetProducerByName((string)parameter);
            if (p != null)
                SearchResults.Add(p);
        });



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
