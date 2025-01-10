using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexGen.Enum;

namespace NexGen.Model
{
    public class MembershipTransaction
    {
        public int Id { get; set; }
        public MembershipType MembershipType { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}