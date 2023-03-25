using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace lab2
{
    /*
     This is a class called MonthlySalary with four properties: Id, TaxpayerRegistrationNumber, Month, and Amount.
     It has a constructor that takes four arguments and sets the corresponding properties.
     The Id property is an integer that represents the unique identifier for each monthly salary.
     The TaxpayerRegistrationNumber property is also an integer and represents the taxpayer registration number of the employee who received the monthly salary.
     The Month property is a DateTime object that represents the month the salary was paid.
     The Amount property is a decimal that represents the amount of the salary paid in that month.
    */
    public class MonthlySalary
    {

        public int Id { get; set; }
        public int TaxpayerRegistrationNumber { get; set; }
        public DateTime Month { get; set; }
        public decimal Amount { get; set; }

        public MonthlySalary() { }
        public MonthlySalary(int id, int taxnum, DateTime month, decimal amount)
        {
            Id = id;
            TaxpayerRegistrationNumber = taxnum;
            Month = month;
            Amount = amount;
        }
    }
}
