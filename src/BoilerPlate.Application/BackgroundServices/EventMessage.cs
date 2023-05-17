using Newtonsoft.Json;

namespace BoilerPlate.Application.BackgroundServices
{
    public class EventMessage
    {
        public string TopicName { get; set; }
        public string TableName { get; set; }
        public string Body { get; set; }

        public EventMessage() { }

        public EventMessage(string topicName, string tableName, string body)
        {
            TopicName = topicName;
            TableName = tableName;
            Body = body;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
