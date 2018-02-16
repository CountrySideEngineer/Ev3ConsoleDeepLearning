using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_04_00 : ACommand_ResLenFix
    {
        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize command and response code, and its 
        /// </summary>
        protected override void Init()
        {
            this.Name = "GetBattery";
            this.Cmd = 0x04;
            this.SubCmd = 0x00;
            this.CmdLen = 0x00;

            this.Res = 0x05;
            this.SubRes = 0x00;
            this.ResLen = 0x04;

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
        /// No parameter to check.
        /// </summary>
        protected override void CheckParam() { }
        #endregion
    }
}
