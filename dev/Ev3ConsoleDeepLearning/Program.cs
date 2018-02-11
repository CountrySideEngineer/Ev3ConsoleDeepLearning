using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ev3Controller.Model;
using Ev3Controller.Ev3Command;
using Ev3Controller.Util;

namespace Ev3ConsoleDeepLearning
{
    using DeepLearning;

    class Program
    {
        #region Private fields and constants (in a region)
        /// <summary>
        /// Instance to control bluetooth port control.
        /// </summary>
        protected static ComPortAccess BtPortAccess;
        #endregion


        static void Main(string[] args)
        {
            string ComPortName;

            QLearning QLearn = new QLearning();
            QLearn.Init();

            ComPortName = args[0];//Name of COM port

            Program.ConnectRoutine(ComPortName);

            ComPortSendRecvSequence Sequence = new ComPortSendRecvSequence();
            ACommand Command = new Command_00_00();
            for (int index = 0; index < 500; index++)
            {
                if (Sequence.SendAndRecvRoutine(Program.BtPortAccess, Command))
                {
                    Console.WriteLine("Snd:" + Ev3Utility.Buff2String(Command.CmdData));
                    Console.WriteLine("Rcv:" + Ev3Utility.Buff2String(Command.ResData));
                }
            }

            Program.DisconnectRoutine();
        }

        /// <summary>
        /// Connect COM port to EV3 device via bluetooth.
        /// </summary>
        /// <param name="ComPortName"></param>
        protected static bool ConnectRoutine(string ComPortName)
        {
            Console.WriteLine("Start connecting sequence to EV3 using " + ComPortName);

            Program.BtPortAccess = new ComPortAccess(new ComPort(ComPortName, "Bt Port " + ComPortName));

            Console.Write("Connecting ... ");

            bool RoutineResult = false;
            try
            {
                if (BtPortAccess.Connect())
                {
                    Console.WriteLine("connected.");
                    RoutineResult = true;
                }
                else
                {
                    Console.WriteLine("failed.");
                    RoutineResult = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("A error occurred while port connection.");
                Console.WriteLine(ex.Message);
                RoutineResult = true;
            }
            return RoutineResult;
        }

        /// <summary>
        /// Disconnect COM port with EV3 device.
        /// </summary>
        /// <returns></returns>
        protected static bool DisconnectRoutine()
        {
            Console.Write("Start disconnecting sequence ... ");
            try
            {
                Program.BtPortAccess.Disconnect();

                Console.WriteLine("Disconnected!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("A error occurred while port disconnection.");
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
