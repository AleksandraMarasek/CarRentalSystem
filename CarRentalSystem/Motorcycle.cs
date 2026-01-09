namespace CarRentalSystem;

class Motorcycle : Vehicle
{
    public override decimal RentalCost(int days)
    {
        return PricePerDay * days * 0.8m;
    }
}
