using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Application.BackgroundServices
{
    public class KafkaTopics
    {
        public string AuditTriggerTopic { get; set; }

        public IEnumerable<string> ToEnumerable()
        {
            return new List<string> 
            { 
                AuditTriggerTopic 
            };
        }
    }
}
