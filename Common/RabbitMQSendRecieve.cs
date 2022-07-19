using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common
{
    public class RabbitProducer
    {
        public static void sendSingleMessage(RabbitMessage myMessage)
        {
            const string queue = "SingleMessage";

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            string JsonString = JsonSerializer.Serialize(myMessage);
            var body = Encoding.UTF8.GetBytes(JsonString);
            channel.BasicPublish("", queue, null, body);
        }
    }

    public class RabbitConsumer
    {
        public static void ConsumeMessages()
        {
            const string queue = "SingleMessage";

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (seneder, e) =>
            {
                var body = e.Body.ToArray();
                var myMessage = JsonSerializer.Deserialize<RabbitMessage>(body);
                Console.WriteLine(myMessage.Name);
            };

            channel.BasicConsume(queue, true, consumer);
            Console.ReadLine();
        }
    }
}
