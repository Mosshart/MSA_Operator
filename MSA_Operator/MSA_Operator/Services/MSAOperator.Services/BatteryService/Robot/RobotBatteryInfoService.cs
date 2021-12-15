using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAOperator.Services.BatteryService.Robot
{
  
    public class RobotBatteryInfoService : BatteryInfoServiceBase
    {
        public RobotBatteryInfoService() : base(new ServiceRobot()) { }
    }

    public class ServiceRobot : IBatteryInfoService
    {
        public override IBatteryInfoData GetBatteryInformation()
        {
            return new RobotBatteryInfo
            {
                DesignedMaxCapacity = 100,
                FullChargeCapacity = 100,
                CurrentCapacity = 25,
                Voltage = 42,
                DischargeRate = 75

            };
        }
    }

    public class RobotBatteryInfo : IBatteryInfoData
    {
        public int DesignedMaxCapacity { get; set; }
        public int FullChargeCapacity { get; set; }
        public uint CurrentCapacity { get; set; }
        public uint Voltage { get; set; }
        public int DischargeRate { get; set; }
    }
}
