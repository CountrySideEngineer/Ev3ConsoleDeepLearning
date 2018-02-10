using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Ev3Command
{
    public abstract class ACommand_ResLenFlex : ACommand
    {
        #region Constructors and the Finalizer
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CommandParam"></param>
        public ACommand_ResLenFlex(ICommandParam CommandParam = null) : base(CommandParam) { }
        #endregion

        #region Other methods and private properties in calling order
        /// <summary>
        /// Check whether size of response data buffer and length set in reponse data, 
        /// calcurated data lenght from the number of device in the data, matche.
        /// (To be more precise, the size should equals the result of formula below.)
        ///     FORMULA : Device num x OneDataLen property.
        /// If they are not match, CommandLenException will be thrown.
        /// </summary>
        /// <returns>Length written in response data.</returns>
        protected override int CheckLen()
        {
            Debug.Assert(this.ResData != null);

            int ResLen = base.CheckLen();
            int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP;
            int DevNum = this.ResData[DataIndex];

            if (ResLen != (DevNum * this.OneDataLen) + 1)
            {
                throw new CommandLenException(
                        "Command or response data Len error",
                        this.Cmd, this.SubCmd, this.Name);
            }
            return ResLen;
        }

        /// <summary>
        /// Check parameters, expecially port number in response data.
        /// </summary>
        protected override void CheckParam()
        {
            int DevNum = base.CheckDevNum();
            int DataIndex = (int)RESPONSE_BUFF_INDEX.RESPONSE_BUFF_INDEX_RES_DATA_TOP + 1;
            for (int DevIndex = 0; DevIndex < DevNum; DevIndex++)
            {
                base.CheckPort(DataIndex);
                DataIndex += this.OneDataLen;
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Size of "ONE DATA".
        /// </summary>
        public byte OneDataLen { get; protected set; }
        #endregion
    }
}
