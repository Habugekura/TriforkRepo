using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsumerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = "cute-queue";
            string exchangeName = "shady-exchange";
            string routingKey = "cute-route";
            
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:triforkyummy@localhost:5672")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            QueueConsumer.Consume(channel, queueName, exchangeName, routingKey);
        }
    }
}
