namespace CarRentalSystem;

class ManualCar : Vehicle
{
    public override decimal RentalCost(int days)
    {
        return PricePerDay * days;
    }
}
