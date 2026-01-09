using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Vehicles;

class ManualCar : Vehicle
{
    public override decimal RentalCost(int days)
    {
        return PricePerDay * days;
    }
}
