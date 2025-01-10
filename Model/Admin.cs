using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexGen.Model
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double OTP { get; set; }
        public DateTime OTPGeneratedAt { get; set; }
        public bool IsOtpVerified { get; set; }

    }
}