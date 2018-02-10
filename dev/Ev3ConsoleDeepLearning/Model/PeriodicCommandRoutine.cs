using Ev3Controller.Ev3Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class PeriodicCommandRoutine : CommandRoutine
    {
        #region Constructors and the Finalizer
        /// <summary>
        /// Constructor.
        /// Setup command to send in the routine.
        /// </summary>
        public PeriodicCommandRoutine()
        {
            this.CommandQueue = new Queue<ACommand>();
            this.CommandQueue.Enqueue(new Command_06_00());
            this.CommandQueue.Enqueue(new Command_0C_00());
            this.CommandQueue.Enqueue(new Command_12_00());
            this.CommandQueue.Enqueue(new Command_16_00());
            this.CommandQueue.Enqueue(new Command_10_01());
            this.CommandQueue.Enqueue(new Command_F0_00());
        }
        #endregion

        #region Public Properties
        public Queue<ACommand> CommandQueue { get; protected set; }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Method to run periodic command routine.
        /// </summary>
        /// <param name="ComPortAcc">ComPortAccess class contains COM port abstract object.</param>
        /// <param name="Sequence">Sequence class to run routine.</param>
        /// <param name="TimerCount">Passed time.</param>
        /// <returns></returns>
        public override bool Routine(
            ComPortAccess ComPortAcc,
            ComPortSendRecvSequence Sequence,
            int TimerCount = 0)
        {
            foreach (ACommand Command in this.CommandQueue)
            {
                Thread.Sleep(1);
                Command.UpdateCmdData();
                try
                {
                    Sequence.SendAndRecvRoutine(ComPortAcc, Command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            this.Log(CommandQueue);

            return false;
        }
        #endregion
    }
}
