using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace lab5
{
    public class StockQuoteSubject : ISubject
    {
        private string _companyName;
        private double _price;

        private List<IObserver> _investors = new List<IObserver>();

        public StockQuoteSubject(string companyName, double price)
        {
            _companyName = companyName;
            _price = price;
        }

        // Add observer to the list
        public void Attach(IObserver investor)
        {
            _investors.Add(investor);
            Console.WriteLine($"Investor {investor.Name} Registered for {_companyName}!");
        }

        // Remove observer from the list
        public void Detach(IObserver investor)
        {
            _investors.Remove(investor);
            Console.WriteLine($"Investor {investor.Name} Un-registered from {_companyName}!");
        }

        // Notify all observers of state changes
        public void Notify()
        {
            foreach (IObserver observer in _investors)
            {
                observer.Update(this);
            }
        }
        public string CompanyName
        {
            get { return this._companyName; }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                    Thread.Sleep(1000);
                }
            }
        }
    }

    public class IBM : StockQuoteSubject
    {
        // Constructor
        public IBM(double price)
            : base("IBM", price)
        {
        }
    }

    public class Apple : StockQuoteSubject
    {
        // Constructor
        public Apple(double price)
            : base("Apple", price)
        {
        }
    }

    public class Intel : StockQuoteSubject
    {
        // Constructor
        public Intel(double price)
            : base("Intel", price)
        {
        }
    }

    public class Amazon : StockQuoteSubject
    {
        // Constructor
        public Amazon(double price)
            : base("Amazon", price)
        {
        }
    }
}
