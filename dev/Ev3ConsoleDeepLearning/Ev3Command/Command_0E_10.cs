using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_0E_10 : Command_0E
    {
        #region Other methods and private properties in calling order
        protected override void Init()
        {
            this.SubCmd = 0x10;
            this.CmdLen = 0x00;

            this.SubRes = 0x10;
            this.ResLen = 0xFF;//Length of response can not be determined.

            this.OneDataLen = 0x02;

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
        /// Check reponse data of GetSensors and get ultrasonic sensor listening data.
        /// </summary>
        protected override void CheckParam()
        {
            int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP;
            int DevNum = this.ResData[DataIndex++];

            if (DevNum != 0)
            {
                for (int index = 0; index < DevNum; index++)
                {
                    this.CheckPort(DataIndex++);

                    byte IsListen = this.ResData[DataIndex++];
                    if ((IsListen != 0) && (IsListen != 1))
                    {
                        throw new CommandOperationException(
                            "InvalidListenState",
                            this.Cmd, this.SubCmd, this.Name);
                    }
                }
            }
        }
        #endregion
    }
}
