using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Application.BackgroundServices
{
    public class KafkaConsumerConfiguration
    {
        public string GroupID { get; set; }
        public string AutoOffSetReset { get; set; }
        public bool EnableAutoOffsetStore { get; set; }
        public bool EnableAutoCommit { get; set; }
        public int PollUntervalMilliseconds { get; set; }
        public string Topics { get; set; }
    }
}
