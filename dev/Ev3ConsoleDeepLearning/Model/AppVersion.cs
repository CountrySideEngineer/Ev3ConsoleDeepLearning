using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class AppVersion
    {
        #region Constructors and the Finalizer
        public AppVersion()
        {
            this.Major = 0x00;
            this.Minor = 0x00;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Major version.
        /// </summary>
        public byte Major { get; set; }

        /// <summary>
        /// Minor version.
        /// </summary>
        public byte Minor { get; set; }
        #endregion
    }
}
