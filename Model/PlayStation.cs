using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexGen.Model
{
    public class PlayStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime ExpectedFreeTime { get; set; }
        public double RequestedTime { get; set; }
    }
}