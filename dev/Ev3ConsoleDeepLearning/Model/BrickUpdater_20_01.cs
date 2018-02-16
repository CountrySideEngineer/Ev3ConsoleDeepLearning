using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ev3Controller.Ev3Command;
using System.Diagnostics;

namespace Ev3Controller.Model
{
    public class BrickUpdater_20_01 : BrickUpdater
    {
        /// <summary>
        /// Update sensor data, especially ultra sonic senosr listening data, device information
        /// of Ev3 Brick sent by GetSonicSensor command.
        /// </summary>
        /// <param name="Command">GetSonicSensor command data.</param>
        /// <param name="Brick">Ev3Brick object to set received data.</param>
        public override void Update(ACommand Command, Ev3Brick Brick)
        {
            Debug.Assert(Command != null);
            Debug.Assert(Brick != null);

            if (Command is Command_20_01)
            {
                int Index = 0;
                int DataTopIndex = 4;
                int DevNum = Command.ResData[DataTopIndex++];
                for (Index = 0; Index < DevNum; Index++)
                {
                    int DataIndex = DataTopIndex + (Index * 2);
                    byte Port = Command.ResData[DataIndex++];
                    byte IsListen = Command.ResData[DataIndex];
                    var Device = Brick.SensorDevice(Port);
                    Device.ConnectedPort = (Ev3Device.INPORT)Port;
                    Device.IsConnected = true;
                    Device.Value2 = IsListen;
                    Device.DeviceType = Ev3SensorDevice.DEVICE_TYPE.SENSOR_DEVICE_ULTRASONIC;
                }
            }
        }
    }
}
