using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ev3Controller.Ev3Command;
using System.Diagnostics;

namespace Ev3Controller.Model
{
    public class BrickUpdater_10_01 : BrickUpdater
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

            if (Command is Command_10_01)
            {
                int Index = 0;
                int DataTopIndex = 4;
                for (Index = 0; Index < 4; Index++)
                {
                    int DataIndex = DataTopIndex + Index * 2;
                    byte Connection = Command.ResData[DataIndex++];
                    int Power = (int)((sbyte)(Command.ResData[DataIndex]));

                    var Device = Brick.MotorDevice(Index);
                    Device.ConnectedPort = (Ev3Device.OUTPORT)Index;
                    if (0x00 == Connection)
                    {
                        Device.IsConnected = false;
                        Device.Power = 0;
                    }
                    else
                    {
                        Device.IsConnected = true;
                        Device.Power = Power;
                    }
                }
            }
        }
    }
}
