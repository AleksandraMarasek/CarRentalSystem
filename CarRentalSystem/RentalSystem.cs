using System.Text.Json;
using System.IO;

namespace CarRentalSystem;

class RentalSystem
{
    private List<Vehicle> _fleet = new List<Vehicle>();
    private List<Person> _users = new List<Person>();
    private List<RentalRecord> _history = new List<RentalRecord>();
    public decimal TotalRevenue { get; private set; }

    private const string DataFolder = "data";
    private string DbFile = Path.Combine(DataFolder, "database.json");
    private string UsersFile = Path.Combine(DataFolder, "users.json");
    private string HistoryFile = Path.Combine(DataFolder, "history.json");
    private string RevenueFile = Path.Combine(DataFolder, "revenue.txt");

    public void AddVehicle(Vehicle vehicle) { _fleet.Add(vehicle); }

    public void AddUser(Person p) => _users.Add(p);

    public Person? Login(string name, string password)
    {
        return _users.FirstOrDefault(u => u.Name == name && u.Password == password);
    }

    public void ShowAvailableVehicles()
    {
        Console.WriteLine("----- Available Vehicles -----");
        var available = _fleet.Where(v => !v.IsRented).ToList();
        if (!available.Any()) Console.WriteLine("No vehicles available.");
        foreach (var vehicle in available)
        {
            Console.WriteLine($"{vehicle.Brand} {vehicle.Model} ({vehicle.Year}) - ${vehicle.PricePerDay} PLN/day");
        }
    }

    public void RentVehicle(string model, int days, Person user)
    {
        var vehicle = _fleet.FirstOrDefault(v => v.Model.Equals(model, StringComparison.OrdinalIgnoreCase) && !v.IsRented);

        if (vehicle != null)
        {
            decimal baseCost = vehicle.RentalCost(days);
            decimal finalCost = baseCost;

            if (user is Employee emp)
            {
                finalCost = baseCost * emp.Discount;
                Console.WriteLine($"Employee discount applied: -20%");
            }

            vehicle.IsRented = true;
            TotalRevenue += finalCost;

            _history.Add(new RentalRecord
            {
                VehicleModel = vehicle.Model,
                UserName = user.Name,
                Days = days,
                FinalCost = finalCost
            });

            SaveData();
            Console.WriteLine($"Successfully rented {vehicle.Brand} {vehicle.Model} for {days} days. Total: {finalCost} PLN");
        }
        else
        {
            Console.WriteLine("Sorry, the requested vehicle is not available.");
        }
    }

    public void ReturnVehicle(string model)
    {
        var vehicle = _fleet.FirstOrDefault(v => v.Model.Equals(model, StringComparison.OrdinalIgnoreCase) && v.IsRented);
        if (vehicle != null)
        {
            vehicle.IsRented = false;
            SaveData();
            Console.WriteLine($"Vehicle {vehicle.Model} has been returned.");
        }
        else Console.WriteLine("vehicle not currently rented or not found.");
    }

    public void ShowHistory()
    {
        Console.WriteLine("\n----- Rental History -----");
        if (!_history.Any()) Console.WriteLine("No history yet.");
        foreach (var h in _history)
            Console.WriteLine($"{h.Date:yyyy-MM-dd} | {h.UserName} | {h.VehicleModel} | {h.Days} days | {h.FinalCost} PLN");
    }

    public void SaveData()
    {
        Directory.CreateDirectory(DataFolder);
        var options = new JsonSerializerOptions { WriteIndented = true };

        File.WriteAllText(DbFile, JsonSerializer.Serialize(_fleet, options));
        File.WriteAllText(UsersFile, JsonSerializer.Serialize(_users, options));
        File.WriteAllText(HistoryFile, JsonSerializer.Serialize(_history, options));
        File.WriteAllText(RevenueFile, TotalRevenue.ToString());
    }

    public void LoadData()
    {
        if (File.Exists(DbFile)) _fleet = JsonSerializer.Deserialize<List<Vehicle>>(File.ReadAllText(DbFile)) ?? new List<Vehicle>();
        if (File.Exists(UsersFile)) _users = JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(UsersFile)) ?? new List<Person>();
        if (File.Exists(HistoryFile)) _history = JsonSerializer.Deserialize<List<RentalRecord>>(File.ReadAllText(HistoryFile)) ?? new List<RentalRecord>();
        if (File.Exists(RevenueFile) && decimal.TryParse(File.ReadAllText(RevenueFile), out decimal rev)) TotalRevenue = rev;
    }

    public bool HasData() => _fleet.Any();
}
