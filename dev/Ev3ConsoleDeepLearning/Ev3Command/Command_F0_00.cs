using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class Command_F0_00 : ACommand
    {
        #region Constructors and the Finalizer
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CommandParam"></param>
        public Command_F0_00(ICommandParam CommandParam = null) : base(CommandParam) { }
        #endregion

        #region Public read-only static fields
        public static Dictionary<byte, byte> ExpectedDataLenDictionary = new Dictionary<byte, byte>()
        {
            { 0x00, 0x00 },
            { 0x20, 0x03 },
            { 0x30, 0x03 },
            { 0x40, 0x01 },
            { 0x50, 0x04 }
        };
        #endregion

        #region Other methods and private properties in calling order
        protected override void Init()
        {
            this.Name = "GetSensorsParameter";
            this.Cmd = 0xF0;
            this.SubCmd = 0x00;
            this.CmdLen = 0x00;

            this.Res = 0xF1;
            this.SubRes = 0x00;
            this.ResLen = 0xFF;

            base.Init();
        }

        /// <summary>
        /// Setup command data for AppVersion command.
        /// </summary>
        protected override void SetUp(ICommandParam CommandParam)
        {
            this.CmdData[(int)COMMAND_BUFF_INDEX.COMMAND_BUFF_INDEX_CMD_DATA_LEN] = this.CmdLen;
        }

        /// <summary>
        /// Check parameters, expecially port number in response data.
        /// </summary>
        protected override void CheckParam()
        {
            byte DeviceDataLen = 0x00;
            byte ExpectedDeviceDataLen = 0x00;
            int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP;
            for (int PortIndex = 0; PortIndex < 4; PortIndex++)
            {
                byte Device = this.ResData[DataIndex];
                DataIndex++;
                DeviceDataLen = this.ResData[DataIndex];
                ExpectedDeviceDataLen = ExpectedDataLenDictionary[Device];
                if (DeviceDataLen != ExpectedDeviceDataLen)
                {
                    throw new CommandLenException(
                            "Command or response data Len error",
                            this.Cmd, this.SubCmd, this.Name);
                }
                DataIndex += (DeviceDataLen + 0x01);
            }
        }
        #endregion
    }
}
