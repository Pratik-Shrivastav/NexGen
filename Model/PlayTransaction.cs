using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Enum;
using NextGen.Enum;

namespace NexGen.Model
{
    public class PlayTransaction
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Game { get; set; }
        public double RequestedTime { get; set; }
        public double ActualTime { get; set; }
        public double Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Enum.PlayStation PlayStation { get; set; }
        public bool Paid { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }



        //For Navigation Property
        public int UserId { get; set; }
        public User User { get; set; }

    }
}