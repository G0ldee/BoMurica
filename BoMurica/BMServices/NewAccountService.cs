using BoMurica.BMModels;
using BoMurica.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMServices
{
    internal class NewAccountService
    {
        private NewAccountService() { }

        private Account _account;

        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }

        private static NewAccountService _instance;

        private static readonly object _lock = new object();

        public static NewAccountService Instance(Account account)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new NewAccountService();
                        _instance.Account = account;
                    }
                }
            }
            return _instance;
        }
    }
}
