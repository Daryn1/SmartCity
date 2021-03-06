﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Web.Models.Transactions
{
    public class TransactionViewModel
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public TransactionCategory Category { get; set; }

        public TransactionType Type { get; set; }

        public string SenderLogin { get; set; }

        public string RecipientLogin { get; set; }
    }
}
