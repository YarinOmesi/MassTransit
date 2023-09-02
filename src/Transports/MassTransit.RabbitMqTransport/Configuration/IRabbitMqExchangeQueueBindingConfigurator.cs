namespace MassTransit
{
    public interface IRabbitMqExchangeQueueBindingConfigurator:
        IRabbitMqQueueConfigurator,
        IRabbitMqExchangeConfigurator,
        IRabbitMqBindingConfigurator
    {
    }
}
