using System;
using System.Collections.Generic;

namespace lab4
{
    public abstract class Staff
    {
        public Staff() { }
        public abstract string GetName();
        public abstract int GetNumberOfRates();
        public abstract decimal GetSalary();
        public abstract void Display();
        public virtual void Add(Staff staff)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Staff staff)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsComposite()
        {
            return true;
        }
    }

    // Leaf class
    public class Position : Staff
    {
        private string _name;
        private int _numberOfRates;
        private decimal _salary;

        public Position(string name, int numberOfRates, decimal salary)
        {
            this._name = name;
            this._numberOfRates = numberOfRates;
            this._salary = salary;
        }

        public override string GetName()
        {
            return _name;
        }

        public override int GetNumberOfRates()
        {
            return _numberOfRates;
        }

        public override decimal GetSalary()
        {
            return _salary;
        }
        public decimal GetGroupSalary()
        {
            return _salary * _numberOfRates;
        }
        public override bool IsComposite()
        {
            return false;
        }

        public override void Display()
        {
            Console.WriteLine($"Position: {_name}, Number of Rates: {_numberOfRates}, Salary: {_salary}");
        }
    }

    // Composite class
    public class StructuralSubdivision : Staff
    {
        private string _code;
        private string _title;
        private List<Staff> _staffMembers = new List<Staff>();

        public StructuralSubdivision(string code, string title)
        {
            this._code = code;
            this._title = title;
        }

        public override void Add(Staff staffMember)
        {
            _staffMembers.Add(staffMember);
        }

        public override void Remove(Staff staffMember)
        {
            _staffMembers.Remove(staffMember);
        }

        public override string GetName()
        {
            return _title;
        }

        public override int GetNumberOfRates()
        {
            int totalNumberOfRates = 0;
            foreach (var staffMember in _staffMembers)
            {
                totalNumberOfRates += staffMember.GetNumberOfRates();
            }
            return totalNumberOfRates;
        }

        public override decimal GetSalary()
        {
            decimal totalSalary = 0.0M;
            foreach (var staffMember in _staffMembers)
            {
                totalSalary += staffMember.GetSalary() * staffMember.GetNumberOfRates();
            }
            return totalSalary;
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override void Display()
        {
            Console.WriteLine($"Structural Subdivision: {_title} ({_code}), Number of Positions: {_staffMembers.Count}, Number of Rates: {GetNumberOfRates()}, Total Salary: {GetSalary()}");
            foreach (var staffMember in _staffMembers)
            {
                staffMember.Display();
            }
        }
    }

    public class Corporation : Staff
    {
        private string _title;
        private List<Staff> _subDivisions = new List<Staff>();

        public Corporation(string title)
        {
            this._title = title;
        }

        public override void Add(Staff subDivision)
        {
            _subDivisions.Add(subDivision);
        }

        public override void Remove(Staff subDivision)
        {
            _subDivisions.Remove(subDivision);
        }

        public override string GetName()
        {
            return _title;
        }

        public override int GetNumberOfRates()
        {
            int totalNumberOfRates = 0;
            foreach (var subDivision in _subDivisions)
            {
                totalNumberOfRates += subDivision.GetNumberOfRates();
            }
            return totalNumberOfRates;
        }

        public override decimal GetSalary()
        {
            decimal totalSalary = 0.0M;
            foreach (var subDivision in _subDivisions)
            {
                totalSalary += subDivision.GetSalary();
            }
            return totalSalary;
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override void Display()
        {
            Console.WriteLine($"Corporation name: {_title}, Number of Sub Divisions: {_subDivisions.Count}, Number of Rates: {GetNumberOfRates()}, Total Salary: {GetSalary()}");
            foreach (var subDivision in _subDivisions)
            {
                subDivision.Display();
            }
        }
    }

    public class Client
    {
        public void DisplayStructure(Staff component)
        {
            component.Display();
        }

        public void AddIfComposite(Staff component1, Staff component2)
        {
            if (component1.IsComposite())
            {
                component1.Add(component2);
            }
        }

        public void RemoveIfComposite(Staff component1, Staff component2)
        {
            if (component1.IsComposite())
            {
                component1.Remove(component2);
            }
        }
    }
}
