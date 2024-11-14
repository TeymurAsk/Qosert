using Confluent.Kafka;
using Newtonsoft.Json;

namespace ENS_API.Services
{
    public class KafkaProducerService
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
        }

        public async Task SendMessageAsync<T>(List<T> messages)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            foreach(var message in messages)
            {
                var jsonMessage = JsonConvert.SerializeObject(message);
                try
                {
                    var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = jsonMessage });
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
