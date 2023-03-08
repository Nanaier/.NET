using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    /*
     The Employee class represents an employee in a company and has various properties that provide information about the employee,
     such as their name, date of birth, education, specialty, and type of employment.The class has a constructor that initializes
     the object with the necessary properties. The EmployeeType enum is also defined to provide two options for the type of employment - full-time and part-time.
     */

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
