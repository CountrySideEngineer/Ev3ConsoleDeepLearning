using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    /// <summary>
    /// Provides interdace to access port.
    /// This class refers to page below.
    /// https://code.msdn.microsoft.com/windowsapps/COM-howto-6c7ff269
    /// </summary>
    public class ComPortAccess
    {
        #region Public constants
        /// <summary>
        /// Buffer si
        /// </summary>
        public const int DATA_BUF_SIZE = 32;
        #endregion

        #region Constructors and the Finalizer
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ComPort"></param>
        public ComPortAccess(ComPort ComPort)
        {
            this.ComPort = ComPort;
        }
        public ComPortAccess() { this.ComPort = null; }
        #endregion

        #region Public Properties
        /// <summary>
        /// Represents a serial port resource.
        /// </summary>
        public SerialPort Port { get; protected set; }

        /// <summary>
        /// Represents a access port information.
        /// </summary>
        public ComPort ComPort { get; protected set; }
        #endregion

        #region Other methods and private properties in calling order

        /// <summary>
        /// Initialize send and receive buffer of serial port.
        /// </summary>
        public void Init()
        {
            if ((this.Port != null) && (this.Port.IsOpen))
            {
                this.Port.DiscardInBuffer();
                this.Port.DiscardOutBuffer();
            }
            else
            {
                Console.WriteLine("Serial Port is not open.");
            }
        }

        /// <summary>
        /// Open serial port specified by argument ComPort.
        /// The argument "ComPort is set to Property ComPort.
        /// </summary>
        /// <param name="ComPort">Serial port information to open.</param>
        /// <returns>Returns true if the port can open, else returns false.</returns>
        public bool Connect(ComPort ComPort)
        {
            this.ComPort = ComPort;

            return this.Connect();
        }
        /// <summary>
        /// Open serial port specified by property ComPort.
        /// </summary>
        /// <returns>Returns true if the port can open, else returns false.</returns>
        public bool Connect()
        {
            try
            {
                /*
                 * Prevent serial port from being opened duplicately and create new SerialPort
                 * instance.
                 */
                if ((this.Port == null) || (!this.Port.IsOpen))
                {
                    this.Port = new SerialPort(this.ComPort.Name,
                        this.ComPort.BaudrateValue,
                        this.ComPort.Parity,
                        this.ComPort.DataBit,
                        this.ComPort.StopBits);
                    this.Port.Open();
                    this.Port.ReadTimeout = 2000;//2.0 sec time out.

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Catch UnauthorizedAccessException in " + this.ToString());
                Console.WriteLine("[ERROR]{0}", ex.Message);

                throw;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Catch ArgumentOutOfRangeException in " + this.ToString());
                Console.WriteLine("[ERROR]{0}", ex.Message);

                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Catch ArgumentException in " + this.ToString());
                Console.WriteLine("[ERROR]{0}", ex.Message);

                throw;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Catch IOException in " + this.ToString());
                Console.WriteLine("[ERROR]{0}", ex.Message);

                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Catch InvalidOperationException in " + this.ToString());
                Console.WriteLine("[ERROR]{0}", ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Close serial prot specified by ComPort and Port property.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if ((this.Port != null) && (this.Port.IsOpen))
                {
                    this.Port.Close();
                    this.Port = null;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Catch InvalidOperationException");
                Console.WriteLine("[ERROR]{0}", ex.StackTrace);

                throw;
            }
        }

        /// <summary>
        /// Send data and store received into buffer RecvBuff.
        /// </summary>
        /// <param name="SendBuff">Buffer to data to send.</param>
        /// <param name="RecvBuff">Buffer to buffer to store received data.</param>
        public virtual void SendAndRecv(byte[] SendBuff, out byte[] RecvBuff)
        {
            this.Port.DiscardInBuffer();
            this.Port.DiscardOutBuffer();

            this.SendData(SendBuff);
            this.RecvData(out RecvBuff);
        }

        /// <summary>
        /// Send data through serial port.
        /// </summary>
        /// <param name="Data">Byte data to send.</param>
        /// <returns>Send data size.</returns>
        public virtual int SendData(byte[] Data)
        {
            int data_size = 0;
            if ((this.ComPort != null) && (this.Port.IsOpen))
            {
                this.Port.Write(Data, 0, Data.Length);
                data_size = Data.Length;
                //To fullfil command data buffer on EV3 side, send data till the buffer is filled up.
                for (int index = Data.Length; index < (int)DATA_BUF_SIZE; index++)
                {
                    char[] Padding = { (char)0x00 };
                    this.Port.Write(Padding, 0, 1);
                    data_size++;
                }
            }
            return data_size;
        }

        /// <summary>
        /// Read data from serial port.
        /// </summary>
        /// <param name="Data">Reference to buffer to store read data.</param>
        /// <returns>Size of read data.</returns>
        public virtual int RecvData(out byte[] Data)
        {
            int LengthRead = 0;
            int WaitCount = 0;
            Data = null;
            if ((this.Port != null) && (this.Port.IsOpen))
            {
                do
                {
                    int LengthToRead = this.Port.BytesToRead;
                    if (LengthToRead > 0)
                    {
                        Data = new byte[LengthToRead];

                        while (LengthRead < LengthToRead)
                        {
                            int ReadLen = this.Port.Read(Data, LengthRead, LengthToRead - LengthRead);
                            LengthRead += ReadLen;
                        }
                        if (LengthRead > 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        /*
                         *  Command receiving task run each 40 milli seconds.
                         *  So, wait twice the time, it will be enough to handle all command data and
                         *  create and send response data.
                         */
                        if (WaitCount > 150)
                        {
                            break;
                        }
                        Thread.Sleep(1);//Stop the task while one million second.
                        WaitCount++;
                    }
                } while (this.Port.IsOpen);
            }

            return LengthRead;
        }
        #endregion
    }
}
