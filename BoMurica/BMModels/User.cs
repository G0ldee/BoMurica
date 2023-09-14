using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMModels
{
    internal class User
    {
        public bool IsAdmin { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Selection { get; set; }
        public int Attemps { get; set; }

        public User() { this.Attemps = 0; }
    }
}
