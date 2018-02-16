using Ev3Controller.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_16_00 : ACommand_ResLenFix
    {
        #region Constructors and the Finalizer
        public Command_16_00() : this(new CommandParam_16_00(0)) { }
        public Command_16_00(ICommandParam CommandParam) : base(CommandParam)
        {
            try
            {
                if (CommandParam is CommandParam_16_00)
                {
                    this.CommandParam = CommandParam as CommandParam_16_00;
                }
                else
                {
                    this.CommandParam = new CommandParam_16_00(0);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                this.CommandParam = new CommandParam_16_00(0);
            }
        }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize command parameters.
        /// </summary>
        protected override void Init()
        {
            this.Name = "SetSteer";
            this.Cmd = 0x16;
            this.SubCmd = 0x00;
            this.CmdLen = 0x01;

            this.Res = 0x17;
            this.SubRes = 0x00;
            this.ResLen = 0x00;

            base.Init();
        }

        /// <summary>
        /// Setup command data consisted of "byte" data array.
        /// </summary>
        protected override void SetUp(ICommandParam CommandParam)
        {
            try
            {
                int DataIndex = (int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN;
                this.CmdData[DataIndex++] = this.CmdLen;
                this.CmdData[DataIndex] = (byte)((CommandParam as CommandParam_16_00).Steer);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);

                int DataIndex = (int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN;
                this.CmdData[DataIndex++] = this.CmdLen;
                this.CmdData[DataIndex] = 0;
            }
        }

        /// <summary>
        /// Update command param, steering output value.
        /// </summary>
        /// <param name="CommandParam">CommandParam object contains Steering value to set.</param>
        public override void UpdateCmdData(ICommandParam CommandParam = null)
        {
            if (null == CommandParam)
            {
                //var Brick = Ev3Brick.GetInstance();
                //int Steer = Brick.Output.Steering;
                //CommandParam = new CommandParam_16_00(Steer);
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
