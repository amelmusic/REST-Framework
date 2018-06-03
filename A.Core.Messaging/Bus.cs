using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Interceptors;

namespace A.Core.Messaging
{
    public partial class Bus : IBus
    {
        // ReSharper disable once InconsistentNaming
        public EasyNetQ.IBus RabbitMQBus { get; set; }
        public Bus(EasyNetQ.IBus bus)
        {
            RabbitMQBus = bus;
        }

        [Log]
        public virtual async Task Publish(dynamic message)
        {
            await RabbitMQBus.PublishAsync(message);
        }
    }
}
