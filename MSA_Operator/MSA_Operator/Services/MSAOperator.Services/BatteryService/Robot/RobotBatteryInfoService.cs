using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MSAOperator.Services.BatteryService.Robot
{
    /// <summary>
    /// 
    /// </summary>
    public class RobotBatteryInfoService : BatteryInfoServiceBase
    {
        public RobotBatteryInfoService() : base(new ServiceRobot()) { }
    }

    /// <summary>
    /// Test class created to mockup data
    /// used as example 
    /// </summary>
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
    /// <summary>
    /// robot battery info data class
    /// </summary>
    public class RobotBatteryInfo : IBatteryInfoData
    {
        /// <summary>
        /// capacity of robot battery
        /// </summary>
        public int DesignedMaxCapacity { get; set; }
        /// <summary>
        /// fully charged battery capacity
        /// </summary>
        public int FullChargeCapacity { get; set; }
        /// <summary>
        /// current battery capacity 
        /// </summary>
        public uint CurrentCapacity { get; set; }
        /// <summary>
        /// battery voltage
        /// </summary>
        public uint Voltage { get; set; }
        /// <summary>
        /// battery discharge rate (if possible)
        /// </summary>
        public int DischargeRate { get; set; }
    }
}
