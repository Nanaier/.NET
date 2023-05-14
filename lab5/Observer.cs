using System;


namespace lab5
{
    public interface IObserver
    {
        string Name { get; }
        // Receive update from subject
        void Update(ISubject subject);
    }

    public class Investor
        : IObserver
    {
        private string _name;
        public Investor(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public void Update(ISubject stockExchange)
        {
            Console.WriteLine($"-> Notified { _name } of { ((StockQuoteSubject)stockExchange).CompanyName }'s has changed to ${ ((StockQuoteSubject)stockExchange).Price }");
        }
    }


    public class CurrencyObserver
       : IObserver
    {
        private string _name;
        public CurrencyObserver(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public void Update(ISubject currencyRate)
        {
            Console.WriteLine($"-> Notified { _name } of { ((CurrencyRateSubject)currencyRate).CurrencyCode }'s has changed to ${ ((CurrencyRateSubject)currencyRate).forUSD }");
        }
    }

}
