﻿
using Confluent.Kafka;
using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using Newtonsoft.Json;

namespace Ecommerce.ProductService.Kafka
{
    public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _ = ConsumerAsync("order-topic", stoppingToken);
            }, stoppingToken);
        }

        public async Task ConsumerAsync(string topic, CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "order-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);
                var order = JsonConvert.DeserializeObject<OrderModel>(consumeResult.Message.Value);
                using var scope = scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

                var product = await db.Products.FindAsync(order.ProductId);
                
                if(product != null)
                {
                    product.Quantity -= order.Qty;
                    await db.SaveChangesAsync();
                }
            }
            consumer.Close();
        }
    }
}
