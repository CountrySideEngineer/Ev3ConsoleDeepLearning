using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class CommandParam_0B_00 : ICommandParam
    {
        #region Constructors and the Finalizer
        public CommandParam_0B_00(int Year, byte Month, byte Day)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// This year.
        /// </summary>
        public int Year { get; protected set; }

        /// <summary>
        /// This month.
        /// </summary>
        public byte Month { get; protected set; }

        /// <summary>
        /// Date of today.
        /// </summary>
        public byte Day { get; protected set; }
        #endregion
    }
}
