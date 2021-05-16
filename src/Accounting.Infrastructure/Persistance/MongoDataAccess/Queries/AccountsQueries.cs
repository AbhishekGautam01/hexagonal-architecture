using Accounting.Application.Queries;
using Accounting.Application.Results;
using Accounting.Infrastructure.Persistance.MongoDataAccess.Entities;
using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Accounting.Infrastructure.Exceptions;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess.Queries
{
    public class AccountsQueries : IAccountQueries
    {
        private readonly Context _context;
        public AccountsQueries(Context context)
        {
            this._context = context;
        }
        public async Task<AccountResult> GetAccount(Guid accountId)
        {
            Account data = await _context
                .Accounts
                .Find(account => account.Id == accountId)
                .SingleOrDefaultAsync();
            if (data == null)
                throw new AccountNotFoundException($"The account {accountId} does not exist or has been closed");
            List<Credit> credits = await _context
                .Credits
                .Find(e => e.AccountId == accountId)
                .ToListAsync();
            List<Debit> debits = await _context
                .Debits
                .Find(e => e.AccountId == accountId)
                .ToListAsync();
            double creditSum = credits.Sum(c => c.Amount);
            double debitSum = debits.Sum(d => d.Amount);
            List<TransactionResult> transactionResults = new List<TransactionResult>();
            foreach (Credit credit in credits)
            {
                TransactionResult transaction = new TransactionResult(credit.Description, credit.Amount, credit.TransactionDate);
                transactionResults.Add(transaction);
            }
            foreach(Debit debit in debits)
            {
                TransactionResult transaction = new TransactionResult(debit.Description, debit.Amount, debit.TransactionDate);
                transactionResults.Add(transaction);
            }

            List<TransactionResult> orderedTransactions = transactionResults.OrderBy(e => e.TransactionDate).ToList();
            AccountResult accountResult = new AccountResult(data.Id, creditSum - debitSum, orderedTransactions);
            return accountResult;
        }
    }
}
