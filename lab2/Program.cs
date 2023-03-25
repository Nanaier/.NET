using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            #region creating data
            var enterprise = new Enterprise();

            // Create a new department
            var department1 = new Department(1, "Sales");
            var department2 = new Department(2, "IT");
            var department3 = new Department(3, "Design");

            // Create some employees and their monthly salaries
            var employee1 = new Employee(1, "Smith", "John", new DateTime(1980, 1, 1), 1, 123456789, "Bachelor's Degree", "Sales", new DateTime(2010, 1, 1), EmployeeType.FullTime);
            var employee2 = new Employee(2, "Doe", "Jane", new DateTime(1985, 5, 10), 2, 987654321, "Master's Degree", "Marketing", new DateTime(2012, 3, 1), EmployeeType.PartTime);
            var employee3 = new Employee(3, "Lee", "David", new DateTime(1992, 9, 20), 3, 456789012, "Bachelor's Degree", "IT", new DateTime(2018, 6, 1), EmployeeType.FullTime);
            var employee4 = new Employee(4, "Garcia", "Maria", new DateTime(1990, 3, 18), 4, 567890123, "Master's Degree", "Finance", new DateTime(2016, 9, 1), EmployeeType.FullTime);
            var employee5 = new Employee(5, "Wang", "Li", new DateTime(1988, 11, 2), 5, 234567890, "Bachelor's Degree", "Human Resources", new DateTime(2017, 5, 1), EmployeeType.PartTime);
            var employee6 = new Employee(6, "Kim", "Min", new DateTime(1995, 7, 13), 6, 345678901, "Bachelor's Degree", "Operations", new DateTime(2020, 1, 1), EmployeeType.FullTime);

            var count_salary_id = 1;
            DateTime endDate = DateTime.Now;
            decimal CalculateMonthlySalary(Employee employee, DateTime date)
            {
                // Calculate the number of years the employee has been with the company
                int yearsEmployed = date.Year - employee.DateOfCommencementOfWork.Year;

                // Check if the employee has completed a full year of employment
                if (date < employee.DateOfCommencementOfWork.AddYears(yearsEmployed))
                {
                    yearsEmployed--;
                }

                // Calculate the base salary based on the employee's job type
                decimal baseSalary = 0;
                switch (employee.Type)
                {
                    case EmployeeType.FullTime:
                        baseSalary = 50000m;
                        break;
                    case EmployeeType.PartTime:
                        baseSalary = 30000m;
                        break;
                        // Add more cases for other job types
                }

                // Calculate the salary multiplier based on the employee's years of experience
                decimal salaryMultiplier = 1 + (yearsEmployed * 0.05m);

                // Calculate the final monthly salary
                decimal monthlySalary = (baseSalary * salaryMultiplier) / 12;

                return monthlySalary;
            }
            void create(Employee employee, List<MonthlySalary> salaries, List<MonthlySalary> allSalaries)
            {
                // Loop through each month from start date to end date
                for (DateTime date = employee.DateOfCommencementOfWork; date <= endDate; date = date.AddMonths(1))
                {
                    // Calculate the monthly salary for the employee
                    decimal monthlySalary = CalculateMonthlySalary(employee, date);

                    // Create a new MonthlySalary object for the current month
                    var monthlySalaryObj = new MonthlySalary(count_salary_id, employee.TaxpayerRegistrationNumber, date, monthlySalary);
                    count_salary_id++;
                    // Add the MonthlySalary object to the list
                    salaries.Add(monthlySalaryObj);
                    allSalaries.Add(monthlySalaryObj);
                }
            }
            var salaries1 = new List<MonthlySalary> {};
            var salaries2 = new List<MonthlySalary> {};
            var salaries3 = new List<MonthlySalary> {};
            var salaries = new List<MonthlySalary> { };
            // Using helper function create MonthlySalary objects for each employee
            create(employee1, salaries1, salaries);
            create(employee2, salaries2, salaries);
            create(employee3, salaries2, salaries);
            create(employee4, salaries1, salaries);
            create(employee5, salaries3, salaries);
            create(employee6, salaries3, salaries);

            // Group employees by department
            var employees1 = new List< Employee> { employee1, employee4 };
            var employees2 = new List< Employee> { employee2, employee3};
            var employees3 = new List< Employee> { employee5, employee6 };
            var employees = new List<Employee> { employee1, employee2, employee3, employee4, employee5, employee6 };

            // Add the employees and salaries to the department
            department1.AddEmployees(employees1);
            department2.AddEmployees(employees2);
            department3.AddEmployees(employees3);

            department1.AddSalaries(salaries1);
            department2.AddSalaries(salaries2);
            department3.AddSalaries(salaries3);
            var departments = new List<Department> { department1, department2, department3 };

            // Add the department to the enterprise
            enterprise.AddDepartment(department1);
            enterprise.AddDepartment(department2);
            enterprise.AddDepartment(department3);


            #endregion

            #region Serialization and Deserialization


            XmlSerializer serializer = new XmlSerializer(typeof(Enterprise));
            using (TextWriter writer = new StreamWriter("data.xml"))
            {
                serializer.Serialize(writer, enterprise);
            }

            // Deserialize from XML
            using (TextReader reader = new StreamReader("data.xml"))
            {
                Enterprise ent = (Enterprise)serializer.Deserialize(reader);

                Console.WriteLine("Departments:");
                foreach (Department department in ent.Departments)
                {
                    Console.WriteLine($"\tDepartment Id: {department.Id}, Name: {department.Name}");

                    Console.WriteLine("\tEmployees:");
                    foreach (Employee employee in department.Employees)
                    {
                        Console.WriteLine($"\t\tEmployee Id: {employee.Id}," +
                            $" Surname: {employee.Surname}," +
                            $" First Name: {employee.FirstName}," +
                            $" Date of Birth: {employee.DateOfBirth.ToString("d")}," +
                            $" \n\t\tRoll Number: {employee.RollNumber}," +
                            $" Taxpayer Registration Number: {employee.TaxpayerRegistrationNumber}," +
                            $" Education: {employee.Education}," +
                            $" \n\t\tSpeciality: {employee.Specialty}," +
                            $" Date of Commencement of Work: {employee.DateOfCommencementOfWork.ToString("d")}," +
                            $" Type: {employee.Type}\n");
                        var sal = from salary in department.Salaries
                                  where salary.TaxpayerRegistrationNumber == employee.TaxpayerRegistrationNumber
                                  select salary;
                        Console.WriteLine("\t\tMonthly Salaries:");
                        foreach (MonthlySalary salary in sal)
                        {
                            Console.WriteLine($"\t\t\tSalary Id: {salary.Id}, Taxpayer Registration Number: {salary.TaxpayerRegistrationNumber}, Month: {salary.Month.ToString("d")}, Amount: {Math.Round(salary.Amount, 3)}");
                        }
                        Console.WriteLine("\n");
                    }
                }
            }



            #endregion
            #region Queries

            XDocument data = XDocument.Load("data.xml");
            // This query selects the names of all departments in the enterprise and then prints them on the console

            var query1 = from department in data.Descendants("Department")
                                  select department.Element("Name").Value;
            Console.WriteLine("\n\n1. Department Names:");
            foreach (var name in query1)
            {
                Console.WriteLine($"\t{name}");
            }


            //This query selects the names and employee count of all departments in the enterprise and then prints them on the console
            Console.WriteLine("\n2. Amount of people in each department:");
            var query2 = from department in data.Descendants("Department")
                         select new
                         {
                             name = department.Element("Name").Value,
                             amount =  department.Element("Employees").Elements("Employee").Count()
                         };
            foreach (var item in query2) Console.WriteLine($"\t{item.name} \t{item.amount}");
            

            //This query selects the first name and surname of all employees in the enterprise who have a bachelor's degree, and then prints them on the console
            Console.WriteLine("\n3. Employees with bachelor's degree:");
            var query3 = data.Descendants("Employee")
                 .Where(e => (string)e.Element("Education") == "Bachelor's Degree");
            foreach (var employee in query3)
            {
                Console.WriteLine($"\t{employee.Element("FirstName").Value} {employee.Element("Surname").Value}");
            }
           
            //This query selects the first name and surname of all employees in the enterprise, orders them by surname in ascending order, and then by first name,
            //and then prints them on the console
            Console.WriteLine("\n4. Employee's Names in Ascending Order:");
            var query4 = data.Descendants("Employee")
                 .OrderBy(e => (string)e.Element("Surname"))
                 .ThenBy(e => (string)e.Element("FirstName"));
            foreach (var emp in query4)
            {
                Console.WriteLine($"\t{emp.Element("Surname").Value} {emp.Element("FirstName").Value}");
            }


            //This query selects the first name, surname, and date of birth of all employees in the enterprise who were born after or in 1990, and then prints them on the console
            Console.WriteLine("\n5. Employees that were born after or in 1990");
            var query5 = data.Descendants("Employee")
                 .Where(e => DateTime.Parse(e.Element("DateOfBirth").Value).Year >= 1990);
            foreach (var employee in query5)
            {
                Console.WriteLine($"\t{employee.Element("Surname").Value} {employee.Element("FirstName").Value} \t{DateTime.Parse(employee.Element("DateOfBirth").Value).ToString("d")}");
            }


            //This query selects the first name and surname of all employees in the enterprise whose first name starts with 'M', orders them by the first name,
            //and then prints them on the console
            Console.WriteLine("\n6. Employees whose First Names start with M in Ascending Order:");
            var query6 = from emp in data.Descendants("Employee")
                         where emp.Element("FirstName").Value.StartsWith("M", StringComparison.InvariantCultureIgnoreCase)
                         orderby emp.Element("FirstName").Value
                         select new
                         {
                             FirstName = emp.Element("FirstName").Value,
                             Surname = emp.Element("Surname").Value
                         };

            foreach (var emp in query6)
            {
                Console.WriteLine($"\t{emp.FirstName} {emp.Surname}");
            }


            //This query selects the first name, surname, and date of birth of all employees in two specific departments using CONCAT and then prints them on the console
            Console.WriteLine($"\n7. Employees from {department1.Name} and {department2.Name} Departments:");
            var query7 = data.Descendants("Department")
                             .Where(d => (int)d.Element("Id") == department1.Id || (int)d.Element("Id") == department2.Id)
                             .Descendants("Employee")
                             .Select(e => new Employee
                             {
                                 FirstName = (string)e.Element("FirstName"),
                                 Surname = (string)e.Element("Surname"),
                                 DateOfBirth = DateTime.Parse((string)e.Element("DateOfBirth")),
                                 Education = (string)e.Element("Education")
                             })
                             .Distinct();
            foreach (var emp in query7) Console.WriteLine($"\t{emp.FirstName} {emp.Surname} \t{emp.DateOfBirth:d}");


            //This query calculates the average salary of all employees in the enterprise and then prints it on the console.
            var query8 = data.Descendants("MonthlySalary").Average(s => (double)s.Element("Amount"));
            Console.WriteLine($"\n8. Average monthly salary in the whole enterprise: {Math.Round(query8, 3)}");


            //This query calculates the average salary of all employees in each department of the enterprise and then prints it on the console
            Console.WriteLine("\n9. Average salary in the each department:");

            var query9 = from department in data.Descendants("Department")
                         select new
                         {
                             name = department.Element("Name").Value,
                             avg_amount = Math.Round(department.Descendants("MonthlySalary").Average(s => (double)s.Element("Amount")), 3)
                         };

            foreach (var item in query9)
            {
                Console.WriteLine($"\t{item.name} \t {item.avg_amount}");
            }


            //This query calculates the count of full-time and part-time employees in the enterprise and then prints it on the console
            Console.WriteLine("\n10. Amount of FullTime and PartTime employees:");

            var query10 = data.Descendants("Employee")
                              .GroupBy(e => e.Element("Type").Value)
                              .Select(empType => new {
                                  name = empType.Key,
                                  amount = empType.Count()
                              });

            foreach (var item in query10)
            {
                Console.WriteLine($"\t{item.name} \t {item.amount}");
            }


            //This query calculates the maximum salary of full-time or part-time employees in the enterprise and then prints it on the console
            Console.WriteLine("\n11. Max salary of FullTime or PartTime employee:");

            var query11 = from e in data.Descendants("Employee")
                          join s in data.Descendants("MonthlySalary")
                          on (string)e.Element("TaxpayerRegistrationNumber") equals (string)s.Element("TaxpayerRegistrationNumber")
                          group s by (string)e.Element("Type") into empType
                          select new
                          {
                              name = empType.Key,
                              amount = empType.Max(s => (decimal)s.Element("Amount"))
                          };
            foreach (var item in query11) Console.WriteLine($"\t{item.name} \t {item.amount}");


            //This query calculates the total salary of each employee during their work in the enterprise in the enterprise and then prints it on the console
            Console.WriteLine("\n12. Total salary for each employee:");
            var query12 = from emp in data.Descendants("Employee")
                          join salary in data.Descendants("MonthlySalary")
                          on emp.Element("TaxpayerRegistrationNumber").Value equals salary.Element("TaxpayerRegistrationNumber").Value
                          group salary by emp into employeeSalaryGroup
                          select new
                          {
                              Employee = employeeSalaryGroup.Key.Element("FirstName").Value + " " + employeeSalaryGroup.Key.Element("Surname").Value,
                              TotalSalary = Math.Round(employeeSalaryGroup.Sum(s => (double)s.Element("Amount")), 3)
                          };

            foreach (var item in query12) Console.WriteLine($"\t{item.Employee} \t{item.TotalSalary}");

            //This query selects the employee with the maximum salary in the enterprise and then prints the name and salary on the console
            Console.WriteLine("\n13. The max salary and the employee:");
            var maxSalary = enterprise.Departments.SelectMany(d => d.Salaries).Max(s => s.Amount);
            var query13 = (from salary in data.Descendants("MonthlySalary")
                           where (decimal)salary.Element("Amount") == maxSalary
                           join emp in data.Descendants("Employee")
                           on (string)salary.Element("TaxpayerRegistrationNumber") equals (string)emp.Element("TaxpayerRegistrationNumber")
                           select new
                           {
                               Employee = (string)emp.Element("FirstName") + " " + (string)emp.Element("Surname"),
                               Salary = (decimal)salary.Element("Amount")
                           }).FirstOrDefault();
            Console.WriteLine($"\t{query13.Employee} \t{Math.Round(query13.Salary, 3)}");


            //This query calculates the total salary of all employees in each department in January of 2022 and then prints it on the console
            Console.WriteLine("\n14. The total salary in each department in January of 2022:");
            var query14 = data.Descendants("Department")
            .Select(d => new {
                DepartmentName = d.Element("Name").Value,
                TotalSalary = d.Descendants("MonthlySalary")
                    .Where(s => DateTime.Parse(s.Element("Month").Value) == new DateTime(2022, 1, 1))
                    .Sum(s => (double)s.Element("Amount"))
            });

            foreach (var item in query14) Console.WriteLine($"\t{item.DepartmentName} \t{Math.Round(item.TotalSalary, 3)}");


            //This query selects the names of all employees in the enterprise and their duration of work in days, orders them in descending
            //order based on their duration of work, and then prints the results on the console
            Console.WriteLine("\n15. The duration of work in days for each employee in Descending Order:");
            var query15 = from emp in data.Descendants("Employee")
                          select new
                          {
                              name = emp.Element("FirstName").Value + " " + emp.Element("Surname").Value,
                              dateBirth = DateTime.Parse(emp.Element("DateOfBirth").Value),
                              duration = DateTime.Now.Subtract(DateTime.Parse(emp.Element("DateOfCommencementOfWork").Value))
                          } into empDuration
                          orderby empDuration.duration descending, empDuration.dateBirth
                          select empDuration;

            foreach (var item in query15) Console.WriteLine($"\t{item.name} \t{item.dateBirth.ToString("d")} \t{item.duration.Days} days");


            //This query calculates the age of the oldest employee in the enterprise and then prints their name, date of birth, and age on the console
            Console.WriteLine("\n16. The age of the oldest employee:");
            var query16 = (from emp in data.Descendants("Employee")
                           let dateBirth = DateTime.Parse(emp.Element("DateOfBirth").Value)
                           let age = (new DateTime(1, 1, 1) + (DateTime.Now - dateBirth)).Year - 1
                           orderby age descending
                           select new
                           {
                               name = emp.Element("FirstName").Value + " " + emp.Element("Surname").Value,
                               dateBirth = dateBirth,
                               age = age
                           }).First();
            Console.WriteLine($"\t{query16.name} \t{query16.dateBirth.ToString("d")} \t{query16.age} years");


            //This query calculates the maximum salary ever earned by an employee in each department and then prints the results on the console
            Console.WriteLine("\n17. The max salary ever in every department:");
            var query17 = data.Descendants("Department")
                .Select(d => new {
                    DepartmentName = d.Element("Name").Value,
                    MaxSalary = d.Descendants("MonthlySalary")
                        .Max(s => (double)s.Element("Amount"))
                }).OrderByDescending(s => s.MaxSalary);

            foreach (var item in query17)
                Console.WriteLine($"\t{item.DepartmentName} \t{Math.Round(item.MaxSalary, 3)}");


            //This query selects the information of employees and their salary in January of 2022 and orders the results by employee type, salary
            //amount, and surname in ascending order. The query then prints the results on the console.
            Console.WriteLine("\n18. Employee information and their salary in January of 2022:");
            var query18 = from e in data.Descendants("Employee")
                          join salary in data.Descendants("MonthlySalary")
                          on e.Element("TaxpayerRegistrationNumber").Value equals salary.Element("TaxpayerRegistrationNumber").Value
                          where DateTime.Parse(salary.Element("Month").Value).Month == 1 && DateTime.Parse(salary.Element("Month").Value).Year == 2022
                          orderby e.Element("Type").Value ascending, (double)salary.Element("Amount") descending, e.Element("Surname").Value ascending
                          select new
                          {
                              info = $"{e.Element("FirstName").Value} {e.Element("Surname").Value} \t{DateTime.Parse(e.Element("DateOfBirth").Value).ToString("d")} \t {e.Element("Type").Value.ToString()}",
                              amount = Math.Round((double)salary.Element("Amount"), 3)
                          };
            foreach (var item in query18) Console.WriteLine($"\t{item.info} \t{item.amount}");

            //This query selects the employees who have worked in the enterprise for more than 5 years and the date they started working, and then
            //orders the results based on their date of commencement of work. The query then prints the results on the console.
            Console.WriteLine("\n19. The employees who has worked in the enterprise for more than 5 years and the date they started working:");
            var query19 = from e in data.Descendants("Employee")
                          where DateTime.Parse(e.Element("DateOfCommencementOfWork").Value) < DateTime.Now.AddYears(-5)
                          orderby DateTime.Parse(e.Element("DateOfCommencementOfWork").Value) ascending
                          select new
                          {
                              Name = e.Element("Surname").Value + " " + e.Element("FirstName").Value,
                              Education = e.Element("Education").Value,
                              Type = e.Element("Type").Value,
                              DateStarted = DateTime.Parse(e.Element("DateOfCommencementOfWork").Value).ToString("d")
                          };
            foreach (var item in query19) Console.WriteLine($"\t{item.Name} \t{item.Education} \t{item.Type} \t{item.DateStarted}");


            //This query groups employees in each department by their employment type (full-time/part-time) and then counts the number of employees
            //in each group. The query then prints the results on the console ordered by department name and employee type.
            Console.WriteLine("\n20. Amount of full/part time type of employees in each department:");
            var query20 = from dept in data.Descendants("Department")
                          from emp in dept.Descendants("Employee")
                          group emp by new { DepartmentName = dept.Element("Name").Value, Type = emp.Element("Type").Value } into empGroup
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
            #endregion
        }
    }
}

