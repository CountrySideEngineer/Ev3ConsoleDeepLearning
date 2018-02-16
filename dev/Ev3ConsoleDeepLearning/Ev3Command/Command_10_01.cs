using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_10_01 : ACommand_ResLenFix
    {
        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize parameter for command to send and response to be received, and its name.
        /// </summary>
        protected override void Init()
        {
            this.Name = "GetMotorPower";
            this.Cmd = 0x10;
            this.SubCmd = 0x01;
            this.CmdLen = 0x00;

            this.Res = 0x11;
            this.SubRes = 0x01;
            this.ResLen = 0x08;

            base.Init();
        }

        /// <summary>
        /// Setup command data , just only length byte.
        /// </summary>
        protected override void SetUp(ICommandParam CommandParam)
        {
            this.CmdData[(int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN] = this.CmdLen;
        }

        protected override void CheckParam()
        {
            int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP;
            int DevNum = 4;
            for (int DevNumIndex = 0; DevNumIndex < DevNum; DevNumIndex++)
            {
                byte IsConnect = this.ResData[DataIndex++];
                if ((IsConnect != 0x00) && (IsConnect != 0x01))
                {
                    throw new CommandOperationException(
                        "InvalidConnectState",
                        this.Cmd, this.SubCmd, this.Name);
                }

                int MotorOutput = Convert.ToInt32((sbyte)this.ResData[DataIndex++]);
                if ((MotorOutput < -100) || (100 < MotorOutput))
                {
                    throw new CommandOperationException(
                        "InvalidMotorPower",
                        this.Cmd, this.SubCmd, this.Name);
                }
            }
        }
        #endregion
    }
}
