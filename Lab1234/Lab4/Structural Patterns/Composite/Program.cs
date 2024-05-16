using System;
using System.Collections.Generic;

// Componenta principala
interface IEmployee
{
    void DisplayDetails();
}

// Angajati 
class Employee : IEmployee
{
    public string Name { get; set; }
    public string Position { get; set; }

    public void DisplayDetails()
    {
        Console.WriteLine($"Employee: {Name}, Position: {Position}");
    }
    
}
//Manageri vanzari
class Sales_Manager : IEmployee
{
    public string Name { get; set; }
    public string Position { get; set; }
    
    private List<IEmployee> subordinates = new List<IEmployee>();

    public void AddSubordinate(IEmployee employee)
    {
        subordinates.Add(employee);
    }

    public void RemoveSubordinate(IEmployee employee)
    {
        subordinates.Remove(employee);
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Sales_Manager: {Name}, Position: {Position}");
        foreach (var subordinate in subordinates)
        {
            subordinate.DisplayDetails();
        }
    }

}

// Administratori
class Administrator : IEmployee
{
    public string Name { get; set; }
    public string Position { get; set; }
    private List<IEmployee> subordinates = new List<IEmployee>();

    public void AddSubordinate(IEmployee Sales_Manager)
    {
        subordinates.Add(Sales_Manager);
    }

    public void RemoveSubordinate(IEmployee Sales_Manager)
    {
        subordinates.Remove(Sales_Manager);
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Administrator: {Name}, Position: {Position}");
        foreach (var subordinate in subordinates)
        {
            subordinate.DisplayDetails();
        }
    }
}

// Client
class Program
{
    static void Main()
    {
        Employee emp = new Employee { Name = "Boris Cazacu", Position = "Sales Manager" };

        Sales_Manager sales_manager = new Sales_Manager { Name = "Dumitru Nimerenco", Position = "Sales Team Leader" };
        sales_manager.AddSubordinate(emp);
        
        Administrator admin = new Administrator { Name = "Daniel Cojocaru", Position = "Administrator" };
        admin.AddSubordinate(sales_manager);
        
        admin.DisplayDetails();
    }
}