using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
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
}
