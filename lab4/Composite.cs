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
            Console.WriteLine($"\tPosition: {_name}, Number of Rates: {_numberOfRates}, Salary: {_salary}");
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
            return ($"{_title} ({_code})");
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
                if (!staffMember.IsComposite())
                    totalSalary += staffMember.GetSalary() * staffMember.GetNumberOfRates();
                else
                    totalSalary += staffMember.GetSalary();
            }
            return totalSalary;
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override void Display()
        {
            var stack = new Stack<(Staff, int)>();
            stack.Push((this, 0));

            while (stack.Count > 0)
            {
                var (staffMember, depth) = stack.Pop();

                for (int i = 0; i < depth; i++)
                {
                    Console.Write('\t');
                }

                if (staffMember.IsComposite())
                {
                    Console.WriteLine($"Structure: {staffMember.GetName()}, Number of Positions: {staffMember.GetNumberOfRates()}, Total Salary: {staffMember.GetSalary()}");
                    var composite = staffMember as StructuralSubdivision;
                    for (int i = composite._staffMembers.Count - 1; i >= 0; i--)
                    {
                        stack.Push((composite._staffMembers[i], depth + 1));
                    }
                }
                else
                {
                    staffMember.Display();
                }
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
