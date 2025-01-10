using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Model;

namespace NextGen.Repository
{
    public interface IAdminRepository
    {
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        Admin GetAdminByUserName(string userName);
    }
}