using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Enum;

namespace NextGen.Request
{
    public class PlayTransactionRequest
    {
        public string Game { get; set; }
        public double RequestedTime { get; set; }
        public PlayStation PlayStation { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}