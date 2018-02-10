using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_10_00 : ACommand_ResLenFix
    {
        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize parameter for command to send and response to be received, and its name.
        /// </summary>
        protected override void Init()
        {
            this.Name = "GetMotorPower";
            this.Cmd = 0x10;
            this.SubCmd = 0x00;
            this.CmdLen = 0x00;

            this.Res = 0x11;
            this.SubRes = 0x00;
            this.ResLen = 0x01;

            base.Init();
        }

        /// <summary>
        /// Setup command data , just only length byte.
        /// </summary>
        protected override void SetUp(ICommandParam CommandParam)
        {
            this.CmdData[(int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN] = this.CmdLen;
        }

        /// <summary>
        /// Check response data:Nothing to do.
        /// </summary>
        protected override void CheckParam() { }
        #endregion
    }
}
