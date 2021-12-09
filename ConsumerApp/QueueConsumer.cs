using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Data.Sqlite;

namespace ConsumerApp
{
    class QueueConsumer
    {
        public static void Consume(IModel channel, string queueName, string exchangeName, string routingKey)
        {
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

            channel.QueueDeclare(
                queueName,
                durable: true,
                exclusive: false,
                autoDelete: true,
                arguments: null);

            channel.QueueBind(queueName, exchangeName, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                
                
                var body = e.Body.ToArray();
                
                var message = Encoding.UTF8.GetString(body);
                var msg = JsonConvert.DeserializeObject<MessageTemplate>(message);

                int check = AddMessageToDatabase(msg);

                if (check == 2)
                {
                    //Sends message back if seconds are odd.
                    var reQueueMessage = new {Message = msg.Message, TimeStamp = DateTime.Now.ToString() };
                    var reQueueBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(reQueueMessage));

                    channel.BasicPublish("", queueName,null, reQueueBody);
                    Console.WriteLine("SENT BACK TO QUEUE ODD");
                }
                else if (check == 1)
                {
                    Tuple<string, string> tmpTuple = new Tuple<string, string>(msg.Message, msg.TimeStamp.ToString());
                    TotallyARealDataBase.RealDataSet.Add(tmpTuple);

                    Console.WriteLine(msg.Message);
                    Console.WriteLine(msg.TimeStamp.ToString());

                    //save to dtb
                }
                else
                {
                    //do nothing queue will pop the message after 2 seconds. And it is getting consumed
                }

            };
            

            
            channel.BasicConsume(queueName, true, consumer);
            Console.ReadLine();
        }

        private static int AddMessageToDatabase(MessageTemplate message)
        {
            DateTime currentTime = DateTime.Now;
            
            if (message.TimeStamp.AddMinutes(1) < currentTime)
            {
                //dispose
                Console.WriteLine("DISPOSED");
                return 0;
            }
            else if (message.TimeStamp.Second % 2 == 0)
            {
                //Number is even
                return 1;
            }
            else
            {
                //Send back to queue
                return 2;
            }
        }

       

    }
}
