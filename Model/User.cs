using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Enum;

namespace NexGen.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public DateOnly Validity { get; set; }
        public UserType Membership { get; set; }
        public double OTP { get; set; }
        public DateTime OTPGeneratedAt { get; set; }
        public bool IsOtpVerified { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public ICollection<PlayTransaction> PlayTransactions { get; set; } = new List<PlayTransaction>();
        public ICollection<MembershipTransaction> MembershipTransactions { get; set; } = new List<MembershipTransaction>();



    }
}