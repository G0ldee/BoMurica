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
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class ATM
    {
        public int AtmID { get; set; }
        public decimal Supply { get; set; }
        public bool Active { get; set; }

        internal void UpdateBalance(decimal balance)
        {
                using (BomDBEntities1 bomDb = new BomDBEntities1())
                {
                    this.Supply = this.Supply + balance;                
                    bomDb.ATMs.AddOrUpdate(this);
                    bomDb.SaveChanges();
                }

            }
        }
    }


