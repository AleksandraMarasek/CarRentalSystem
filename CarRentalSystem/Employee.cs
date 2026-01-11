namespace CarRentalSystem;

public class Employee : Person
{
    public override string Role => "Employee";
    public decimal Discount => 0.8m;
}
