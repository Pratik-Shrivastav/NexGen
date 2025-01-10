using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Data;
using NexGen.Model;

namespace NextGen.Repository.Impl
{
    public class AdminRepository : IAdminRepository
    {
        private readonly NexGenDbContext _context;

        public AdminRepository(NexGenDbContext context)
        {
            _context = context;
        }

        public void AddAdmin(Admin admin)
        {
            _context.AdminTable.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            _context.AdminTable.Update(admin);
            _context.SaveChanges();
        }

        public Admin GetAdminByUserName(string userName)
        {
            return _context.AdminTable.FirstOrDefault(a => a.Username == userName);
        }
    }
}