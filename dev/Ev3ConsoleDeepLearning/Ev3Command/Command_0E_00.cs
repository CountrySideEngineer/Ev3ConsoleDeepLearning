using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_0E_00 : Command_0E
    {
        #region Other methods and private properties in calling order
        protected override void Init()
        {
            this.SubCmd = 0x00;
            this.CmdLen = 0x00;

            this.SubRes = 0x00;
            this.ResLen = 0x04;

            base.Init();
        }

        /// <summary>
        /// Setup command data for GetMotors original data.
        /// </summary>
        protected override void SetUp(ICommandParam CommandParam)
        {
            this.CmdData[(int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN] = this.CmdLen;
        }

        /// <summary>
        /// Check whether size of response data buffer and length set in reponse data, 
        /// calcurated data lenght from the number of device in the data, matche.
        /// (To be more precise, the size is 4 byte more than the length.)
        /// If they are not match, CommandLenException will be thrown.
        /// </summary>
        /// <returns>Length written in response data.</returns>
        protected override int CheckLen()
        {
            Debug.Assert(this.ResData != null);

            int Len = this.ResData.Length;
            int ResLen = this.ResData[(int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_LEN];
            if (Len != (ResLen + 4))
            {
                throw new CommandLenException(
                        "Command or response data Len error",
                        this.Cmd, this.SubCmd, this.Name);
            }

            return ResLen;//WANT!!Change using TAPLE.
        }

        /// <summary>
        /// Check response data of GetSensors, sub code 0x00, type of sensor connected to each port.
        /// </summary>
        protected override void CheckParam()
        {
            byte SensorType = 0x00;
            for (int index = 0; index < 4; index++)
            {
                int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP + index;
                SensorType = this.ResData[DataIndex];
                switch (SensorType)
                {
                    case 0x00:
                    case 0x01:
                    case 0x02:
                    case 0x03:
                    case 0x04:
                    case 0x05:
                    case 0x06:
                        break;

                    default:
                        throw new CommandOperationException(
                            "ReceiveUnexpectedSensorType",
                            this.Cmd, this.SubCmd, this.Name);
                }
            }
        }
        #endregion
    }
}
