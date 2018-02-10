using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public abstract class Command_50 : ACommand_ResLenFlex
    {
        #region Constructors and the Finalizer
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CommandParam"></param>
        public Command_50(ICommandParam CommandParam) : base(CommandParam) { }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize command and response code, and its command name.
        /// </summary>
        protected override void Init()
        {
            this.Name = "GetGyroSensor";
            this.Cmd = 0x50;
            this.Res = 0x51;

            base.Init();
        }
        #endregion
    }
}
