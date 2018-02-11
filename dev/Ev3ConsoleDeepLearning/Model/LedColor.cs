using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public class LedColor
    {
        #region Public constants
        public enum COLOR
        {
            COLOR_OFF,
            COLOR_RED,
            COLOR_GREEN,
            COLOR_ORANGE,
            COLOR_MAX
        }
        #endregion

        #region Public read-only static fields
        protected static Dictionary<COLOR, string> ColorNameDictionary = new Dictionary<COLOR, string>
        {
            { COLOR.COLOR_OFF, "OFF" },
            { COLOR.COLOR_RED, "Red" },
            { COLOR.COLOR_GREEN, "Green" },
            { COLOR.COLOR_ORANGE, "Orange" },
        };
        #endregion

        #region Constructors and the Finalizer
        #endregion

        #region Public Properties
        /// <summary>
        /// Color of light.
        /// </summary>
        public COLOR Color { get; set; }

        /// <summary>
        /// Name about Color property.
        /// </summary>
        public string ColorName
        {
            get
            {
                return ColorNameDictionary[this.Color];
            }
        }

        #endregion
    }
}
