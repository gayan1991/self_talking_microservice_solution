using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Application.BackgroundServices
{
    public class KafkaConfig
    {
        public string Bootstrapservers { get; set; }
        public KafkaConsumerConfiguration Consumer { get; set; }
    }
}
