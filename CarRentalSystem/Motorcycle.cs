using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem;

class Motorcycle : Vehicle
{
    public override decimal RentalCost(int days)
    {
        return PricePerDay * days * 0.8m;
    }
}
