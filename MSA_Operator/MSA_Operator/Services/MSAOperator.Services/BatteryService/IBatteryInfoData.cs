using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MSAOperator.Services.BatteryService
{
    /// <summary>
    /// battery info interface
    /// </summary>
    public interface IBatteryInfoData
    {
        int FullChargeCapacity { get; set; }
        uint CurrentCapacity { get; set; }
    }
}
