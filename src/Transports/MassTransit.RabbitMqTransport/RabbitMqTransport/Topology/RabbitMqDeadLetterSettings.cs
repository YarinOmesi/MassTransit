﻿namespace MassTransit.RabbitMqTransport.Topology
{
    using System.Collections.Generic;
    using Configuration;


    public class RabbitMqDeadLetterSettings : ExchangeQueueBindingConfigurator,
        DeadLetterSettings
    {

        public RabbitMqDeadLetterSettings(ReceiveSettings source, string name)
            : base(name, name, source.ExchangeType, source.Durable, source.AutoDelete)
        {
            foreach (KeyValuePair<string, object> argument in source.ExchangeArguments)
                SetExchangeArgument(argument.Key, argument.Value);

            foreach (KeyValuePair<string, object> argument in source.QueueArguments)
                SetQueueArgument(argument.Key, argument.Value);
        }

        public BrokerTopology GetBrokerTopology()
        {
            var builder = new PublishEndpointBrokerTopologyBuilder();

            builder.Exchange = builder.ExchangeDeclare(ExchangeName, ExchangeType, Durable, AutoDelete, ExchangeArguments);

            var queue = builder.QueueDeclare(QueueName, Durable, !QueueExpiration.HasValue && AutoDelete, false, QueueArguments);

            builder.QueueBind(builder.Exchange, queue, RoutingKey, BindingArguments);

            return builder.BuildBrokerTopology();
        }
    }
}
