using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAOperator.Services.BatteryService
{
    public abstract class IBatteryInfoService
    {
        public abstract IBatteryInfoData GetBatteryInformation();
    }
}
