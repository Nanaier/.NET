using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    public class Employee
    {
        public int Id { get; }
        public string Surname { get; }
        public string FirstName { get; }
        public DateTime DateOfBirth { get; }
        public int RollNumber { get; }
        public int TaxpayerRegistrationNumber { get; }
        public string Education { get; }
        public string Specialty { get; }
        public DateTime DateOfCommencementOfWork { get; }
        public EmployeeType Type { get; }

        public Employee(int id, string surname, string firstname, DateTime dateofbirth, int rollnumber, int taxnum, string education, string spec, DateTime datestart, EmployeeType type)
        {
            Id = id;
            Surname = surname;
            FirstName = firstname;
            DateOfBirth = dateofbirth;
            RollNumber = rollnumber;
            TaxpayerRegistrationNumber = taxnum;
            Education = education;
            Specialty = spec;
            DateOfCommencementOfWork = datestart;
            Type = type;
        }
    }
    public enum EmployeeType
    {
        FullTime,
        PartTime
    }
}
