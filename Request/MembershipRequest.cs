using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Enum;

namespace NextGen.Request
{
    public class MembershipRequest
    {
        public MembershipType Membership { get; set; }
        public int ValidityDays { get; set; }
        public double Amount { get; set; }
    }
}