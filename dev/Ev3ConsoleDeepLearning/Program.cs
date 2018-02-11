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
            int MotorState1 = 0;
            int MotorState2 = 0;
            int NextMotorState1 = 0;
            int NextMotorState2 = 0;
            int MotorOutput1 = 0;
            int MotorOutput2 = 0;
            int Distance = 0;
            int Direction = 0;

            string ComPortName;

            QLearning QLearn = new QLearning();
            QLearn.Init();

            ComPortName = args[0];//Name of COM port

            Program.ConnectRoutine(ComPortName);

            ComPortSendRecvSequence Sequence = new ComPortSendRecvSequence();
            var GetDistance = new Command_20_00();
            var GetMotor = new Command_10_01();
            var SetMotor = new Command_12_00();
            var Updater = BrickUpdater.Factory(GetMotor); 
            for (int index = 0; index < 3000; index++)
            {
                try
                {
                    if (Sequence.SendAndRecvRoutine(Program.BtPortAccess, GetMotor))
                    {
                        //Console.WriteLine("Snd:" + Ev3Utility.Buff2String(GetMotor.CmdData));
                        //Console.WriteLine("Rcv:" + Ev3Utility.Buff2String(GetMotor.ResData));
                    }
                    System.Threading.Thread.Sleep(50);//Wait 50 msec.
                    if (Sequence.SendAndRecvRoutine(Program.BtPortAccess, GetDistance))
                    {
                        //Console.WriteLine("Snd:" + Ev3Utility.Buff2String(GetDistance.CmdData));
                        //Console.WriteLine("Rcv:" + Ev3Utility.Buff2String(GetDistance.ResData));
                    }

                    BrickUpdater.Factory(GetMotor).Update(GetMotor, Ev3Brick.GetInstance());
                    BrickUpdater.Factory(GetDistance).Update(GetDistance, Ev3Brick.GetInstance());

                    MotorOutput1 = Ev3Brick.GetInstance().MotorDevice(0).Power;
                    MotorOutput2 = Ev3Brick.GetInstance().MotorDevice(3).Power;

                    MotorState1 = MotorOutput1 + 41;
                    MotorState2 = MotorOutput2 + 41;
                    Distance = Ev3Brick.GetInstance().SensorDevice(2).Value1;
                    if (250 < Distance)
                    {
                        Distance = 250;
                    }

                    QLearning.ACTION Act = QLearn.SelectAction(
                        (Int16)MotorState1, (Int16)MotorState2, Distance);
                    QLearn.NextState(
                        (Int16)MotorState1, (Int16)MotorState2, Distance, Act,
                        ref NextMotorState1, ref NextMotorState2);
                    QLearn.UpdateQValues(
                        (Int16)MotorState1, (Int16)MotorState2, Distance, Act,
                        NextMotorState1, NextMotorState2, false);

                    Console.WriteLine((int)Act + ","
                            + MotorOutput1 + ","
                            + MotorOutput2 + ","
                            + (NextMotorState1 - 41) + ","
                            + (NextMotorState2 - 41) + ","
                            + Distance);

                    MotorOutput1 = NextMotorState1 - 41;
                    MotorOutput2 = NextMotorState2 - 41;

                    if (MotorOutput1 < 0)
                    {
                        MotorOutput1 = Math.Abs(MotorOutput1);
                        Direction = 0;
                    }
                    else
                    {
                        Direction = 1;
                    }

                    System.Threading.Thread.Sleep(50);//Wait 50 msec.
                    SetMotor.UpdateCmdData(new CommandParam_12_00((byte)MotorOutput1, (byte)Direction));
                    if (Sequence.SendAndRecvRoutine(Program.BtPortAccess, SetMotor))
                    {
                        //Console.WriteLine("Snd:" + Ev3Utility.Buff2String(SetMotor.CmdData));
                        //Console.WriteLine("Rcv:" + Ev3Utility.Buff2String(SetMotor.ResData));
                    }
                    System.Threading.Thread.Sleep(50);//Wait 50 msec.
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
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
