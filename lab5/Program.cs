using System;
using System.Collections.Generic;

namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            IBM IBM = new IBM(122.84);
            Apple Apple = new Apple(172.57);
            Intel Intel = new Intel(28.95);
            Amazon Amazon = new Amazon(110.26);

            //Observer
            Investor i1 = new Investor("Anastasiia Lysenko");
            Investor i2 = new Investor("Murat Al Khadam");
            Investor i3 = new Investor("Oleksandr Demianchyk");
            Investor i4 = new Investor("Oleksii Babashev");

            List<Investor> _investors = new List<Investor> { i1, i2, i3, i4 };

            foreach (Investor investor in _investors)
            {
                IBM.Attach(investor);
                Apple.Attach(investor);
                Intel.Attach(investor);
                Amazon.Attach(investor);
                Console.WriteLine("\n-------------------------\n");
            }

            IBM.Price = 124.1;
            Console.WriteLine("\n-------------------------\n");
            Apple.Price = 170.43;
            Console.WriteLine("\n-------------------------\n");
            Amazon.Price = 107.23;
            Console.WriteLine("\n-------------------------\n");
            Intel.Price = 32.67;

            Console.WriteLine("\n-------------------------\n");

            Intel.Detach(i3);
            Intel.Detach(i4);

            Console.WriteLine("\n-------------------------\n");

            Intel.Price = 31.63;

            Console.WriteLine("\n-----------------------------------\n");

            UAH UAH = new UAH(36.57);
            GBP GBP = new GBP(0.8);
            JPY JPY = new JPY(135.75);
            SEK SEK = new SEK(10.39);
            CurrencyObserver al = new CurrencyObserver("Anastasiia Lysenko");
            CurrencyObserver mak = new CurrencyObserver("Murat Al Khadam");
            CurrencyObserver od = new CurrencyObserver("Oleksandr Demianchyk");
            CurrencyObserver ob = new CurrencyObserver("Oleksii Babashev");

            List<CurrencyObserver> _currencyObservers = new List<CurrencyObserver> { al, mak, od, ob };

            foreach (CurrencyObserver currencyObserver in _currencyObservers)
            {
                UAH.Attach(currencyObserver);
                GBP.Attach(currencyObserver);
                JPY.Attach(currencyObserver);
                SEK.Attach(currencyObserver);
                Console.WriteLine("\n-------------------------\n");
            }

            UAH.forUSD = 38.45;
            Console.WriteLine("\n-------------------------\n");
            GBP.forUSD = 0.77;
            Console.WriteLine("\n-------------------------\n");
            JPY.forUSD = 132.69;
            Console.WriteLine("\n-------------------------\n");
            SEK.forUSD = 11.24;

            Console.WriteLine("\n-------------------------\n");

            UAH.Detach(al);
            UAH.Detach(ob);

            Console.WriteLine("\n-------------------------\n");

            UAH.forUSD = 35.68;

            Console.WriteLine("\n-----------------------------------\n");

            Console.ReadLine();

        }
    }
}
