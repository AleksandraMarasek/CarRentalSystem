namespace CarRentalSystem;

public class RentalRecord
{
    public string VehicleModel { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int Days { get; set; }
    public decimal FinalCost { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}
