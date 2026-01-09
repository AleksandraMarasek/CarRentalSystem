namespace CarRentalSystem;

 class AutomaticCar : Vehicle
{
    public override decimal RentalCost(int days)
    {
        return PricePerDay * days * 1.15m ;
    }
}
