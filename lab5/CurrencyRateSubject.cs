using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace lab5
{

    public class CurrencyRateSubject : ISubject
    {
        private string _currencyCode;
        private double _valueUsd;
        private List<IObserver> _currencyObservers = new List<IObserver>();

        public CurrencyRateSubject(string currencyCode, double valueUsd)
        {
            _currencyCode = currencyCode;
            _valueUsd = valueUsd;
        }

        public string CurrencyCode
        {
            get { return this._currencyCode; }
        }

        public double forUSD
        {
            get { return _valueUsd; }
            set
            {
                if (_valueUsd != value)
                {
                    _valueUsd = value;
                    Notify();
                    Thread.Sleep(1000);
                }
            }
        }

        // Add observer to the list
        public void Attach(IObserver observer)
        {
            _currencyObservers.Add(observer);
            Console.WriteLine($"Currency observer {observer.Name} Registered for {_currencyCode}!");
        }

        // Remove observer from the list
        public void Detach(IObserver observer)
        {
            _currencyObservers.Remove(observer);
            Console.WriteLine($"Currency observer {observer.Name} Un-registered from {_currencyCode}!");
        }

        // Notify all observers of state changes
        public void Notify()
        {
            foreach (IObserver observer in _currencyObservers)
            {
                observer.Update(this);
            }
        }
    }

    public class UAH : CurrencyRateSubject //Ukrainian Hryvnia
    {
        // Constructor
        public UAH(double valueUsd)
            : base("UAH", valueUsd)
        {
        }
    }

    public class GBP : CurrencyRateSubject //British Pound
    {
        // Constructor
        public GBP(double valueUsd)
            : base("GBP", valueUsd)
        {
        }
    }
    public class JPY : CurrencyRateSubject //Japanese Yen
    {
        // Constructor
        public JPY(double valueUsd)
            : base("JPY", valueUsd)
        {
        }
    }
    public class SEK : CurrencyRateSubject //Swedish Krona
    {
        // Constructor
        public SEK(double valueUsd)
            : base("SEK", valueUsd)
        {
        }
    }


}
