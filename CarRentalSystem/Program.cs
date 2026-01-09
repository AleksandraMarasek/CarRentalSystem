using CarRentalSystem;

RentalSystem rentalSystem = new RentalSystem();

rentalSystem.AddVehicle(new ManualCar
{
    Brand = "Opel",
    Model = "Astra",
    Year = 1998,
    PricePerDay = 40
});
rentalSystem.AddVehicle(new AutomaticCar
{
    Brand = "BMW",
    Model = "X5",
    Year = 2020,
    PricePerDay = 120
});
rentalSystem.AddVehicle(new ManualCar
{
    Brand = "Toyota",
    Model = "Corolla",
    Year = 2015,
    PricePerDay = 60
});
rentalSystem.AddVehicle(new Motorcycle
{
    Brand = "Yamaha",
    Model = "YZF-R3",
    Year = 2019,
    PricePerDay = 50
});
rentalSystem.AddVehicle(new AutomaticCar
{
    Brand = "Audi",
    Model = "A6",
    Year = 2018,
    PricePerDay = 110
});
rentalSystem.AddVehicle(new Van 
{ 
    Brand = "Ford", 
    Model = "Transit", 
    PricePerDay = 200 
});


bool running = true;
while (running)
{
    Console.WriteLine("\n----- Car Rental System -----");
    Console.WriteLine("1. Show Available vehicles");
    Console.WriteLine("2. Choose  your vehicle");
    Console.WriteLine("3. Show total revenue");
    Console.WriteLine("4. Exit");
    Console.Write("Enter number: ");

    switch (Console.ReadLine())
    {
        case "1":
            rentalSystem.ShowAvailableVehicles();
            break;
        case "2":
            Console.Write("Enter car model: ");
            string model = Console.ReadLine();
            Console.Write("For how many days? ");
            if (int.TryParse(Console.ReadLine(), out int days))
                rentalSystem.RentVehicle(model, days);
            break;
        case "3":
            Console.WriteLine($"\nTotal revenue: {rentalSystem.TotalRevenue} PLN");
            break;
        case "4":
            running = false;
            break;
    }
}