using System;

// Subject interface
interface IRentCar
{
    void Rent(); // Metoda pentru închirierea mașinii
}

// RealSubject class
class RentCar : IRentCar
{
    public void Rent()
    {
        Console.WriteLine("Renting a car."); // Implementarea reală a închirierii mașinii
    }
}

// Proxy class
class RentCarProxy : IRentCar
{
    private RentCar _realSubject; // Referință către RealSubject

    public void Rent()
    {
        if (_realSubject == null) // Verificăm dacă RealSubject nu a fost încă creat
        {
            Console.WriteLine("Creating RentCar instance.");
            _realSubject = new RentCar(); // Creăm RealSubject dacă nu există deja
        }

        _realSubject.Rent(); // Apelăm metoda Rent a RealSubject pentru a închiria mașina
    }
}

// Client
class Program
{
    static void Main()
    {
        // Utilizarea Proxy-ului pentru a închiria mașina
        RentCarProxy proxy = new RentCarProxy();
        proxy.Rent();
    }
}