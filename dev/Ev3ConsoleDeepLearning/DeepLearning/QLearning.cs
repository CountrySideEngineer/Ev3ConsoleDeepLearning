using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ev3ConsoleDeepLearning.DeepLearning
{
    /// <summary>
    /// Class for machine learning, Q-Learning algorithm.
    /// </summary>
    public class QLearning
    {
        protected enum ACTION {
            ACTION_SPEED_UP = 0,
            ACTION_SLOW_DOWN,
            ACTION_TURN_RIGHT,
            ACTION_TURN_LEFT,
            ACTION_SPEED_KEEP,
            ACTION_NUM
        };

        protected const int STATE_MOTOR = 81;   //-41 to 41
        protected const int STATE_DIST = 251;   //  0 to 250
        protected const int Q_VALUE_MAX = 10000;// 0 to 100(LSB:0.01)
        protected const int Q_VALUE_MIN = 0;    // minimum value.
        protected const Int16 ALPHA = 10;       //Learning coefficient, LSB : 0.01.
        protected const Int16 GANNMA = 90;      //Discount rate, LSB : 0.01. 

        protected Int16[ , , , ] qvalues;//4 dimensional array.
        protected Random rnd;

        /// <summary>
        /// Initialize values.
        /// </summary>
        public void Init()
        {
            this.rnd = new Random(10000);

            this.InitQValues();
        }

        /// <summary>
        /// Initialize q-values.
        /// </summary>
        protected void InitQValues()
        {
            qvalues = new Int16[STATE_MOTOR, STATE_MOTOR, STATE_DIST, (int)ACTION.ACTION_NUM];
            for (int i = 0; i < STATE_MOTOR; i++)
            {
                for (int j = 0; j < STATE_MOTOR; j++)
                {
                    for (int k = 0; k < STATE_DIST; k++)
                    {
                        for (int l = 0; l < (int)ACTION.ACTION_NUM; l++)
                        {
                            qvalues[i, j, k, l] = (Int16)rnd.Next(Q_VALUE_MIN, Q_VALUE_MAX);

                            if ((0 == i) || (0 == j))
                            {
                                qvalues[i, j, k, (int)ACTION.ACTION_SLOW_DOWN] = 0;
                            }
                            if (((STATE_MOTOR - 1) == i) || ((STATE_MOTOR - 1) == j) || (k < 10))
                            {
                                qvalues[i, j, k, (int)ACTION.ACTION_SPEED_KEEP] = 0;
                                qvalues[i, j, k, (int)ACTION.ACTION_TURN_LEFT] = 0;
                                qvalues[i, j, k, (int)ACTION.ACTION_TURN_RIGHT] = 0;
                                qvalues[i, j, k, (int)ACTION.ACTION_SLOW_DOWN] = 0;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Select action based on current state decided by motor output power and distance read
        /// from ultrasonic sensor.
        /// </summary>
        /// <param name="MotorState1">Motor output power.</param>
        /// <param name="MotorState2">Motor output power.</param>
        /// <param name="Distance">Distance read from ultrasonic sensor.</param>
        /// <returns>
        /// Action selected (decided) base on "Q" values specified by motor output
        /// power and distance.
        /// </returns>
        protected const Int16 EPSILON = 3000;
        protected ACTION SelectAction(int MotorState1, int MotorState2, int Distance)
        {
            ACTION SelAction = ACTION.ACTION_NUM;

            if (this.Random100() < EPSILON)
            {
                do
                {
                    SelAction = this.SelectActionByRandom();
                } while (0 == this.qvalues[MotorState1, MotorState2, Distance, (int)SelAction]);
            }
            else
            {
                SelAction = this.SelectActionByQ(MotorState1, MotorState2, Distance);
            }

            return SelAction;
        }

        /// <summary>
        /// Select action based on Q-Learned value, qvalues, specified by motor output powers and
        /// distance read from ultrasonic sensor.
        /// </summary>
        /// <param name="MotorState1">motor output power</param>
        /// <param name="MotorState2">motor output power</param>
        /// <param name="Distance">distance read from ultrasonic sensor</param>
        /// <returns>selected action</returns>
        ACTION SelectActionByQ(int MotorState1, int MotorState2, int Distance)
        {
            ACTION SelAction = ACTION.ACTION_NUM;
            int QMax = 0;
            int ActionIndex = 0;

            for (ActionIndex = 0; ActionIndex < (int)ACTION.ACTION_NUM; ActionIndex++)
            {
                if (QMax < this.qvalues[MotorState1, MotorState2, Distance, ActionIndex])
                {
                    QMax = this.qvalues[MotorState1, MotorState2, Distance, ActionIndex];
                    SelAction = (ACTION)ActionIndex;
                }
            }

            return SelAction;
        }

        /// <summary>
        /// Returns action randomly.
        /// </summary>
        /// <returns></returns>
        ACTION SelectActionByRandom()
        {
            return (ACTION)(this.rnd.Next() % (int)ACTION.ACTION_NUM);
        }

        /// <summary>
        /// Select next state based on current motor output, distance, and action
        /// based on these values.
        /// </summary>
        /// <param name="MotorState1">Current motor output.</param>
        /// <param name="MotorState2">Current motor output.</param>
        /// <param name="Distance">Distance read from ultrasonic sensor.</param>
        /// <param name="CurAction">Current action </param>
        /// <param name="NextMotorState1">Referenceto next motor output.</param>
        /// <param name="NextMotorState2">Referenceto next motor output.</param>
        void NextState(int MotorState1, int MotorState2, int Distance, ACTION CurAction, 
            ref int NextMotorState1, ref int NextMotorState2)
        {
            switch (CurAction)
            {
                case ACTION.ACTION_SPEED_UP:
                    NextMotorState1 = MotorState1 + 1;
                    NextMotorState2 = MotorState2 + 1;
                    break;

                case ACTION.ACTION_SLOW_DOWN:
                    NextMotorState1 = MotorState1 - 1;
                    NextMotorState2 = MotorState2 - 1;
                    break;

                case ACTION.ACTION_TURN_LEFT:
                    NextMotorState1 = MotorState1 + 1;
                    NextMotorState2 = MotorState2;
                    break;

                case ACTION.ACTION_TURN_RIGHT:
                    NextMotorState1 = MotorState1;
                    NextMotorState2 = MotorState2 + 1;
                    break;

                case ACTION.ACTION_SPEED_KEEP:
                default:
                    NextMotorState1 = MotorState1;
                    NextMotorState2 = MotorState2;
                    break;
            }

            if (NextMotorState1 <= 0) { NextMotorState1 = 0; }
            if (NextMotorState2 <= 0) { NextMotorState2 = 0; }
            if (STATE_MOTOR <= NextMotorState1) { NextMotorState1 = STATE_MOTOR - 1; }
            if (STATE_MOTOR <= NextMotorState2) { NextMotorState2 = STATE_MOTOR - 1; }
        }

        /// <summary>
        /// Update Q-Learning value, qvalues.
        /// </summary>
        /// <param name="MotorState1">Current motor output.</param>
        /// <param name="MotorState2">Current motor output.</param>
        /// <param name="Distance">Distance read from ultrasonic sensor.</param>
        /// <param name="CurAction">Current action </param>
        /// <param name="NextMotorState1">Next motor output.</param>
        /// <param name="NextMotorState2">Next motor output.</param>
        /// <param name="IsPenalty"></param>
        void UpdateQValues(int MotorState1, int MotorState2, int Distance, ACTION CurAct,
            int NextMotorState1, int NextMotorState2, bool IsPenalty)
        {
            ACTION NextAct;
            Int16 qvalue = 0;
            Int16 NextQValue = 0;
            Int16 AlphaItem = 0;
            Int16 GannmaItem = 0;

            if (!IsPenalty)
            {
                NextAct = SelectAction(NextMotorState1, NextMotorState2, Distance);
                NextQValue = (Int16)this.qvalues[NextMotorState1, NextMotorState2, Distance, (int)NextAct];
                qvalue = (Int16)this.qvalues[MotorState1, MotorState2, Distance, (int)CurAct];

                GannmaItem = (Int16)((Int32)(NextQValue * GANNMA) / 100);
                AlphaItem = (Int16)((((Int32)GannmaItem - (Int32)qvalue) * ALPHA) / 100);
                this.qvalues[MotorState1, MotorState2, Distance, (int)CurAct] += AlphaItem;
                if (Q_VALUE_MAX <= this.qvalues[MotorState1, MotorState2, Distance, (int)CurAct])
                {
                    this.qvalues[MotorState1, MotorState2, Distance, (int)CurAct] = Q_VALUE_MAX;
                }
                if (this.qvalues[MotorState1, MotorState2, Distance, (int)CurAct] <= Q_VALUE_MIN)
                {
                    this.qvalues[MotorState1, MotorState2, Distance, (int)CurAct] = Q_VALUE_MIN;
                }
            }
        }

        /// <summary>
        /// Create random value from 0 to 100, LSB is 0.01.
        /// </summary>
        /// <returns>Rando value</returns>
        protected Int16 Random100()
        {
            return (Int16)this.rnd.Next(Q_VALUE_MIN, Q_VALUE_MAX);
        }
    }
}