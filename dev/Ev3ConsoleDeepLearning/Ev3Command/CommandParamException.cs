using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class CommandParamException : CommandException
    {
        #region Constructors and the Finalizer
        public CommandParamException() { }
        public CommandParamException(string message)
            : base(message) { }
        public CommandParamException(string message, Exception inner)
            : base(message, inner) { }
        public CommandParamException(string message, byte Cmd, byte SubCmd, string Name)
            : base(message, Cmd, SubCmd, Name) { }
        public CommandParamException(
            string message, Exception inner, byte Cmd, byte SubCmd, string Name)
            : base(message, inner, Cmd, SubCmd, Name) { }
        #endregion
    }
}