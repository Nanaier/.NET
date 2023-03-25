using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace lab2
{
    /*
     The Enterprise class represents a company and contains a list of departments. It has a constructor that initializes
     an empty list of departments and a method AddDepartment to add a new department to the list.The Departments property
     provides read-only access to the list of departments, which can be useful for querying the company's organizational structure.
     */
    [XmlRoot("Enterprise")]
    public class Enterprise
    {
        [XmlElement("Department")]
        public List<Department> Departments;

        public Enterprise()
        {
            Departments = new List<Department>();
        }
        //public IReadOnlyList<Department> Departments => _departments;
        public void AddDepartment(Department department)
        {
            Departments.Add(department);
        }

    }
}