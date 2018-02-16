using Ev3Controller.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_12_00 : ACommand_ResLenFix
    {
        #region Constructors and the Finalizer
        public Command_12_00() : this(new CommandParam_12_00(0, 0)) { }
        public Command_12_00(ICommandParam CommandParam) : base(CommandParam)
        {
            if (CommandParam is CommandParam_12_00)
            {
                this.CommandParam = CommandParam as CommandParam_12_00;
            }
            else
            {
                this.CommandParam = new CommandParam_12_00(0, 0);
            }
        }
        #endregion

        #region Other methods and private properties in calling order
        protected override void Init()
        {
            this.Name = "GetMotorPower";
            this.Cmd = 0x12;
            this.SubCmd = 0x00;
            this.CmdLen = 0x02;

            this.Res = 0x13;
            this.SubRes = 0x00;
            this.ResLen = 0x00;

            base.Init();
        }

        /// <summary>
        /// Setup command data consisted of "byte" data array.
        /// </summary>
        protected override void SetUp(ICommandParam CommandParam)
        {
            int DataIndex = (int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN;
            this.CmdData[DataIndex++] = this.CmdLen;
            this.CmdData[DataIndex++] = (CommandParam as CommandParam_12_00).Power;
            this.CmdData[DataIndex] = (CommandParam as CommandParam_12_00).Direction;
        }

        /// <summary>
        /// Update command param, motor output.
        /// </summary>
        /// <param name="CommandParam"></param>
        public override void UpdateCmdData(ICommandParam CommandParam = null)
        {
            if (null == CommandParam)
            {
                //var Brick = Ev3Brick.GetInstance();
                //byte Output = Convert.ToByte(Math.Abs(Brick.Output.MotorOutput));
                //byte Direction = (byte)(Brick.Output.MotorOutput > 0 ? 1 : 0);
                //CommandParam = new CommandParam_12_00(Output, Direction);
            }
            this.SetUp(CommandParam);
        }

        protected override void CheckParam() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Object contains parameter to be set in command data array.
        /// </summary>
        public ICommandParam CommandParam { get; protected set; }
        #endregion
    }
}
