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
    /// battery service interface
    /// </summary>
    public abstract class IBatteryInfoService
    {
        public abstract IBatteryInfoData GetBatteryInformation();
    }
}
