using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Supermarket.Models.BusinessLogicLayer
{
    public class ProducerService
    {
        private readonly DataContext _context;

        public ProducerService()
        {
            _context = ((MainWindow)Application.Current.MainWindow)._context;
        }

        public Producer? GetProducerByName(string name)
        {
            return _context.Producers.FirstOrDefault(p => p.Name == name);
        }

        public List<Producer> GetAllProducers()
        {
            return _context.Producers.ToList();
        }

        public void AddProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();
        }

        public void UpdateProducer(int id, string name, string country)
        {
            var existingProducer = _context.Producers.FirstOrDefault(p => p.ID == id);
            if (existingProducer != null)
            {
                existingProducer.Name = name;
                existingProducer.Country = country;
                _context.SaveChanges();
            }
        }

        public void DeleteProducer(string name)
        {
            var producer = _context.Producers.FirstOrDefault(p => p.Name == name);
            if (producer != null)
            {
                _context.Producers.Remove(producer);
                _context.SaveChanges();
            }
        }
    }
}
