using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BusinessLogic.Utils {
    public static class TransactionScopeProvider {

        public static TransactionScope CreateTransactionScope() {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.Serializable;
            options.Timeout = TransactionManager.MaximumTimeout;
            return new TransactionScope(TransactionScopeOption.Required, options);
        }
    }
}
