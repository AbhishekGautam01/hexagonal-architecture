using Accounting.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess.Repositories
{
    public class AccountRepository : IAccountReadOnlyRepository, IAccountWriteOnlyRepository
    {
        private readonly Context _context;
        public AccountRepository(Context context)
        {
            _context = context;
        }
        public async Task Add(Account account, Credit credit)
        {
            Entities.Account accountEntity = new Entities.Account()
            {
                CustomerId = account.CustomerId,
                Id = account.Id
            };
            Entities.Credit creditEntity = new Entities.Credit()
            {
                AccountId = credit.AccountId,
                Amount = credit.Amount,
                Id = credit.Id,
                TransactionDate = credit.TransactionDate
            };
            await _context.Accounts.InsertOneAsync(accountEntity);
            await _context.Credits.InsertOneAsync(creditEntity);
        }

        public async Task Delete(Account account)
        {
            await _context.Credits.DeleteManyAsync(e => e.AccountId == account.Id);
            await _context.Debits.DeleteManyAsync(e => e.AccountId == account.Id);
            await _context.Accounts.DeleteOneAsync(e => e.Id == account.Id);
        }

        public async Task<Account> Get(Guid id)
        {
            Entities.Account account = await _context
                .Accounts
                .Find(e => e.Id == id)
                .SingleOrDefaultAsync();

            List<Entities.Credit> credits = await _context
                .Credits
                .Find(e => e.AccountId == id)
                .ToListAsync();
            List<Entities.Debit> debits = await _context
                .Debits
                .Find(e => e.AccountId == id)
                .ToListAsync();
            List<ITransaction> transactions = new List<ITransaction>();

            foreach (Entities.Credit transactionData in credits)
            {
                Credit transaction = Credit.Load(
                    transactionData.Id,
                    transactionData.AccountId,
                    transactionData.Amount,
                    transactionData.TransactionDate);

                transactions.Add(transaction);
            }

            foreach (Entities.Debit transactionData in debits)
            {
                Debit transaction = Debit.Load(
                    transactionData.Id,
                    transactionData.AccountId,
                    transactionData.Amount,
                    transactionData.TransactionDate);

                transactions.Add(transaction);
            }
            var orderedTransactions = transactions.OrderBy(o => o.TransactionDate).ToList();

            TransactionCollection transactionCollection = new TransactionCollection();
            transactionCollection.Add(orderedTransactions);

            Account result = Account.Load(
                account.Id,
                account.CustomerId,
                transactionCollection);

            return result;
        }

        public async Task Update(Account account, Credit credit)
        {
            Entities.Credit creditEntity = new Entities.Credit
            {
                AccountId = credit.AccountId,
                Amount = credit.Amount,
                Description = credit.Description,
                Id = credit.Id,
                TransactionDate = credit.TransactionDate
            };
            var filter = Builders<Entities.Credit>.Filter.Eq(e => e.Id, credit.Id);

            await _context.Credits.ReplaceOneAsync(filter, creditEntity);
        }

        public async Task Update(Account account, Debit debit)
        {
            Entities.Debit debitEntity = new Entities.Debit
            {
                AccountId = debit.AccountId,
                Amount = debit.Amount,
                Description = debit.Description,
                Id = debit.Id,
                TransactionDate = debit.TransactionDate
            };
            var filter = Builders<Entities.Debit>.Filter.Eq(e => e.Id, debit.Id);

            await _context.Debits.ReplaceOneAsync(filter, debitEntity);
        }
    }
}
