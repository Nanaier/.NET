using System;
using System.Linq;
using System.Collections.Generic;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var enterprise = new Enterprise();

            // Create a new department
            var department1 = new Department(1, "Sales");
            var department2 = new Department(2, "IT");
            var department3 = new Department(3, "Design");

            // Create some employees and their monthly salaries
            var employee1 = new Employee(1, "Smith", "John", new DateTime(1980, 1, 1), 1, 123456789, "Bachelor's Degree", "Sales", new DateTime(2010, 1, 1), EmployeeType.FullTime);
            var employee2 = new Employee(2, "Doe", "Jane", new DateTime(1985, 5, 10), 2, 987654321, "Master's Degree", "Marketing", new DateTime(2012, 3, 15), EmployeeType.PartTime);
            var employee3 = new Employee(3, "Lee", "David", new DateTime(1992, 9, 20), 3, 456789012, "Bachelor's Degree", "IT", new DateTime(2018, 6, 1), EmployeeType.FullTime);
            var employee4 = new Employee(4, "Garcia", "Maria", new DateTime(1990, 3, 18), 4, 567890123, "Master's Degree", "Finance", new DateTime(2016, 9, 1), EmployeeType.FullTime);
            var employee5 = new Employee(5, "Wang", "Li", new DateTime(1988, 11, 2), 5, 234567890, "Bachelor's Degree", "Human Resources", new DateTime(2017, 5, 1), EmployeeType.PartTime);
            var employee6 = new Employee(6, "Kim", "Min", new DateTime(1995, 7, 13), 6, 345678901, "Bachelor's Degree", "Operations", new DateTime(2020, 1, 1), EmployeeType.FullTime);

            var salary1 = new MonthlySalary(1, 123456789, new DateTime(2022, 1, 1), 5000.0m);
            var salary2 = new MonthlySalary(2, 123456789, new DateTime(2022, 2, 1), 5500.0m);
            var salary3 = new MonthlySalary(3, 987654321, new DateTime(2022, 1, 1), 3000.0m);
            var salary4 = new MonthlySalary(4, 987654321, new DateTime(2022, 2, 1), 3500.0m);
            var salary5 = new MonthlySalary(5, 234567890, new DateTime(2022, 1, 1), 3500.0m);
            var salary6 = new MonthlySalary(6, 345678901, new DateTime(2022, 1, 1), 6000.0m);
            var salary7 = new MonthlySalary(7, 456789012, new DateTime(2022, 2, 1), 6500.0m);
            var salary8 = new MonthlySalary(8, 234567890, new DateTime(2022, 2, 1), 4000.0m);
            var salary9 = new MonthlySalary(9, 345678901, new DateTime(2022, 2, 1), 6500.0m);
            var salary10 = new MonthlySalary(10, 456789012, new DateTime(2022, 1, 1), 5500.0m);
            var salary11 = new MonthlySalary(11, 567890123, new DateTime(2022, 1, 1), 5500.0m);
            var salary12 = new MonthlySalary(12, 567890123, new DateTime(2022, 2, 1), 5800.0m);


            var salaries1 = new List<MonthlySalary> { salary1, salary2, salary11, salary12 };
            var salaries2 = new List<MonthlySalary> { salary3, salary4, salary7, salary10 };
            var salaries3 = new List<MonthlySalary> { salary5, salary6, salary8, salary9 };
            var employees1 = new List<Employee> { employee1, employee4 };
            var employees2 = new List<Employee> { employee2, employee3};
            var employees3 = new List<Employee> { employee5, employee6 };
            // Add the employees and salaries to the department
            department1.AddEmployees(employees1);
            department2.AddEmployees(employees2);
            department3.AddEmployees(employees3);

            department1.AddSalaries(salaries1);
            department2.AddSalaries(salaries2);
            department3.AddSalaries(salaries3);

            // Add the department to the enterprise
            enterprise.AddDepartment(department1);
            enterprise.AddDepartment(department2);
            enterprise.AddDepartment(department3);




            Console.WriteLine("1.Department Names:");
            var query1 = from d in enterprise.Departments select d.Name;
            foreach (var name in query1) Console.WriteLine($"\t{name}");

            Console.WriteLine("\n2. Amount of people in each department:");
            var query2 = from d in enterprise.Departments
                         select new
                         {
                             name = d.Name,
                             amount = d.Employees.Count
                         };
            foreach (var item in query2) Console.WriteLine($"\t{item.name} \t{item.amount}");

            Console.WriteLine("\n3. Employees with bachelor's degree:");
            var query3 = enterprise.Departments.SelectMany(d => d.Employees)
            .Where(e => e.Education == "Bachelor's Degree");
            foreach (var employee in query3)
            {
                Console.WriteLine($"\t{employee.FirstName} {employee.Surname}");
            }

            Console.WriteLine("\n4. Employee's Names in Ascending Order:");
            var query4 = enterprise.Departments.SelectMany(d => d.Employees).OrderBy(e => e.Surname).ThenBy(e => e.FirstName);                       
            foreach (var emp in query4) Console.WriteLine($"\t{emp.Surname} {emp.FirstName}");

            Console.WriteLine("\n5. Employees that were born after or in 1990");
            var query5 = enterprise.Departments.SelectMany(d => d.Employees).Where(e => e.DateOfBirth.Year >= 1990);
            foreach (var emp in query5) Console.WriteLine($"\t{emp.Surname} {emp.FirstName} \t{emp.DateOfBirth.ToString("d")}");

            Console.WriteLine("\n6. Employees whose First Names start with M in Ascending Order:");
            var query6 = from emp in enterprise.Departments.SelectMany(d => d.Employees)
                         where emp.FirstName.ToUpper().StartsWith("M") 
                                orderby emp.FirstName
                                select emp; 
            foreach (var emp in query6) Console.WriteLine($"\t{emp.FirstName} {emp.Surname}");


            Console.WriteLine($"\n7. Employees from {department1.Name} and {department2.Name} Departments:");
            var query7 = department1.Employees.Concat(department2.Employees).Distinct();
            foreach (var emp in query7) Console.WriteLine($"\t{emp.FirstName} {emp.Surname} \t{emp.DateOfBirth.ToString("d")}");

            Console.WriteLine("\n8. Average salary in the whole enterprise:");
            var query8 = enterprise.Departments.SelectMany(d => d.Salaries).Average(s => s.Amount);
            Console.WriteLine($"\t{Math.Round(query8,3)}");

            Console.WriteLine("\n9. Average salary in the each department:");
            var query9 = from d in enterprise.Departments
                         select new
                         {
                             name = d.Name,
                             avg_amount = Math.Round(d.Salaries.Average(s => s.Amount),3)
                         };
            foreach (var item in query9) Console.WriteLine($"\t{item.name} \t {item.avg_amount}");

            Console.WriteLine("\n10. Amount of FullTime and PartTime employees:");
            var query10 = from e in enterprise.Departments.SelectMany(d => d.Employees)
                         group e by e.Type into empType
                         select new {
                             name = empType.Key,
                             amount = empType.Count()
                         };
            foreach (var item in query10) Console.WriteLine($"\t{item.name} \t {item.amount}");

            Console.WriteLine("\n11. Max salary of FullTime or PartTime employee:");
            var query11 = from e in enterprise.Departments.SelectMany(d => d.Employees)
                         join salary in enterprise.Departments.SelectMany(d => d.Salaries)
                         on e.TaxpayerRegistrationNumber equals salary.TaxpayerRegistrationNumber
                         group salary by e.Type into empType
                         select new
                         {
                             name = empType.Key,
                             amount = empType.Max(s => s.Amount)
                         };
            foreach (var item in query11) Console.WriteLine($"\t{item.name} \t {item.amount}");

            Console.WriteLine("\n12. Total salary for each employee:");
            var query12 = from emp in enterprise.Departments.SelectMany(d => d.Employees)
                                   join salary in enterprise.Departments.SelectMany(d => d.Salaries)
                                   on emp.TaxpayerRegistrationNumber equals salary.TaxpayerRegistrationNumber
                                   group salary by emp into employeeSalaryGroup
                                   select new
                                   {
                                       Employee = employeeSalaryGroup.Key.FirstName + " " + employeeSalaryGroup.Key.Surname,
                                       TotalSalary = employeeSalaryGroup.Sum(s => s.Amount)
                                   };

            foreach (var item in query12) Console.WriteLine($"\t{item.Employee} \t{item.TotalSalary}");

            Console.WriteLine("\n13. The max salary and the employee:");
            var query13 = from salary in (enterprise.Departments.SelectMany(d => d.Salaries)
                .Where(s => s.Amount == enterprise.Departments.SelectMany(d => d.Salaries).Max(s => s.Amount)))
                join emp in enterprise.Departments.SelectMany(d => d.Employees)
                on salary.TaxpayerRegistrationNumber equals emp.TaxpayerRegistrationNumber
                where salary.Month == new DateTime(2022, 2, 1)
                            select new
                            {
                                Employee = emp.FirstName + " " + emp.Surname,
                                Salary = salary.Amount
                            };

            foreach (var item in query13) Console.WriteLine($"\t{item.Employee} \t{item.Salary}");

            Console.WriteLine("\n14. The total salary in each department in January of 2022:");
            var query14 = enterprise.Departments
            .Select(d => new {
                DepartmentName = d.Name,
                TotalSalary = d.Salaries
            .Where(s => s.Month == new DateTime(2022, 1, 1))
            .Sum(s => s.Amount)
            });

            foreach (var item in query14) Console.WriteLine($"\t{item.DepartmentName} \t{item.TotalSalary}");

            Console.WriteLine("\n15. The duration of work in days for each employee in Descending Order:");
            var query15 = (from emp in enterprise.Departments.SelectMany(d => d.Employees)
                           select new
                           {
                               name = emp.FirstName + " " + emp.Surname,
                               dateBirth = emp.DateOfBirth,
                               duration = DateTime.Now.Subtract(emp.DateOfCommencementOfWork)
                           }).OrderByDescending(i => i.duration).ThenBy(i => i.dateBirth);
            foreach (var item in query15) Console.WriteLine($"\t{item.name} \t{item.dateBirth.ToString("d")} \t{item.duration.Days} days");

            Console.WriteLine("\n16. The age of the oldest employee:");
            var query16 = (from emp in enterprise.Departments.SelectMany(d => d.Employees)
                           select new
                           {
                               name = emp.FirstName + " " + emp.Surname,
                               dateBirth = emp.DateOfBirth,
                               age = (new DateTime(1, 1, 1) + (DateTime.Now - emp.DateOfBirth)).Year - 1
                           }).OrderByDescending(i => i.age).First();
            Console.WriteLine($"\t{query16.name} \t{query16.dateBirth.ToString("d")} \t{query16.age} years");

            Console.WriteLine("\n17. The max salary ever in every department:");
            var query17 = enterprise.Departments
            .Select(d => new {
                DepartmentName = d.Name,
                MaxSalary = d.Salaries
            .Max(s => s.Amount)
            }).OrderByDescending( s => s.MaxSalary);
            foreach (var item in query17) Console.WriteLine($"\t{item.DepartmentName} \t{item.MaxSalary}");

            Console.WriteLine("\n18. Employee information and their salary in January of 2022:");
            var query18 = from e in enterprise.Departments.SelectMany(d => d.Employees)
                          join salary in enterprise.Departments.SelectMany(d => d.Salaries)
                          on e.TaxpayerRegistrationNumber equals salary.TaxpayerRegistrationNumber
                          where salary.Month == new DateTime(2022, 2, 1)
                          orderby e.Type ascending, salary.Amount descending, e.Surname ascending
                          select new
                          {
                              info = $"{e.FirstName} {e.Surname} \t{e.DateOfBirth.ToString("d")} \t {e.Type.ToString()}",
                              amount = salary.Amount
                          };
            foreach (var item in query18) Console.WriteLine($"\t{item.info} \t{item.amount}");

            Console.WriteLine("\n19. The employees who has worked in the enterprise for more than 5 years and the date they started working:");
            var query19 = enterprise.Departments
                .SelectMany(d => d.Employees)
                .Where(e => e.DateOfCommencementOfWork < DateTime.Now.AddYears(-5))
                .OrderBy(e => e.DateOfCommencementOfWork);
            foreach (var item in query19) Console.WriteLine($"\t{item.Surname} {item.FirstName} \t{item.Education} \t{item.Type} \t{item.DateOfCommencementOfWork.ToString("d")}");


            Console.WriteLine("\n20. Amount of full/part time type of employees in each department:");
            var query20 = from dept in enterprise.Departments
                                       from emp in dept.Employees
                                       group emp by new { DepartmentName = dept.Name, emp.Type } into empGroup
                                       orderby empGroup.Key.DepartmentName, empGroup.Key.Type
                                       select new
                                       {
                                           Department = empGroup.Key.DepartmentName,
                                           EmployeeType = empGroup.Key.Type,
                                           EmployeeCount = empGroup.Count()
                                       };

            foreach (var group in query20)
            {
                Console.WriteLine($"\t{group.Department}\t{group.EmployeeType}\t{group.EmployeeCount}");
            }
        }
    }
}
