using CarRentalSystem;

RentalSystem rentalSystem = new RentalSystem();
rentalSystem.LoadData();

rentalSystem.AddUser(new Employee { Name = "Ola", Password = "12" });
rentalSystem.AddUser(new Customer { Name = "Filip", Password = "32" });


Console.WriteLine("Welcome to Car Rental!");
Console.WriteLine("\nWhen choosing a car type in its model. (Example Opel Astra -> Astra)\n");

Console.Write("Name: ");
string name = Console.ReadLine() ?? "";
Console.Write("Password: ");
string pass = Console.ReadLine() ?? "";

Person? currentUser = rentalSystem.Login(name, pass);

if (currentUser == null)
{
    Console.WriteLine("Login failed!");
    return;
}

Console.WriteLine($"\nLogged in as: {currentUser.Name} ({currentUser.Role})");

bool running = true;
while (running)
{
    Console.WriteLine("\n----- Car Rental System -----");
    Console.WriteLine("1. Show Available vehicles");
    Console.WriteLine("2. Rent vehicle");
    if (currentUser is Employee)
    {
        Console.WriteLine("3. View History & Revenue");
    }
    Console.WriteLine("4. Return vehicle");
    Console.WriteLine("5. Exit");
    Console.Write("\nEnter number: ");


    switch (Console.ReadLine())
    {
        case "1":
            rentalSystem.ShowAvailableVehicles();
            break;
        case "2":
            Console.Write("Enter car model: ");
            string model = Console.ReadLine() ?? "";
            Console.Write("For how many days? ");
            if (int.TryParse(Console.ReadLine(), out int days))
                rentalSystem.RentVehicle(model, days, currentUser);
            break;
        case "3":
            if (currentUser is Employee)
            {
                rentalSystem.ShowHistory();
                Console.WriteLine($"Total Revenue: {rentalSystem.TotalRevenue} PLN");
            }
            break;
        case "4":
            Console.Write("Enter model to return: ");
            string ret = Console.ReadLine() ?? "";
            rentalSystem.ReturnVehicle(ret);
            break;
        case "5":
            rentalSystem.SaveData();
            running = false;
            break;
    }
}

/*
 HOW TO RUN IN DOCKER:

docker build -t car-app .
docker run -it --rm -v ${PWD}/data:/app/data car-app
 
 */