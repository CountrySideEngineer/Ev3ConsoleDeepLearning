using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class CommandParam_12_00 : ICommandParam
    {
        #region Constructors and the Finalizer
        public CommandParam_12_00(byte Power, byte Direction)
        {
            if (0x64 <= Power)
            {
                this.Power = 0x64;
            }
            else
            {
                this.Power = Power;
            }

            if (0x00 != Direction)
            {
                this.Direction = 0x01;
            }
            else
            {
                this.Direction = Direction;
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Target motor output power.
        /// </summary>
        public byte Power { get; protected set; }

        /// <summary>
        /// Motor rorate direction.
        /// </summary>
        public byte Direction { get; protected set; }
        #endregion
    }
}
