using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class SafeState
    {
        #region Private fields and constants (in a region)
        protected Dictionary<SAFE_STATE, string> SafeStateDictionary = new Dictionary<SAFE_STATE, string>()
        {
            { SAFE_STATE.SAFE_STATE_SAFE, "Safe" },
            { SAFE_STATE.SAFE_STATE_ATTN, "Attention" },
            { SAFE_STATE.SAFE_STATE_WARN, "Warning" },
            { SAFE_STATE.SAFE_STATE_STOP, "STOP" },
            { SAFE_STATE.SAFE_STATE_UNKNOWN, "ERROR" },
        };

        protected static Dictionary<SAFE_STATE, string> SafeStateResourceDictionary = new Dictionary<SAFE_STATE, string>()
        {
            { SAFE_STATE.SAFE_STATE_SAFE, @"../Resource/pict/safe_state_safe.png" },
            { SAFE_STATE.SAFE_STATE_ATTN, @"../Resource/pict/safe_state_attn.png" },
            { SAFE_STATE.SAFE_STATE_WARN, @"../Resource/pict/safe_state_warn.png" },
            { SAFE_STATE.SAFE_STATE_STOP, @"../Resource/pict/safe_state_stop.png" },
            { SAFE_STATE.SAFE_STATE_UNKNOWN, @"../Resource/pict/safe_state_unknown.png" },
        };
        #endregion

        #region Public constants
        public enum SAFE_STATE
        {
            SAFE_STATE_SAFE,
            SAFE_STATE_ATTN,
            SAFE_STATE_WARN,
            SAFE_STATE_STOP,
            SAFE_STATE_UNKNOWN,
            SAFE_STATE_MAX,
        };
        #endregion

        #region Constructors and the Finalizer
        public SafeState()
        {
            this.State = SAFE_STATE.SAFE_STATE_SAFE;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Safe state informatino.
        /// </summary>
        public SAFE_STATE State { get; set; }

        /// <summary>
        /// Safe state string.
        /// </summary>
        public string StateName
        {
            get
            {
                return SafeStateDictionary[this.State];
            }
        }

        /// <summary>
        /// Bitmap image which shows state.
        /// </summary>
        public string StateImage
        {
            get { return SafeState.SafeStateResourceDictionary[this.State]; }
        }
        #endregion
    }
}
