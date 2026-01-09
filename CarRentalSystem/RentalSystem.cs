using System.Text.Json;
using System.IO;

namespace CarRentalSystem;

class RentalSystem
{
    private List<Vehicle> _fleet = new List<Vehicle>();
    public decimal TotalRevenue { get; private set; }

    private const string DataFolder = "data";
    private string DbFile = Path.Combine(DataFolder, "database.json");
    private string RevenueFile = Path.Combine(DataFolder, "revenue.txt");

    public void AddVehicle(Vehicle vehicle)
    {
        _fleet.Add(vehicle);
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

    public void RentVehicle(string model, int days)
    {
        var vehicle = _fleet.FirstOrDefault(v => v.Model.Equals(model, StringComparison.OrdinalIgnoreCase) && !v.IsRented);

        if (vehicle != null)
        {
            decimal cost = vehicle.RentalCost(days);
            vehicle.IsRented = true;
            TotalRevenue += cost;
            SaveData();
            Console.WriteLine($"You have rented the {vehicle.Brand} {vehicle.Model} for {days} days. Total cost: ${cost}");
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
        else Console.WriteLine("This vehicle is not currently rented.");
    }

    public void SaveData()
    {
        Directory.CreateDirectory(DataFolder);

        var json = JsonSerializer.Serialize(_fleet, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(DbFile, json);
        File.WriteAllText(RevenueFile, TotalRevenue.ToString());
    }

    public void LoadData()
    {
        if (File.Exists(DbFile))
        {
            var json = File.ReadAllText(DbFile);
            _fleet = JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>();
        }

        if (File.Exists(RevenueFile))
        {
            if (decimal.TryParse(File.ReadAllText(RevenueFile), out decimal savedRevenue))
            {
                TotalRevenue = savedRevenue;
            }
        }
    }

    public bool HasData() => _fleet.Any();
}
