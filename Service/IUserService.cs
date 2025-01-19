using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Model;
using NextGen.Request;

namespace NextGen.Service
{
    public interface IUserService
    {
        void AddUser(UserRequest userRequest, string subject);
        void ResendOTP(string phonenumber);
        void ValidateOtp(string phonenumber, double otp, string subject);
        User GetUserByPhoneNumber(string phonenumber);
        void AddMembership(string phonenumber, MembershipRequest membershipRequest, string subject);
        void StartPlayTime(string phonenumber, PlayTransactionRequest playTransactionRequest, string subject);
        User GetAllTransactionsOfUser(string phonenumber);
        User GetAllMembershipTransactionsOfUser(string phonenumber);
    }
}