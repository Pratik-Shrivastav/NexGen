using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Model;
using NextGen.Enum;

namespace NextGen.Service
{
    public interface ITransactionService
    {
        ICollection<PlayTransaction> GetAllTransaction();
        PlayTransaction EndPlayTransaction(int id,string subject);
        public void Payment(int transactionId, PaymentMethod paymentMethod, string subject);
    }
}