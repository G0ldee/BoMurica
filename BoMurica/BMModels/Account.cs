using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMModels
{
    internal class Account
    {
        private String _clientId;
        private Decimal _initDepositDebt;
        private String _type;

        public String ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        public Decimal InitDepositDebt
        {
            get { return _initDepositDebt; }
            set { _initDepositDebt = value;}
        }

        public String Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
