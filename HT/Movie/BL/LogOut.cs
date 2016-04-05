using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Menu;

namespace Movie.BL
{
    class LogOut
    {
       public static void LogOutNow()
        {
            BLMain.current.Id = 0;
            BLMain.current.Password = "";
            BLMain.current.Username = "";
            Switcher.Switch(new LogIn());
        }
    }
}
