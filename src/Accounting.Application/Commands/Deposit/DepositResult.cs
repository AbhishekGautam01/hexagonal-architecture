﻿using Accounting.Application.Results;
using Accounting.Domain.Accounts;
using Accounting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Commands.Deposit
{
    public sealed class DepositResult
    {
        public TransactionResult Transaction { get; }
        public double UpdatedBalance { get; }
        public DepositResult(
            Credit credit,
            Amount updatedBalance
            )
        {
            Transaction = new TransactionResult(credit.Description, credit.Amount, credit.TransactionDate);
            UpdatedBalance = updatedBalance;
        }
    }
}
