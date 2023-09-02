namespace MassTransit.RabbitMqTransport.Configuration
{
    using System;
    using System.Collections.Generic;
    using Topology;


    public class ExchangeQueueBindingConfigurator : IRabbitMqExchangeQueueBindingConfigurator, EntitySettings
    {
        public RabbitMqQueueConfigurator QueueConfigurator { get; }
        public RabbitMqExchangeConfigurator ExchangeConfigurator { get; }

        public ExchangeQueueBindingConfigurator(string exchangeName, string queueName, string exchangeType, bool durable, bool autoDelete)
        {
            QueueConfigurator = new MyQueueConfigurator(queueName, durable, autoDelete);
            ExchangeConfigurator = new MyExchangeConfigurator(exchangeName, exchangeType, durable, autoDelete);

            BindingArguments = new Dictionary<string, object>();
            RoutingKey = "";
        }

        public IDictionary<string, object> BindingArguments { get; }

        public void SetBindingArgument(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                BindingArguments.Remove(key);
            else
                BindingArguments[key] = value;
        }

        public string RoutingKey { get; set; }

        public void AutoDeleteAfter(TimeSpan duration)
        {
            QueueConfigurator.AutoDelete = true;
            ExchangeConfigurator.AutoDelete = true;

            QueueConfigurator.QueueExpiration = duration;
        }

        public string QueueName
        {
            get => QueueConfigurator.QueueName;
            set => QueueConfigurator.QueueName = value;
        }

        public IDictionary<string, object> QueueArguments => QueueConfigurator.QueueArguments;

        public bool Exclusive
        {
            get => QueueConfigurator.Exclusive;
            set => QueueConfigurator.Exclusive = value;
        }

        public bool Lazy
        {
            set => QueueConfigurator.Lazy = value;
        }

        public TimeSpan? QueueExpiration
        {
            get => QueueConfigurator.QueueExpiration;
            set => QueueConfigurator.QueueExpiration = value;
        }

        public bool SingleActiveConsumer
        {
            set => QueueConfigurator.SingleActiveConsumer = value;
        }

        public void SetQueueArgument(string key, object value) => QueueConfigurator.SetQueueArgument(key, value);
        public void SetQueueArgument(string key, TimeSpan value) => QueueConfigurator.SetQueueArgument(key, value);
        public void EnablePriority(byte maxPriority) => QueueConfigurator.EnablePriority(maxPriority);
        public void SetQuorumQueue(int? replicationFactor = default) => QueueConfigurator.SetQuorumQueue(replicationFactor);

        public bool Durable
        {
            get => QueueConfigurator.Durable && ExchangeConfigurator.Durable;
            set => QueueConfigurator.Durable = ExchangeConfigurator.Durable = value;
        }

        public bool AutoDelete
        {
            get => QueueConfigurator.AutoDelete && ExchangeConfigurator.AutoDelete;
            set => QueueConfigurator.AutoDelete = ExchangeConfigurator.AutoDelete = value;
        }

        public string ExchangeType
        {
            get => ExchangeConfigurator.ExchangeType;
            set => ExchangeConfigurator.ExchangeType = value;
        }

        public void SetExchangeArgument(string key, object value) => ExchangeConfigurator.SetExchangeArgument(key, value);

        public void SetExchangeArgument(string key, TimeSpan value) => ExchangeConfigurator.SetExchangeArgument(key, value);

        public IDictionary<string, object> ExchangeArguments => ExchangeConfigurator.ExchangeArguments;

        public string ExchangeName
        {
            get => ExchangeConfigurator.ExchangeName;
            set => ExchangeConfigurator.ExchangeName = value;
        }

        private class MyQueueConfigurator : RabbitMqQueueConfigurator
        {
            public MyQueueConfigurator(string queueName, bool durable, bool autoDelete)
                : base(queueName, durable, autoDelete)
            {
            }
        }


        private class MyExchangeConfigurator : RabbitMqExchangeConfigurator
        {
            public MyExchangeConfigurator(string exchangeName, string exchangeType, bool durable = true, bool autoDelete = false)
                : base(exchangeName, exchangeType, durable, autoDelete)
            {
            }

            public MyExchangeConfigurator(Exchange source)
                : base(source)
            {
            }
        }
    }
}
