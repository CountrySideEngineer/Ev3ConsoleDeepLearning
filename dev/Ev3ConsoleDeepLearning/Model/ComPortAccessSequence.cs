using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    /// <summary>
    /// Implement each sequence to access COM port, open and close , and read and write.
    /// </summary>
    public abstract class ComPortAccessSequence
    {
        #region Public constants
        protected enum StateIndex
        {
            STATE_INDEX_BASE,
            STATE_INDEX_STARTING,
            STATE_INDEX_STARTED,
            STATE_INDEX_FINISHED,
        };
        #endregion

        #region Public constants
        /// <summary>
        /// Flag whether the loop continues or not.
        /// This flag must be changed only in this class method, \"Sequence\" or \"StopSequence\".
        /// </summary>
        public bool DoesContinue { get; protected set; }

        /// <summary>
        /// Flag shows the method \"Sequence\" is running or not.
        /// </summary>
        public bool IsRunning { get; protected set; }
        #endregion

        #region Public Properties
        public delegate void TaskFinishedEventHandler(object sender, EventArgs e);
        public event TaskFinishedEventHandler TaskFinishedEvent;

        /// <summary>
        /// Delegate to notify sent and received data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void NotifySendReceiveDataEventHandler(object sender, EventArgs e);
        public event NotifySendReceiveDataEventHandler NotifySendReceiveDataEvent;

        /// <summary>
        /// Delegata to notify an exception raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void NotifyRecvExceptionEventHandler(object sender, EventArgs e);
        public event NotifyRecvExceptionEventHandler NotifyRecvExceptionEvent;
        #endregion

        #region Public Properties
        /// <summary>
        /// Flag show whether the object has message to show before the sequence starts.
        /// </summary>
        public bool HasBaseMessage
        {
            get
            {
                return this.HasMessageArrayItem(StateIndex.STATE_INDEX_BASE);
            }
        }

        /// <summary>
        /// Flag show whether the object has message to show when the sequence starting.
        /// </summary>
        public bool HasStartingMessage
        {
            get
            {
                return this.HasMessageArrayItem(StateIndex.STATE_INDEX_STARTED);
            }
        }

        /// <summary>
        /// Flag show whether the object has message to show when the sequence started.
        /// </summary>
        public bool HasStartedMessage
        {
            get
            {
                return this.HasMessageArrayItem(StateIndex.STATE_INDEX_STARTED);
            }
        }

        /// <summary>
        /// Flag show whether the object has message to show when the sequence finished.
        /// </summary>
        public bool HasFinishedMessage
        {
            get
            {
                return this.HasMessageArrayItem(StateIndex.STATE_INDEX_FINISHED);
            }
        }

        /// <summary>
        /// A message shown before the sequence starts.
        /// </summary>
        public string BaseMessage
        {
            get
            {
                return this.MessageArrayItem(StateIndex.STATE_INDEX_BASE);
            }
        }

        /// <summary>
        /// A message shown when the sequence starting.
        /// </summary>
        public string StartingMessage
        {
            get
            {
                return this.MessageArrayItem(StateIndex.STATE_INDEX_STARTING);
            }
        }

        /// <summary>
        /// A message shown when the sequence started.
        /// </summary>
        public string StartedMessage
        {
            get
            {
                return this.MessageArrayItem(StateIndex.STATE_INDEX_STARTED);
            }
        }

        /// <summary>
        /// A message shown when the sequence finished.
        /// </summary>
        public string FinishedMessage
        {
            get
            {
                return this.MessageArrayItem(StateIndex.STATE_INDEX_FINISHED);
            }
        }

        /// <summary>
        /// ConnectionState notified while starting sequence.
        /// </summary>
        public ConnectionState BaseConnectionState
        {
            get
            {
                return this.ConnectinoStateArrayItem(StateIndex.STATE_INDEX_BASE);
            }
        }

        /// <summary>
        /// ConnectionState notified while starting sequence.
        /// </summary>
        public ConnectionState StartingConnectionState
        {
            get
            {
                return this.ConnectinoStateArrayItem(StateIndex.STATE_INDEX_STARTING);
            }
        }

        /// <summary>
        /// ConnectionState notified when the sequence started.
        /// </summary>
        public ConnectionState StartedConnectionState
        {
            get
            {
                return this.ConnectinoStateArrayItem(StateIndex.STATE_INDEX_STARTED);
            }
        }

        /// <summary>
        /// ConnectionState notified when the sequence finished.
        /// </summary>
        public ConnectionState FinishedConnectionState
        {
            get
            {
                return this.ConnectinoStateArrayItem(StateIndex.STATE_INDEX_FINISHED);
            }
        }
        #endregion

        #region Constructors and the Finalizer
        public ComPortAccessSequence() { this.Init(); }

        #endregion

        #region Other methods and private properties in calling order
        protected virtual void Init()
        {
            ConnectionStateInformationDictionary = null;
        }
        /// <summary>
        /// Call a sequence method in other thread.
        /// </summary>
        /// <param name="obj"></param>
        public Task StartSequence(ComPortAccess ComPortAcc)
        {
            //Task<object> task = Task<object>.Run(() =>
            //{
            //    return this.Sequence(ComPortAcc);
            //});
            //Task ContinuationTask = task.ContinueWith((Antecedent) =>
            //{
            //    object Result = Antecedent.Result;
            //    if (Result != null)
            //    {
            //        if (Result is bool)
            //        {
            //            bool SeqResult = (bool)Result;
            //            if (SeqResult)
            //            {
            //                this.OnTaskFinishedEvent(
            //                    new SequenceChangedEventArgs(
            //                        this.FinishedConnectionState,
            //                        SeqResult));
            //            }
            //            else
            //            {
            //                this.OnTaskFinishedEvent(
            //                    new SequenceChangedEventArgs(
            //                        this.BaseConnectionState,
            //                        SeqResult));
            //            }
            //        }
            //        else
            //        {
            //            this.OnTaskFinishedEvent(
            //                new SequenceChangedEventArgs(this.FinishedConnectionState));
            //        }
            //    }
            //    else
            //    {
            //        this.OnTaskFinishedEvent(
            //            new SequenceChangedEventArgs(this.FinishedConnectionState));
            //    }
            //});
            //return ContinuationTask;
            return null;
        }

        /// <summary>
        /// Wait for Sequence method does finish.
        /// </summary>
        public void StopSequence()
        {
            if (this.IsRunning)
            {
                this.DoesContinue = false;
                while (this.IsRunning)
                {
                    Thread.Sleep(2);
                }
            }
        }

        /// <summary>
        /// Abstract method to run main sequence to access COM port.
        /// </summary>
        /// <param name="ComPortAcc">ComPortAccess object used to access COM port.</param>
        /// <returns>Result of the sequence.</returns>
        public abstract object Sequence(ComPortAccess ComPortAcc);

        /// <summary>
        /// A method to get flag shows whether this sequence has message to show 
        /// when state of sequence, starting, started, and finished.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        protected bool HasMessageArrayItem(StateIndex Index)
        {
            return ConnectionStateInformationDictionary[Index].HasMessage;
        }

        /// <summary>
        /// A method returns a string shown when the state of sequence changed.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        protected string MessageArrayItem(StateIndex Index)
        {
            return ConnectionStateInformationDictionary[Index].Message;
        }

        /// <summary>
        /// Amethod returns a ConnectionState.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        protected ConnectionState ConnectinoStateArrayItem(StateIndex Index)
        {
            return ConnectionStateInformationDictionary[Index].ConnState;
        }

        public void OnTaskFinishedEvent(EventArgs e)
        {
            this.TaskFinishedEvent?.Invoke(this, e); 
        }

        /// <summary>
        /// Raise NotifySendReceiveEvent event.
        /// </summary>
        /// <param name="e"></param>
        public void OnNotifySendReceiveData(EventArgs e)
        {
            this.NotifySendReceiveDataEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Raise NotifyRecvExceptionEvent
        /// </summary>
        /// <param name="e"></param>
        public void OnNotifyRecvExceptionEvent(EventArgs e)
        {
            this.NotifyRecvExceptionEvent?.Invoke(this, e);
        }
        protected Dictionary<StateIndex, ConnectionStateInformation> ConnectionStateInformationDictionary;
        #endregion

        #region Inner class
        protected class ConnectionStateInformation
        {
            public bool HasMessage { get; protected set; }
            public string Message { get; protected set; }
            public ConnectionState ConnState { get; protected set; }

            public ConnectionStateInformation(bool HasMessage, string Message)
            {
                this.HasMessage = HasMessage;
                this.Message = Message;
            }
            public ConnectionStateInformation(bool HasMessage, string Message, ConnectionState ConnState)
            {
                this.HasMessage = HasMessage;
                this.Message = Message;
                this.ConnState = ConnState;
            }
        }
        #endregion
    }
}
