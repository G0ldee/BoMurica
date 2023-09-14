using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMModels
{
    internal class TransactionData
    {
        public bool Out { get; set; }
        public string ClientId { get; set; }
        public Decimal Amount { get; set; }
        public String Source { get; set; }
        public String Destination { get; set; }

    }
}
