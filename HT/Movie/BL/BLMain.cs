using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BL
{

    class BLMain
    {
        private static string cs = Movie.Properties.Settings.Default.Elokuva;
        
        public static DataTable GetData()
        {
            try
            {
                Console.WriteLine(cs);
                DataTable dt = DB.DBElokuvat.GetData(cs);
                
                return dt;
             }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
