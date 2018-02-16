using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_0C_00 : ACommand_ResLenFix
    {
        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize command and response code, and its 
        /// </summary>
        protected override void Init()
        {
            this.Name = "GetMotors";
            this.Cmd = 0x0C;
            this.SubCmd = 0x00;
            this.CmdLen = 0x00;

            this.Res = 0x0D;
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
        /// Check response data of GetMotor reponse data.
        /// </summary>
        protected override void CheckParam()
        {
            byte MotorData = 0x00;
            for (int index = 0; index < 4; index++)
            {
                int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP + index;
                MotorData = this.ResData[DataIndex];
                switch (MotorData)
                {
                    case 0x00:
                    case 0x01:
                    case 0x02:
                    case 0x03:
                        break;

                    default:
                        throw new CommandOperationException(
                            "ReceiveUnexpectedMotorType",
                            this.Cmd, this.SubCmd, this.Name);
                }
            }
        }
        #endregion
    }
}
