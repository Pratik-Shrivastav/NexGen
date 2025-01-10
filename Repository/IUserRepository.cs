using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Model;

namespace NextGen.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);
        User GetUserByPhoneNumber(string phoneNumber);
        void SaveChanges();
        User GetAllTransaction(string phoneNumber);
        User GetAllMembershipTransaction(string phoneNumber);

        ICollection<PlayTransaction> GetAllPlayTransactions();
        ICollection<MembershipTransaction> GetAllMembershipTransactions();

        void UpdatePlayTransaction(PlayTransaction playTransaction);
        PlayTransaction GetTransactionById(int id);

    }
}