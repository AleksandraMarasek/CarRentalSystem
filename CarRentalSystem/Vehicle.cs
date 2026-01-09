using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem;

abstract class Vehicle
{
    public string Brand {  get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsRented { get; set; }

    public abstract decimal RentalCost(int days);
}
