using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class TargetOutput
    {
        #region Constructors and the Finalizer
        public TargetOutput()
        {
            this.MotorOutput = 0;
            this.Steering = 0;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Target motor output.
        /// </summary>
        public int MotorOutput { get; set; }

        /// <summary>
        /// Steering.
        /// </summary>
        public int Steering { get; set; }
        #endregion
    }
}
