using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAOperator.Services.BatteryService
{
    public interface IBatteryInfoData
    {
        int FullChargeCapacity { get; set; }
        uint CurrentCapacity { get; set; }
    }
}
