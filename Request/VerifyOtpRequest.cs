using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextGen.Request
{
    public class VerifyOtpRequest
    {
        public string Username { get; set; }
        public double OTP { get; set; }
    }
}