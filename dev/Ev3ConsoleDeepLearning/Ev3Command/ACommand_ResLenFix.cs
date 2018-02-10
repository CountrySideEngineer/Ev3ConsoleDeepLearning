using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public abstract class ACommand_ResLenFix : ACommand
    {
        #region Constructors and the Finalizer
        public ACommand_ResLenFix(ICommandParam CommandParam = null) : base(CommandParam) { }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Compare size of response data with data part in response data.
        /// If the result shows it invalids, CommandLenException will be thrown.
        /// </summary>
        /// <param name="OptDataIndex"></param>
        /// <returns></returns>
        protected override int CheckLen()
        {
            Debug.Assert(this.ResData != null);

            int ResLen = base.CheckLen();
            if (ResLen != this.ResLen)
            {
                throw new CommandLenException(
                        "Command or response data Len error",
                        this.Cmd, this.SubCmd, this.Name);
            }
            return ResLen;//WANT!!Change using TAPLE.
        }
        #endregion
    }
}
