//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BoMurica
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminLoginCred
    {
        public int CredentialsID { get; set; }
        public string AdminID { get; set; }
        public string UserCode { get; set; }
        public int AdPassword { get; set; }
    
        public virtual AdminInfo AdminInfo { get; set; }
    }
}
