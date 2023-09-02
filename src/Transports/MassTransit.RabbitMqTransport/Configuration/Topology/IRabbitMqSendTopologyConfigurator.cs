namespace MassTransit
{
    using System;


    public interface IRabbitMqSendTopologyConfigurator :
        ISendTopologyConfigurator,
        IRabbitMqSendTopology
    {
        Action<IRabbitMqExchangeQueueBindingConfigurator> ConfigureErrorSettings { set; }
        Action<IRabbitMqExchangeQueueBindingConfigurator> ConfigureDeadLetterSettings { set; }
    }
}
