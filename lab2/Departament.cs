using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace lab2
{
    /*
     The Department class represents a department in an organization and contains information about its employees and salaries. 
     It has an Id and a Name property that are set through the constructor and cannot be modified after that. It also has private fields _employees
     and _salaries which are lists of Employee and MonthlySalary objects respectively.The class provides four methods to add employees and salaries
     to the department. AddEmployee and AddEmployees take a single Employee object or a list of Employee objects respectively and add them to the _employees
     list. Similarly, AddSalary and AddSalaries take a single MonthlySalary object or a list of MonthlySalary objects respectively and add them to the _salaries list.
     The Employees and Salaries properties are read-only and return a read-only list of the department's employees and salaries respectively.
     */
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Employee> Employees;

        public List<MonthlySalary> Salaries;
        public Department() { }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
            Employees = new List<Employee>();
            Salaries = new List<MonthlySalary>();
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
        public void AddEmployees(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                Employees.Add(employees[i]);
            }

        }
        public void AddSalary(MonthlySalary salary)
        {
            Salaries.Add(salary);
        }

        public void AddSalaries(List<MonthlySalary> salary)
        {
            for (int i = 0; i < salary.Count; i++)
            {
                Salaries.Add(salary[i]);
            }

        }
    }
}
