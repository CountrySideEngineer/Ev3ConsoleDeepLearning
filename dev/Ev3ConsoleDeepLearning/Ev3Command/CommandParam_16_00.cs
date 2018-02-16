using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class CommandParam_16_00 : ICommandParam
    {
        #region Constructors and the Finalizer
        public CommandParam_16_00(int Steer)
        {
            this.Steer = Steer;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Target steering ratio.
        /// </summary>
        public int Steer { get; protected set; }
        #endregion
    }
}
