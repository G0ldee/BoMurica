using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoMurica.BMViewModel
{
    internal class MortgageViewModel:ViewModelBase
    {
         private readonly MortgageAccount _mortgageAccount;

        public string Account => _mortgageAccount.Account;
        public string ClientID => _mortgageAccount.ClientID;
        public decimal Balance => _mortgageAccount.Balance;

        public MortgageViewModel(MortgageAccount mortgage)
        {
            _mortgageAccount = mortgage;
        }
    }
    }

