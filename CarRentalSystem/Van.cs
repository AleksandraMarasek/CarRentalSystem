namespace CarRentalSystem;

 class Van : Vehicle
{
    public override decimal RentalCost(int days)
    {
        return PricePerDay * days * 1.5m;
    }
}
