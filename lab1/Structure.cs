using System;
using System.Collections.Generic;
using System.Linq;


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

public class Department
{
    public int Id { get; }
    public string Name { get; }
    private List<Employee> _employees;
    private List<MonthlySalary> _salaries;

    public Department(int id, string name)
    {
        Id = id;
        Name = name;
        _employees = new List<Employee>();
        _salaries = new List<MonthlySalary>();
    }

    public IReadOnlyList<Employee> Employees => _employees;
    public IReadOnlyList<MonthlySalary> Salaries => _salaries;

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
    }
    public void AddEmployees(List<Employee> employees)
    {
        for (int i = 0; i < employees.Count; i++)
        {
            _employees.Add(employees[i]);
        }

    }
    public void AddSalary(MonthlySalary salary)
    {
        _salaries.Add(salary);
    }

    public void AddSalaries(List<MonthlySalary> salary)
    {
        for (int i = 0; i < salary.Count; i++)
        {
            _salaries.Add(salary[i]);
        }
        
    }
}

class Enterprise
{
    private List<Department> _departments;

    public Enterprise()
    {
        _departments = new List<Department>();
    }
    public IReadOnlyList<Department> Departments => _departments;
    public void AddDepartment(Department department)
    {
        _departments.Add(department);
    }

}