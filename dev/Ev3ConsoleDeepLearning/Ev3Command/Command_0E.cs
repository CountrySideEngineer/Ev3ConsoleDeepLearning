using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public abstract class Command_0E : ACommand
    {
        #region Public Properties
        public byte OneDataLen { get; protected set; }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize command and response code, and its command name.
        /// </summary>
        protected override void Init()
        {
            this.Name = "GetSensors";

            this.Cmd = 0x0E;
            this.Res = 0x0F;

            base.Init();
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
            int ResLen = base.CheckLen();
            int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP;
            int DevNum = this.ResData[DataIndex];

            if (ResLen != (DevNum * this.OneDataLen) + 1)
            {
                throw new CommandLenException(
                        "Command or response data Len error",
                        this.Cmd, this.SubCmd, this.Name);
            }
            return ResLen;
        }
        #endregion
    }
}
