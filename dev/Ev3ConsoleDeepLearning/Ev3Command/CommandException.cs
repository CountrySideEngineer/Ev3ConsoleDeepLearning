using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    /// <summary>
    /// Base class for exception that will be raised when the command response data contains
    /// invalid data.
    /// </summary>
    public class CommandException : Exception
    {
        #region Constructors and the Finalizer
        /// <summary>
        /// Constructor.
        /// </summary>
        public CommandException() { this.Init(); }
        public CommandException(string message) : base(message) { this.Init(); }
        public CommandException(string message, Exception inner) : base(message, inner)
        {
            this.Init();
        }
        public CommandException(string message, byte Cmd, byte SubCmd, string Name)
            : base(message)
        {
            this.Init(Cmd, SubCmd, Name);
        }
        public CommandException(
            string message, Exception inner, byte Cmd, byte SubCmd, string Name)
            : base(message, inner)
        {
            this.Init(Cmd, SubCmd, Name);
        }

        /// <summary>
        /// Initialize exception information.
        /// </summary>
        /// <param name="Cmd">Command code</param>
        /// <param name="SubCmd">Sub command code</param>
        /// <param name="Name">Name of command</param>
        public void Init(byte Cmd = 0xFF, byte SubCmd = 0xFF, string Name = "")
        {
            this.Cmd = Cmd;
            this.SubCmd = SubCmd;
            this.Name = Name;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Comman code cause of the exception.
        /// </summary>
        public byte Cmd { get; protected set; }

        /// <summary>
        /// Sub command code cause of the exception.
        /// </summary>
        public byte SubCmd { get; protected set; }

        /// <summary>
        /// Name of command cause of the exception.
        /// </summary>
        public string Name { get; protected set; }
        #endregion
    }
}
