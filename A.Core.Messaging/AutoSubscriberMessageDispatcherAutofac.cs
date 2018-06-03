using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EasyNetQ.AutoSubscribe;

namespace A.Core.Messaging
{
    public class AutoSubscriberMessageDispatcherAutofac : IAutoSubscriberMessageDispatcher
    {
        public IContainer Container { get; set; }

        public AutoSubscriberMessageDispatcherAutofac(IContainer container)
        {
            Container = container;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : IConsume<TMessage>
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var consumer = scope.Resolve<TConsumer>();
                consumer.Consume(message);
            }
        }

        public async Task DispatchAsync<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : IConsumeAsync<TMessage>
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var consumer = scope.Resolve<TConsumer>();
                await consumer.Consume(message);
            }
        }
    }
}
