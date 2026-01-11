using System.Text.Json.Serialization;

namespace CarRentalSystem;

[JsonDerivedType(typeof(Customer), typeDiscriminator: "customer")]
[JsonDerivedType(typeof(Employee), typeDiscriminator: "employee")]
public abstract class Person
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public abstract string Role { get; }
}
