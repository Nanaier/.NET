using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1
{
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
}