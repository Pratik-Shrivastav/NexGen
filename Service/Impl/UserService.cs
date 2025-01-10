using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using NexGen.CommonFunction;
using NexGen.Model;
using NextGen.Repository;
using NextGen.Request;

namespace NextGen.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public const int SILVER_HOURS = 10;
        public const int GOLD_HOURS = 20;
        public const int PLATINUM_HOURS = 100;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void AddUser(UserRequest userRequest, string subject)
        {
            var user = _mapper.Map<User>(userRequest);
            user.OTP = GenerateOtp.GenerateOtpToLogin();
            user.OTPGeneratedAt = DateTime.Now;
            user.CreatedOn = DateTime.Now;
            user.CreatedBy = subject;
            user.IsOtpVerified = false;
            user.Membership = NexGen.Enum.UserType.Normal;
            _userRepository.AddUser(user);
            EmailHandler.SendEmail(user.Email, "OTP for Login", $"Your OTP is {user.OTP}");
        }

        public void ResendOTP(string phonenumber){
            var user = _userRepository.GetUserByPhoneNumber(phonenumber);
            user.OTP = GenerateOtp.GenerateOtpToLogin();
            user.OTPGeneratedAt = DateTime.Now;
            EmailHandler.SendEmail(user.Email, "OTP for Login", $"Your OTP is {user.OTP}");
            _userRepository.AddUser(user);
        }

        public void ValidateOtp(string phonenumber, double otp, string subject)
        {
            var user = _userRepository.GetUserByPhoneNumber(phonenumber);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.OTP != otp)
            {
                throw new Exception("Invalid OTP");
            }
            user.IsOtpVerified = true;
            user.UpdatedOn = DateTime.Now;
            user.UpdatedBy = subject;
            _userRepository.UpdateUser(user);
        }

        public User GetUserByPhoneNumber(string phonenumber)
        {
            return _userRepository.GetUserByPhoneNumber(phonenumber);
        }

        public void AddMembership(string phonenumber,MembershipRequest membershipRequest, string subject)
        {
            var user = _userRepository.GetUserByPhoneNumber(phonenumber);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if(!user.IsOtpVerified)
            {
                throw new Exception("User is not verified");
            }

            var membership = new MembershipTransaction();
            membership.MembershipType = membershipRequest.Membership;
            membership.Amount = membershipRequest.Amount;
            membership.CreatedOn = DateTime.Now;
            membership.CreatedBy = subject;

            user.MembershipTransactions.Add(membership);
            user.UpdatedOn = DateTime.Now;
            user.UpdatedBy = subject;
            user.Membership = NexGen.Enum.UserType.Membership;

            switch (membershipRequest.Membership)
            {
                case NexGen.Enum.MembershipType.Silver:
                    user.Validity = DateOnly.FromDateTime(DateTime.Now.AddDays(membershipRequest.ValidityDays));
                    user.Balance = user.Balance + SILVER_HOURS*60;
                    break;
                case NexGen.Enum.MembershipType.Gold:
                    user.Validity = DateOnly.FromDateTime(DateTime.Now.AddDays(membershipRequest.ValidityDays));
                    user.Balance = user.Balance + GOLD_HOURS*60;
                    break;
                case NexGen.Enum.MembershipType.Platinum:
                    user.Validity = DateOnly.FromDateTime(DateTime.Now.AddDays(membershipRequest.ValidityDays));
                    user.Balance = user.Balance + PLATINUM_HOURS*60;
                    break;
            }
            _userRepository.UpdateUser(user);
        }

        public void StartPlayTime(string phonenumber,PlayTransactionRequest playTransactionRequest, string subject)
        {
            var user = _userRepository.GetUserByPhoneNumber(phonenumber);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!user.IsOtpVerified)
            {
                throw new Exception("User is not verified");
            }
            if (user.Balance <= 0 && user.Membership == NexGen.Enum.UserType.Membership)
            {
                throw new Exception("No balance");
            }

            var playTransaction = new PlayTransaction();
            playTransaction.Game = playTransactionRequest.Game;
            playTransaction.RequestedTime = playTransactionRequest.RequestedTime;
            playTransaction.PlayStation = playTransactionRequest.PlayStation;
            playTransaction.DiscountType = playTransactionRequest.DiscountType;
            playTransaction.Paid = false;
            playTransaction.StartTime = DateTime.Now;
            playTransaction.CreatedOn = DateTime.Now;
            playTransaction.CreatedBy = subject;
            
            _userRepository.UpdateUser(user);
        }

        public User GetAllTransactionsOfUser(string phonenumber)
        {
            var user = _userRepository.GetAllTransaction(phonenumber);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public User GetAllMembershipTransactionsOfUser(string phonenumber)
        {
            var user = _userRepository.GetAllMembershipTransaction(phonenumber);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }   
    }
}