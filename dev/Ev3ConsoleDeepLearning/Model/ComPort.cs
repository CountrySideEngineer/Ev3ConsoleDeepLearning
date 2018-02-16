using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public enum Baudrate
    {
        /// <summary>
        /// Baudrate:110bps
        /// </summary>
        Baudrate110,
        /// <summary>
        /// Baudrate:300bps
        /// </summary>
        Baudrate300,
        /// <summary>
        /// Baudrate:600bps
        /// </summary>
        Baudrate600,
        /// <summary>
        /// Baudrate:1200bps
        /// </summary>
        Baudrate1200,
        /// <summary>
        /// Baudrate:2400bps
        /// </summary>
        Baudrate2400,
        /// <summary>
        /// Baudrate:4800bps
        /// </summary>
        Baudrate4800,
        /// <summary>
        /// Baudrate:9600bps
        /// </summary>
        Baudrate9600,
        /// <summary>
        /// Baudrate:14400bps(14.4kbps)
        /// </summary>
        Baudrate14400,
        /// <summary>
        /// Baudrate:19200bps(19.2kpbs)
        /// </summary>
        Baudrate19200,
        /// <summary>
        /// Baudrate:38400bps(38.4kbps)
        /// </summary>
        Baudrate38400,
        /// <summary>
        /// Baudrate:57600bps(57.6kbps)
        /// </summary>
        Baudrate57600,
        /// <summary>
        /// Baudrate:115200bps(115.2kbps)
        /// </summary>
        Baudrate115200,
        /// <summary>
        /// Baudrate:230400bps(230.4kbps)
        /// </summary>
        Baudrate230400,
        /// <summary>
        /// Baudrate:460600bps(460.6kbps)
        /// </summary>
        Baudrate460600,
        /// <summary>
        /// Baudrate:921600bps(921.6kbps)
        /// </summary>
        Baudrate921600
    }

    /// <summary>
    /// Represent COM port resources.
    /// </summary>
    public class ComPort
    {
        #region Private fields and constants (in a region)
        protected static Dictionary<Baudrate, int> PortBaudrateMap = new Dictionary<Baudrate, int>
        {
            { Baudrate.Baudrate110,        110 },
            { Baudrate.Baudrate300,        300 },
            { Baudrate.Baudrate600,        600 },
            { Baudrate.Baudrate1200,      1200 },
            { Baudrate.Baudrate2400,      2400 },
            { Baudrate.Baudrate4800,      4800 },
            { Baudrate.Baudrate9600,      9600 },
            { Baudrate.Baudrate14400,    14400 },
            { Baudrate.Baudrate19200,    19200 },
            { Baudrate.Baudrate38400,    38400 },
            { Baudrate.Baudrate57600,    57600 },
            { Baudrate.Baudrate115200,  115200 },
            { Baudrate.Baudrate230400,  230400 },
            { Baudrate.Baudrate460600,  460600 },
            { Baudrate.Baudrate921600,  921600 }
        };
        #endregion

        #region Constructors and the Finalizer
        public ComPort(string Name, string Device)
        {
            this.Name = Name;
            this.Device = Device;

            //Defualt configuration.
            this.Baudrate = Baudrate.Baudrate9600;
            this.Parity = Parity.None;
            this.StopBits = StopBits.One;
            this.DataBit = 8;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Port name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Device name.
        /// </summary>
        public string Device { get; protected set; }

        /// <summary>
        /// Port baudrate.
        /// </summary>
        public int BaudrateValue { get; protected set; }

        /// <summary>
        /// Enumrator of port baudrate.
        /// </summary>
        protected Baudrate _Baudrate;
        public Baudrate Baudrate
        {
            get { return this._Baudrate; }
            set
            {
                this._Baudrate = value;
                this.BaudrateValue = ComPort.PortBaudrateMap[this._Baudrate];
            }
        }

        /// <summary>
        /// Parity of port.
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// Data bit configuration.
        /// </summary>
        public int DataBit { get; set; }

        /// <summary>
        /// Stop bit configuration.
        /// </summary>
        public StopBits StopBits { get; set; }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Compare the ComPort object specified by argument matches with self.
        /// </summary>
        /// <param name="ComPort">ComPort object to compare.</param>
        /// <returns>If matches, returns true, otherwise returns false.</returns>
        public bool Equals(ComPort ComPort)
        {
            try
            {
                if (this.Name.Equals(ComPort.Name) && this.Device.Equals(ComPort.Device))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }
}
