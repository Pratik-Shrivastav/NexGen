using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NextGen.Request;

namespace NextGen.Service
{
    public interface IAdminService
    {
        void AddAdmin(AdminRequest admin);
        void UpdateAdmin(AdminRequest admin);
        void LoginAdmin(LoginRequest loginRequest);
        string VerifyOtp(VerifyOtpRequest verifyOtpRequest);
        void LogOut(string username);
        
    }
}