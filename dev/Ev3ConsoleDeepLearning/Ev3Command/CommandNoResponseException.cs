using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class CommandNoResponseException : CommandException
    {
        #region Constructors and the Finalizer
        public CommandNoResponseException() { }
        public CommandNoResponseException(string message)
            : base(message) { }
        public CommandNoResponseException(string message, Exception inner)
            : base(message, inner) { }
        public CommandNoResponseException(string message, byte Cmd, byte SubCmd, string Name)
            : base(message, Cmd, SubCmd, Name) { }
        public CommandNoResponseException(
            string message, Exception inner, byte Cmd, byte SubCmd, string Name)
            : base(message, inner, Cmd, SubCmd, Name) { }
        #endregion
    }
}
