using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    public class MonthlySalary
    {
        public int Id { get; set; }
        public int TaxpayerRegistrationNumber { get; set; }
        public DateTime Month { get; set; }
        public decimal Amount { get; set; }
        public MonthlySalary(int id, int taxnum, DateTime month, decimal amount)
        {
            Id = id;
            TaxpayerRegistrationNumber = taxnum;
            Month = month;
            Amount = amount;
        }
    }
}
