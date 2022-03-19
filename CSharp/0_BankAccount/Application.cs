using System.Collections.Generic;
using System.Linq;

namespace CSharp
{
    public class BankAccount {
        private readonly IList<TransactionLine> _transactions;

        public BankAccount(IList<TransactionLine> transactions)
        {
            _transactions = transactions;
        }

        private (TransactionLine Transaction, TransactionStatus Status) CheckTransaction(TransactionLine t) =>
            t.Amount > Amount.Limit ? (t,TransactionStatus.Invalid) : (t, TransactionStatus.Valid);

        public IEnumerable<TransactionLine> AccountCheckSuspectTransaction()
        {
            var transactionsInvalid = _transactions
                .Select(CheckTransaction)
                .SelectMany(ts => ts.Status == TransactionStatus.Invalid 
                    ? Enumerable.Empty<(TransactionLine Transaction, TransactionStatus Status)>() 
                    : new List<(TransactionLine Transaction, TransactionStatus Status)>{ts}); //peu lisible ?
            
            var transactionsInvalidRefacto = _transactions
                .Select(CheckTransaction) //map du functor
                .Where(t => t.Status == TransactionStatus.Invalid); //on peut se dire que ça transforme une transaction en "je retourne rien, ou je retourne 1 transaction (une liste de 0 ou 1 transaction)" => Maybe Transaction ?
                    
            
            return transactionsInvalid.Select(t => t.Transaction);
        }
    }
}