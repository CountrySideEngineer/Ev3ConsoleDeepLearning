using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    using Ev3Command;
    using System.Threading;

    public class ComPortSendRecvSequence : ComPortAccessSequence
    {
        #region Public Properties
        /// <summary>
        /// Command queue to send in Sequence method.
        /// </summary>
        public Queue<ACommand> CommandQueue;
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Initialize the class.
        /// </summary>
        protected override void Init()
        {
            this.ConnectionStateInformationDictionary = new Dictionary<StateIndex, ConnectionStateInformation>
            {
                { StateIndex.STATE_INDEX_BASE,
                    new ConnectionStateInformation(
                        true, "Wait command request", ConnectionState.Connected) },
                { StateIndex.STATE_INDEX_STARTING,
                    new ConnectionStateInformation(
                        true, "Starting send and receive sequence.", ConnectionState.Connected) },
                { StateIndex.STATE_INDEX_STARTED,
                    new ConnectionStateInformation(
                        true, "Started send and receive sequence.", ConnectionState.Connected) },
                { StateIndex.STATE_INDEX_FINISHED,
                    new ConnectionStateInformation(
                        true, "Finished send and receive sequence.", ConnectionState.Connected) },
            };

            this.CommandQueue = new Queue<ACommand>();
        }

        /// <summary>
        /// A sequence to send and receive command. This sequence continues
        /// semi-permanently, till the sequence is stopped by action.
        /// </summary>
        /// <param name="ComPortAcc">Port information to access serial port.</param>
        /// <returns>Always null.</returns>
        public override object Sequence(ComPortAccess ComPortAcc)
        {
            this.IsRunning = true;
            this.DoesContinue = true;

            CommandRoutine Routine = new InitCommandRoutine();
            Routine.Routine(ComPortAcc, this);

            Routine = new PeriodicCommandRoutine();
            while (this.DoesContinue)
            {
                Routine.Routine(ComPortAcc, this);
            }
            this.IsRunning = false;

            return null;
        }

        /// <summary>
        /// A routine to send command and receive response, and notify the result.
        /// </summary>
        /// <param name="ComPortAcc"></param>
        /// <param name="Command"></param>
        /// <returns></returns>
        public bool SendAndRecvRoutine(ComPortAccess ComPortAcc, ACommand Command)
        {
            try
            {
                byte[] ResData;
                ComPortAcc.SendAndRecv(Command.CmdData, out ResData);
                Command.ResData = ResData;
                Command.Check();

                return true;
            }
            catch (CommandException CmdExpt)
            {
                Console.WriteLine(CmdExpt.Message);

                return false;
            }
        }
        #endregion
    }
}
