using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem;

class RentalSystem
{
    private List<Vehicle> _fleet = new List<Vehicle>();
    public decimal TotalRevenue { get; private set; }

    public void AddVehicle(Vehicle vehicle)
    {
        _fleet.Add(vehicle);
    }

    public void ShowAvailableVehicles()
    {
        Console.WriteLine("----- Available Vehicles -----");
        foreach (var vehicle in _fleet.Where(vehicle => !vehicle.IsRented))
        {
            Console.WriteLine($"{vehicle.Brand} {vehicle.Model} ({vehicle.Year}) - ${vehicle.PricePerDay}/day");
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
            Console.WriteLine($"You have rented the {vehicle.Brand} {vehicle.Model} for {days} days. Total cost: ${cost}");
        }
        else
        {
            Console.WriteLine("Sorry, the requested vehicle is not available.");
        }
    }
}
