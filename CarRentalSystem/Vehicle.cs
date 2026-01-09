using System.Text.Json.Serialization;

namespace CarRentalSystem;

[JsonDerivedType(typeof(ManualCar), typeDiscriminator: "manual")]
[JsonDerivedType(typeof(AutomaticCar), typeDiscriminator: "automatic")]
[JsonDerivedType(typeof(Van), typeDiscriminator: "van")]
[JsonDerivedType(typeof(Motorcycle), typeDiscriminator: "motorcycle")]

abstract class Vehicle
{
    public string Brand {  get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsRented { get; set; }

    public abstract decimal RentalCost(int days);
}
