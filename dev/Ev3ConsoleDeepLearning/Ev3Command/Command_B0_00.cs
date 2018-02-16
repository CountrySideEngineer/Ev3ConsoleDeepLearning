using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_B0_00 : ACommand_ResLenFix
    {
        #region Private fields and constants (in a region)
        /// <summary>
        /// Year in A.D.
        /// </summary>
        protected int Year;

        /// <summary>
        /// Month of this year.
        /// </summary>
        protected byte Month;

        /// <summary>
        /// Date of today.
        /// </summary>
        protected byte Day;
        #endregion

        #region Constructors and the Finalizer
        #endregion

        #region Other methods and private properties in calling order
        protected override void Init()
        {
            this.Name = "SetDate1";
            this.Cmd = 0xB0;
            this.SubCmd = 0x00;
            this.CmdLen = 0x04;

            this.Res = 0xB1;
            this.SubRes = 0x00;
            this.ResLen = 0x04;

            base.Init();
        }

        /// <summary>
        /// Check received response data, received year, month and date equal to
        /// the date sent by command data.
        /// </summary>
        protected override void CheckParam()
        {
            int RecvYear = 0;
            byte RecvMonth = 0;
            byte RecvDay = 0;

            RecvYear = (int)((UInt16)this.ResData[4] + (UInt16)((this.ResData[5] << 8) & 0xFF00));
            RecvMonth = this.ResData[6];
            RecvDay = this.ResData[7];

            if (!((RecvYear == this.Year) && (RecvMonth == this.Month) && (RecvDay == this.Day)))
            {
                throw new CommandInvalidParamException(
                        "Receive invalid date data",
                        this.Cmd, this.SubCmd, this.Name);
            }
        }

        /// <summary>
        /// Setup command data for SetData1 command.
        /// Set date data of today, year, month and date of today.
        /// </summary>
        /// <param name="CommandParam"></param>
        protected override void SetUp(ICommandParam CommandParam)
        {
            this.Year = DateTime.Today.Year;
            this.Month = (byte)DateTime.Today.Month;
            this.Day = (byte)DateTime.Today.Day;

            int DataIndex = (int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN;
            this.CmdData[DataIndex++] = this.CmdLen;
            this.CmdData[DataIndex++] = (byte)(this.Year & 0x00FF);
            this.CmdData[DataIndex++] = (byte)((this.Year & 0xFF00) >> 8);
            this.CmdData[DataIndex++] = (byte)(this.Month);
            this.CmdData[DataIndex] = (byte)(this.Day);
        }
        #endregion
    }
}
