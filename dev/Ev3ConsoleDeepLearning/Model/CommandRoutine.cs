using Ev3Controller.Ev3Command;
using Ev3Controller.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public abstract class CommandRoutine
    {
        #region Constructors and the Finalizer
        public CommandRoutine()
        {
            var Now = DateTime.Now;
            this.LogFileName = Now.ToString("yyyyMMddhhmmss") + @".log";
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// File name of routine log.
        /// </summary>
        public string LogFileName;
        #endregion

        #region Other methods and private properties in calling order
        public abstract bool Routine(
            ComPortAccess ComPortAcc,
            ComPortSendRecvSequence Sequence,
            int TimerCount = 0);

        /// <summary>
        /// Make command log. Write Sent and received data
        /// </summary>
        /// <param name="CommandQueue"></param>
        public void Log(Queue<ACommand> CommandQueue)
        {
            var Now = DateTime.Now;

            using (StreamWriter Writer =
                new StreamWriter(this.LogFileName, true, Encoding.GetEncoding("utf-8")))
            {
                foreach (ACommand Command in CommandQueue)
                {
                    Writer.WriteLine(
                        Now.ToString("yyyy/MM/dd hh:mm:ss ") +
                        @"Snd: " + Ev3Utility.Buff2String(Command.CmdData));
                    Writer.WriteLine(
                        Now.ToString("yyyy/MM/dd hh:mm:ss ") +
                        @"Rcv: " + Ev3Utility.Buff2String(Command.ResData));
                }
            }
        }

        /// <summary>
        /// Make command log. Write Sent and received data
        /// </summary>
        /// <param name="CommandQueue"></param>
        public void Log(ACommand Command)
        {
            var Now = DateTime.Now;

            using (StreamWriter Writer =
                new StreamWriter(this.LogFileName, true, Encoding.GetEncoding("utf-8")))
            {
                Writer.WriteLine(
                    Now.ToString("yyyy/MM/dd hh:mm:ss ") +
                    @"Snd: " + Ev3Utility.Buff2String(Command.CmdData));
                Writer.WriteLine(
                    Now.ToString("yyyy/MM/dd hh:mm:ss ") +
                    @"Rcv: " + Ev3Utility.Buff2String(Command.ResData));
            }
        }
        #endregion
    }
}
