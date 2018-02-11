using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ev3Controller.Ev3Command;
using System.Diagnostics;

namespace Ev3Controller.Model
{
    public class BrickUpdater_10_00 : BrickUpdater
    {
        /// <summary>
        /// Update motor device information of Ev3 Brick by GetMotorPower command,
        /// especially motor output power.
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="Brick"></param>
        public override void Update(ACommand Command, Ev3Brick Brick)
        {
            Debug.Assert(Command != null);
            Debug.Assert(Brick != null);
            //Nothing to check in this response data.
        }
    }
}
