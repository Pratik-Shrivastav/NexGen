using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NexGen.CommonFunction;
using NexGen.Model;
using NextGen.Repository;
using NextGen.Request;

namespace NextGen.Service.Impl
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        
        public AdminService(IAdminRepository adminRepository, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        

        public void AddAdmin(AdminRequest admin)
        {
            Admin adminNew = _mapper.Map<Admin>(admin);
            adminNew.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            _adminRepository.AddAdmin(adminNew);
        }

        public void UpdateAdmin(AdminRequest admin)
        {
            Admin adminNew = _mapper.Map<Admin>(admin);
            Admin oldAdmin = _adminRepository.GetAdminByUserName(admin.Username);
            if(oldAdmin == null){
                throw new Exception("Admin not found");
            }
            if(!oldAdmin.IsOtpVerified){
                throw new Exception("Admin is not verified");
            }
            adminNew.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            _adminRepository.UpdateAdmin(adminNew);
        }

        public void LoginAdmin(LoginRequest loginRequest)
        {
            Admin admin = _adminRepository.GetAdminByUserName(loginRequest.Username);
            if (admin == null)
            {
                throw new Exception("Invalid username or password");
            }
            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, admin.Password))
            {
                throw new Exception("Invalid username or password");
            }
            admin.OTP = GenerateOtp.GenerateOtpToLogin();
            admin.OTPGeneratedAt = DateTime.Now;
            admin.IsOtpVerified = false;
            _adminRepository.UpdateAdmin(admin);
            EmailHandler.SendEmail(admin.Email, "OTP for Login", $"Your OTP is { admin.OTP}");
        }

        public string VerifyOtp(VerifyOtpRequest verifyOtpRequest)
        {
            Admin admin = _adminRepository.GetAdminByUserName(verifyOtpRequest.Username);
            if (admin == null)
            {
                throw new Exception("Invalid username or password");
            }
            if (admin.OTP != verifyOtpRequest.OTP)
            {
                throw new Exception("Invalid OTP");
            }
            if (admin.OTPGeneratedAt.AddMinutes(5) < DateTime.Now)
            {
                throw new Exception("OTP expired");
            }
            admin.IsOtpVerified = true;
            _adminRepository.UpdateAdmin(admin);
            string token = GenerateToken.GetToken(admin, _configuration);
            return token;
        }

        public void LogOut(string username)
        {
            Admin admin = _adminRepository.GetAdminByUserName(username);
            if (admin == null)
            {
                throw new Exception("Admin Not Found");
            }
            admin.IsOtpVerified = false;
            _adminRepository.UpdateAdmin(admin);
        }
    }
}