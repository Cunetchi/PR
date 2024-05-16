using System;
using System.Collections.Generic;

// Flyweight interface
interface IRentCarFlyweight
{
    void Rent();
}

// ConcreteFlyweight class
class RentCarFlyweight : IRentCarFlyweight
{
    private string _carDetails;

    public RentCarFlyweight(string carDetails)
    {
        _carDetails = carDetails;
    }

    public void Rent()
    {
        Console.WriteLine($"Renting car with details: {_carDetails}");
    }
}

// FlyweightFactory class
class RentCarFlyweightFactory
{
    private Dictionary<string, IRentCarFlyweight> _flyweights = new Dictionary<string, IRentCarFlyweight>();

    public IRentCarFlyweight GetFlyweight(string carDetails)
    {
        if (!_flyweights.ContainsKey(carDetails))
        {
            _flyweights[carDetails] = new RentCarFlyweight(carDetails);
        }
        return _flyweights[carDetails];
    }

    public int GetTotalFlyweights()
    {
        return _flyweights.Count;
    }
}

// Client
class Program
{
    static void Main()
    {
        RentCarFlyweightFactory factory = new RentCarFlyweightFactory();

        // Renting cars with different details
        IRentCarFlyweight car1 = factory.GetFlyweight("Toyota Corolla 2022");
        car1.Rent();

        IRentCarFlyweight car2 = factory.GetFlyweight("Honda Civic 2021");
        car2.Rent();

        // Renting the same car as car1
        IRentCarFlyweight car3 = factory.GetFlyweight("Toyota Corolla 2022");
        car3.Rent();

        Console.WriteLine($"Total number of flyweights: {factory.GetTotalFlyweights()}");
    }
}