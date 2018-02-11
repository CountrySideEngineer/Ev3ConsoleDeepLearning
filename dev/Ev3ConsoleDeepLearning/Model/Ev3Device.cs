using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public abstract class Ev3Device
    {
        #region Public constants
        public enum INPORT
        {
            INPORT_1,
            INPORT_2,
            INPORT_3,
            INPORT_4,
            INPORT_MAX,
        };
        public enum OUTPORT
        {
            OUTPORT_A,
            OUTPORT_B,
            OUTPORT_C,
            OUTPORT_D,
            OUTPORT_MAX
        };
        #endregion

        #region Public read-only static fields
        /// <summary>
        /// Dictinary of INPORT name.
        /// </summary>
        public static readonly Dictionary<INPORT, string> InPortNameDictionary =
            new Dictionary<INPORT, string>()
        {
                { INPORT.INPORT_1, "PORT_1" },
                { INPORT.INPORT_2, "PORT_2" },
                { INPORT.INPORT_3, "PORT_3" },
                { INPORT.INPORT_4, "PORT_4" },
                { INPORT.INPORT_MAX, "" },
        };

        /// <summary>
        /// Dictionary of OUTPORT name.
        /// </summary>
        public static readonly Dictionary<OUTPORT, string> OutPortNameDictionary =
            new Dictionary<OUTPORT, string>()
        {
                { OUTPORT.OUTPORT_A, "PORT_A" },
                { OUTPORT.OUTPORT_B, "PORT_B" },
                { OUTPORT.OUTPORT_C, "PORT_C" },
                { OUTPORT.OUTPORT_D, "PORT_D" },
                { OUTPORT.OUTPORT_MAX, "" },
        };
        #endregion

        #region Constructors and the Finalizer
        public Ev3Device()
        {
            this.IsConnected = false;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Name of port.
        /// </summary>
        public string Port
        {
            get { return this.GetPortName(); }
        }

        /// <summary>
        /// Device name.
        /// </summary>
        public string Device
        {
            get { return this.GetDeviceName(); }
        }

        /// <summary>
        /// Whether a device is connected to port or not.
        /// </summary>
        public bool IsConnected { get; set; }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Abstract method which returns port name the device is connected.
        /// </summary>
        /// <returns></returns>
        public abstract string GetPortName();

        /// <summary>
        /// Abstract method which returns device name connected to port.
        /// </summary>
        /// <returns></returns>
        public abstract string GetDeviceName();
        #endregion
    }
}
