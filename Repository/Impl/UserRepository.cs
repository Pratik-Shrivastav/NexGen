using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexGen.Data;
using NexGen.Model;

namespace NextGen.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly NexGenDbContext _context;

        public UserRepository(NexGenDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.UserTable.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.UserTable.Update(user);
            _context.SaveChanges();
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return _context.UserTable.Include(x=>x.MembershipTransactions).Include(y=>y.PlayTransactions).FirstOrDefault(u => u.Phone == phoneNumber);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public User GetAllTransaction(string phoneNumber)
        {
            return _context.UserTable.Include(x=>x.PlayTransactions).FirstOrDefault(u => u.Phone == phoneNumber);
        }

        public User GetAllMembershipTransaction(string phoneNumber)
        {
            return _context.UserTable.Include(x=>x.MembershipTransactions).FirstOrDefault(u => u.Phone == phoneNumber);
        }

        public ICollection<PlayTransaction> GetAllPlayTransactions()
        {
            return _context.PlayTransactionTable.Include(x=>x.User).ToList();
        }

        public ICollection<MembershipTransaction> GetAllMembershipTransactions()
        {
            return _context.MembershipTransactionTable.ToList();
        }

        public void UpdatePlayTransaction(PlayTransaction playTransaction)
        {
            _context.PlayTransactionTable.Update(playTransaction);
            _context.SaveChanges();
        }

        public PlayTransaction GetTransactionById(int id)
        {
            return _context.PlayTransactionTable.Include(x=>x.User).FirstOrDefault(x => x.Id == id);
            
        }


    }
}