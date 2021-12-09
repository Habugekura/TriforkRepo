using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerApp
{
    interface IMessage
    {
        int UniqueId { get; set; }
        string Message { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
