using Ev3Controller.Ev3Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class InitCommandRoutine : CommandRoutine
    {
        /// <summary>
        /// Method to run periodic command routine.
        /// In this routine, send EchoBack command till succeeds 5 times and AppVersion.
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
            int EchoBackOkCount = 0;
            var Command = new Command_00_00();
            while (true)
            {
                if (Sequence.SendAndRecvRoutine(ComPortAcc, Command))
                {
                    EchoBackOkCount++;
                }
                if (EchoBackOkCount > 10)
                {
                    EchoBackOkCount = 0;
                    break;
                }
            }

            while (!Sequence.SendAndRecvRoutine(ComPortAcc, new Command_02_00())) { }

            return false;
        }
    }
}
