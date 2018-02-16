using Ev3Controller.Ev3Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3Controller.Model
{
    public abstract class BrickUpdater
    {
        #region Factory Methods
        public static BrickUpdater Factory(ACommand Cmd)
        {
            var UpdaterList = new[] {
                new { Cmd = 0x10, Sub = 0x00, Updater = ((new BrickUpdater_10_00()) as BrickUpdater) },
                new { Cmd = 0x10, Sub = 0x01, Updater = ((new BrickUpdater_10_01()) as BrickUpdater) },
                new { Cmd = 0x12, Sub = 0x00, Updater = ((new BrickUpdater_12_00()) as BrickUpdater) },
                new { Cmd = 0x20, Sub = 0x00, Updater = ((new BrickUpdater_20_00()) as BrickUpdater) },
            };
            var CmdUpdater = UpdaterList.Where(x => x.Cmd == Cmd.Cmd && x.Sub == Cmd.SubCmd);
            BrickUpdater Upater = null;
            if (CmdUpdater.Any())
            {
                /*
                 * The number of items in CmdUpdater must be one or zero because the UPDATER is
                 * associted with just only one command and sub command code.
                 * So the first item in reslt must be the target item.
                 */
                Upater = CmdUpdater.First().Updater;
                return Upater;
            }
            else
            {
                throw new ArgumentException(
                    string.Format("Cmd = 0x{0:x2} Sub = 0x{1:x2}", Cmd.Cmd, Cmd.SubCmd));
            }

        }
        #endregion

        #region
        public abstract void Update(ACommand Command, Ev3Brick Brick);
        #endregion
    }
}
