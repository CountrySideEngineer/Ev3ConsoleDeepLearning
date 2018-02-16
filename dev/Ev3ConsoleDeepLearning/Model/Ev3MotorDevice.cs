using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class Ev3MotorDevice : Ev3Device
    {
        #region Public constants
        /// <summary>
        /// Enumrator of motor device type.
        /// </summary>
        public enum DEVICE_TYPE
        {
            MOTOR_DEVICE_NO_DEVICE,
            MOTOR_DEVICE_MEDIUM_MOTOR,
            MOTOR_DEVICE_LARGE_MOTOR,
            MOTOR_DEVICE_UNADJUSTED,
            MOTOR_DEVICE_UNKNOWN,
            MOTOR_DEVICE_MAX,
        }
        #endregion

        #region Public read-only static fields
        public static readonly Dictionary<DEVICE_TYPE, string>
            DeviceTypeDictionary = new Dictionary<DEVICE_TYPE, string>()
        {
                { DEVICE_TYPE.MOTOR_DEVICE_NO_DEVICE, "NO DEVICE" },
                { DEVICE_TYPE.MOTOR_DEVICE_MEDIUM_MOTOR, "MEDIUM MOTOR" },
                { DEVICE_TYPE.MOTOR_DEVICE_LARGE_MOTOR, "LARGE MOTOR" },
                { DEVICE_TYPE.MOTOR_DEVICE_UNADJUSTED, "UnAdjusted" },
                { DEVICE_TYPE.MOTOR_DEVICE_UNKNOWN, "Unknown" },
                { DEVICE_TYPE.MOTOR_DEVICE_MAX, "Unknown" },
        };
        #endregion

        #region Constructors and the Finalizer
        public Ev3MotorDevice()
        {
            this.ConnectedPort = OUTPORT.OUTPORT_MAX;
            this.DeviceType = DEVICE_TYPE.MOTOR_DEVICE_NO_DEVICE;
            this.Power = 0;
            this.Counts = 0;

            this.PowerUnit = "";
            this.CountsUnit = "";
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Motor output power.
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Unit of motor power.
        /// </summary>
        public string PowerUnit { get; set; }

        /// <summary>
        /// Angle position of motor.
        /// </summary>
        public long Counts { get; set; }

        /// <summary>
        /// Unit of counts.
        /// </summary>
        public string CountsUnit { get; set; }

        /// <summary>
        /// Port the motor device is connected.
        /// </summary>
        public OUTPORT ConnectedPort;

        /// <summary>
        /// Motor device type.
        /// </summary>
        public DEVICE_TYPE DeviceType;
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Abstract method which returns port name the device is connected.
        /// </summary>
        /// <returns></returns>
        public override string GetPortName()
        {
            return OutPortNameDictionary[this.ConnectedPort];
        }

        /// <summary>
        /// Abstract method which returns connected device name.
        /// </summary>
        /// <returns></returns>
        public override string GetDeviceName()
        {
            return DeviceTypeDictionary[this.DeviceType];
        }
        #endregion
    }
}
