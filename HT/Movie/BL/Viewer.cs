using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DB;
using Movie.BL;

namespace Movie.BL
{
    class Viewer
    {
        private string username;
        private string password;
        private int id;
       
        public Viewer(int id, string username, string password)
        {
            this.password = password;
            this.username = username;
            this.id = id;
        }
        public string Username
            {
            get { return this.username; }
            set { this.username = value; }
            }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

    }
}
