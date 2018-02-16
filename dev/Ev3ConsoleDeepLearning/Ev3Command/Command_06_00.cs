using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_06_00 : ACommand_ResLenFix
    {
        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize command and response code, and its 
        /// </summary>
        protected override void Init()
        {
            this.Name = "GetSafeState";
            this.Cmd = 0x06;
            this.SubCmd = 0x00;
            this.CmdLen = 0x00;

            this.Res = 0x07;
            this.SubRes = 0x00;
            this.ResLen = 0x01;

            base.Init();
        }

        /// <summary>
        /// Setup command data for GetBattery original response data.
        /// </summary>
        protected override void SetUp(ICommandParam CommandParam)
        {
            this.CmdData[(int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN] = this.CmdLen;
        }

        /// <summary>
        /// Check reponse data of AppVersion original response data.
        /// </summary>
        protected override void CheckParam()
        {
            byte SafeState = this.ResData[(int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP];
            switch (SafeState)
            {
                case 0x00:
                case 0x01:
                case 0x02:
                case 0x03:
                    break;
                default:
                    throw new CommandOperationException(
                        "ReceiveUnexpectedSafeState",
                        this.Cmd, this.SubCmd, this.Name);
            }
        }
        #endregion
    }
}
