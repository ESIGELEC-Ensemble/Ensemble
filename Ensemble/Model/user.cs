using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensemble.Model
{
    class User
    {
        private int id
        {
            get;
            set;
        }
        private string email
        {
            get;
            set;
        }
        private string password
        {
            get;
            set;
        }
        private string photoURL
        {
            get;
            set;
        }

        public User(int t_id, string t_email, string t_password, string t_photoURL)
        {
            id = t_id;
            email = t_email;
            password = t_password;
            photoURL = t_photoURL;
        }

    }
}
