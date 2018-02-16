using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public class CommandInvalidParamException : CommandException
    {
        #region Constructors and the Finalizer
        public CommandInvalidParamException() { }
        public CommandInvalidParamException(string message)
            : base(message) { }
        public CommandInvalidParamException(string message, Exception inner)
            : base(message, inner) { }
        public CommandInvalidParamException(string message, byte Cmd, byte SubCmd, string Name)
            : base(message, Cmd, SubCmd, Name) { }
        public CommandInvalidParamException(
            string message, Exception inner, byte Cmd, byte SubCmd, string Name)
            : base(message, inner, Cmd, SubCmd, Name) { } 
        #endregion
    }
}
