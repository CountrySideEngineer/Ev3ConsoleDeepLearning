using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class Ev3SensorDevice : Ev3Device
    {
        #region Public constants
        /// <summary>
        /// Enumrator of sensor device type.
        /// </summary>
        public enum DEVICE_TYPE
        {
            SENSOR_DEVICE_NO_DEVICE,
            SENSOR_DEVICE_ULTRASONIC,
            SENSOR_DEVICE_GYRO,
            SENSOR_DEVICE_TOUCH,
            SENSOR_DEVICE_COLOR,
            SENSOR_DEVICE_HT_NXT_ACCEL,
            SENSOR_DEVICE_NXT_TEMP,
            SENSOR_DEVICE_UNKNOWN,
            SENSOR_DEVICE_MAX,
        }
        #endregion

        #region Public read-only static fields
        public static readonly Dictionary<DEVICE_TYPE, string>
            DeviceTypeDictionary = new Dictionary<DEVICE_TYPE, string>()
        {
                { DEVICE_TYPE.SENSOR_DEVICE_NO_DEVICE, "NO DEVICE" },
                { DEVICE_TYPE.SENSOR_DEVICE_ULTRASONIC, "ULTRASONIC" },
                { DEVICE_TYPE.SENSOR_DEVICE_GYRO, "GYRO" },
                { DEVICE_TYPE.SENSOR_DEVICE_TOUCH, "TOUCH" },
                { DEVICE_TYPE.SENSOR_DEVICE_COLOR, "COLOR" },
                { DEVICE_TYPE.SENSOR_DEVICE_HT_NXT_ACCEL, "HTX" },
                { DEVICE_TYPE.SENSOR_DEVICE_NXT_TEMP, "TEMPERATURE" },
                { DEVICE_TYPE.SENSOR_DEVICE_UNKNOWN, "UNKNOWN" },
                { DEVICE_TYPE.SENSOR_DEVICE_MAX, "UNKNOWN" },
        };
        #endregion

        #region Constructors and the Finalizer
        public Ev3SensorDevice()
        {
            this.ConnectedPort = INPORT.INPORT_MAX;
            this.DeviceType = DEVICE_TYPE.SENSOR_DEVICE_NO_DEVICE;

            this.Value1 = 0;
            this.Value2 = 0;
            this.Value3 = 0;

            this.Unit1 = "";
            this.Unit2 = "";
            this.Unit3 = "";
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Sensor value1.
        /// </summary>
        public int Value1 { get; set; }

        /// <summary>
        /// Sensor value2.
        /// </summary>
        public int Value2 { get; set; }

        /// <summary>
        /// Sensor value3.
        /// </summary>
        public int Value3 { get; set; }

        /// <summary>
        /// Unit of Value1
        /// </summary>
        public string Unit1 { get; set; }

        /// <summary>
        /// Unit of Value2
        /// </summary>
        public string Unit2 { get; set; }

        /// <summary>
        /// Unit of Value3
        /// </summary>
        public string Unit3 { get; set; }

        /// <summary>
        /// Port the sensor device is connected.
        /// </summary>
        public INPORT ConnectedPort { get; set; }

        /// <summary>
        /// Connected device type.
        /// </summary>
        public DEVICE_TYPE DeviceType;
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Returns port name the device is connected.
        /// </summary>
        /// <returns></returns>
        public override string GetPortName()
        {
            return InPortNameDictionary[this.ConnectedPort];
        }

        /// <summary>
        /// Returns connected device name connected.
        /// </summary>
        /// <returns></returns>
        public override string GetDeviceName()
        {
            return DeviceTypeDictionary[this.DeviceType];
        }
        #endregion
    }
}
