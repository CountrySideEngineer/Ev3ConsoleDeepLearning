using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Util
{
    public class Ev3Utility
    {
        #region Other methods and private properties in calling order
        public static string Buff2String(byte[] Buff)
        {
            string BuffString = string.Empty;
            try
            {
                foreach (byte BuffData in Buff)
                {
                    BuffString += string.Format(@"0x{0:x2} ", BuffData);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BuffString;
        }
        #endregion
    }
}
