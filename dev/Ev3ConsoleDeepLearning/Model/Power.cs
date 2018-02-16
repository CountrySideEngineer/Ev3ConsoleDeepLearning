using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class Power
    {
        #region Constructors and the Finalizer
        public Power()
        {
            this.Voltage = 0;
            this.Current = 0;
        }
        #endregion
        #region Public Properties
        /// <summary>
        /// Voltage of power supply.
        /// </summary>
        public int Voltage { get; set; }

        /// <summary>
        /// Current of power supply
        /// </summary>
        public int Current { get; set; }
        #endregion
    }
}
