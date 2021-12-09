using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ProducerApp
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel, string routingName, string exchangeName)
        {
            //time to leave, deletes the message from the quque automatically. This ensures the message will be deleted if not send back, or saved in the database. 
            //All messages saved in the database are also deleted from the queue
            var ttl = new Dictionary<string, object>
            {
                {"message-ttl", 2000 }
            };
            channel.ExchangeDeclare(
                exchangeName,
                ExchangeType.Direct,
                arguments: ttl);
            //Add wutang name generator and timestamp

            while (true)
            {
                string messageText = WuTangClanNameGenerator();

                var message = new { Message = messageText, TimeStamp = DateTime.Now.ToString() };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchangeName, routingName, null, body);

                Thread.Sleep(500);
            }
        }

        private static string WuTangClanNameGenerator()
        {
            string wutangname = "";
            int rndNumber = new Random().Next(1,11);
            switch (rndNumber)
            {
                case 1:
                    wutangname = "Unstable Unicorn";
                    break;
                
                case 2:
                    wutangname = "Gentleman Commander";
                    break;

                case 3:
                    wutangname = "Sword Saint Samurai";
                    break;

                case 4:
                    wutangname = "Jamaica me crazy";
                    break;

                case 5:
                    wutangname = "The Rizza";
                    break;

                case 6:
                    wutangname = "Method Man";
                    break;

                case 7:
                    wutangname = "genius minatory";
                    break;

                case 8:
                    wutangname = "venturer Mad Mad";
                    break;

                case 9:
                    wutangname = "creator empathetic";
                    break;

                case 10:
                    wutangname = "Sir scrappy";
                    break;

            }
            return wutangname;
        }
    }
}
