using BoMurica.BMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.Stores
{
    internal class CurrentUserService
    {
        private CurrentUserService() { }

        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        private static CurrentUserService _instance;

        private static readonly object _lock = new object();
        
        public static CurrentUserService Instance(User user)
        {
            if(_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CurrentUserService();
                        _instance.User = user;
                    }
                }
            }
            return _instance;
        }
    }
}

