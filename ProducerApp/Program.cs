using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ProducerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string routingName = "cute-route";
            string exchangeName = "shady-exchange";

            var factory = new ConnectionFactory() { 
                Uri = new Uri("amqp://guest:triforkyummy@localhost:5672") 
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            QueueProducer.Publish(channel, routingName, exchangeName);
        }
    }
}
