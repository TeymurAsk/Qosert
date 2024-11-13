﻿using Confluent.Kafka;
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

        public async Task SendMessageAsync<T>(T message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var jsonMessage = JsonConvert.SerializeObject(message);

            try
            {
                var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = jsonMessage });
                Console.WriteLine($"Message sent to Kafka: {result.Value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send message: {ex.Message}");
            }
        }
    }
}