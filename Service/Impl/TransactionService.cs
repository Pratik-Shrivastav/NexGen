using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Model;
using NextGen.Enum;
using NextGen.Repository;

namespace NextGen.Service.Impl
{
    public class TransactionService : ITransactionService
    {

        private readonly IUserRepository _userRepository;
        private const double RATE = 120;
        private const double INSTA_DISCOUNT = 0.9;
        private const double REFERRAL_DISCOUNT = 0.8;



        public TransactionService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<PlayTransaction> GetAllTransaction(){
            return _userRepository.GetAllPlayTransactions();
        }
        public PlayTransaction EndPlayTransaction(int id,string subject)
        {
            var playTransaction = _userRepository.GetTransactionById(id);
            playTransaction.EndTime = DateTime.Now;
            playTransaction.ActualTime = (playTransaction.EndTime - playTransaction.StartTime).TotalMinutes;
            playTransaction.UpdatedOn = DateTime.Now;
            playTransaction.UpdatedBy = subject;
            double totalAmount = 0.0;
            CalculatePlay(playTransaction.User.Phone, playTransaction.ActualTime, ref totalAmount);
            playTransaction.Amount = totalAmount;
            if (playTransaction.Amount == 0.0)
            {
                playTransaction.Paid = true;
            }
            if(playTransaction.DiscountType == NexGen.Enum.DiscountType.InstaStory){
                playTransaction.Amount -= playTransaction.Amount * INSTA_DISCOUNT;
            }
            else if(playTransaction.DiscountType == NexGen.Enum.DiscountType.Referal){
                playTransaction.Amount -= playTransaction.Amount * REFERRAL_DISCOUNT;

            }
            _userRepository.UpdatePlayTransaction(playTransaction);
            _userRepository.SaveChanges();
            return playTransaction;
        }

        public void CalculatePlay(string phonenumber, double time, ref double totalAmount)
        {
            var user = _userRepository.GetUserByPhoneNumber(phonenumber);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            switch (user.Membership)
            {
                case NexGen.Enum.UserType.Normal:
                    totalAmount = CalculateForNomal(user, time);
                    break;
                case NexGen.Enum.UserType.Membership:
                    totalAmount = CalculateForMember(user, time);
                    break;
            }
        }

        public static double CalculateForNomal(User user, double time)
        {
            var hours = Math.Floor(time / 60);
            var minutes = Math.Floor(time % 60);
            if (minutes > 15 && minutes < 30)
            {
                hours += 0.5;
            }
            else if (minutes > 30)
            {
                hours++;
            }
            return hours * RATE;
        }

        public static double CalculateForMember(User user, double time)
        {
            if (user.Balance == time)
            {
                user.Balance = 0;
                user.Membership = NexGen.Enum.UserType.Normal;
                return 0.0;
            }
            else if (user.Balance > time)
            {
                user.Balance = user.Balance - time;
                return 0.0;

            }
            user.Balance = 0;
            user.Membership = NexGen.Enum.UserType.Normal;
            double calculatedAmount = CalculateForNomal(user, time - user.Balance);
            return calculatedAmount;
        }

        public void Payment(int transactionId, PaymentMethod paymentMethod, string subject){
            var transaction = _userRepository.GetTransactionById(transactionId);
            transaction.PaymentMethod = paymentMethod;
            transaction.Paid = true;
            transaction.UpdatedOn = DateTime.Now;
            transaction.UpdatedBy = subject;
            _userRepository.UpdatePlayTransaction(transaction);
        }

    }
}