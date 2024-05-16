using System;

// Adaptee - Clasa existentă pe care vrem să o adaptăm clientului
class CarRentalService
{
    public void RentCar(string carType)
    {
        Console.WriteLine($"Renting a {carType} car.");
    }

    public void RentCheapCar()
    {
        RentCar("cheap");
    }
}

// Target - Interfața pe care o va primi clientul
interface ICarRentalService
{
    void RentLuxuryCar();
    void RentSuvCar();
}

// Adapter - Adaptează clasa CarRentalService la interfața ICarRentalService
class CarRentalAdapter : ICarRentalService
{
    private CarRentalService _carRentalService;

    public CarRentalAdapter(CarRentalService carRentalService)
    {
        _carRentalService = carRentalService;
    }

    public void RentLuxuryCar()
    {
        _carRentalService.RentCar("luxury");
    }

    public void RentSuvCar()
    {
        _carRentalService.RentCar("suv");
    }
}

// Client
class Program
{
    static void Main()
    {
        // Cream o instanță la Adaptee
        CarRentalService carRentalService = new CarRentalService();

        // Cream o instanță la Adapter, și o parsăm către instanța Adaptee
        ICarRentalService adapter = new CarRentalAdapter(carRentalService);

        // Utilizăm adapterul pentru a închiria o mașină de lux
        adapter.RentLuxuryCar();

        // Utilizăm direct serviciul existent pentru a închiria o mașină ieftină
        carRentalService.RentCheapCar();
    }
}