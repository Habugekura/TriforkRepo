using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerApp
{
    class MessageTemplate : IMessage
    {
        public int UniqueId { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        
        public MessageTemplate(int uniqueId, string message, DateTime timeStamp)
        {
            this.UniqueId = uniqueId;
            this.Message = message;
            this.TimeStamp = timeStamp;
        }
       
    }
}
