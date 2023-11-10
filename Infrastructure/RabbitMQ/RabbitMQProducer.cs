using System.Text;
using Core.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ;

public class RabbitMQProducer : IRabbitMQProducer
{
    public async Task SendProductMessageAsync<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateChannel();

        channel.QueueDeclare("product", exclusive: false);

        var json = JsonConvert.SerializeObject(message);
        
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(exchange: "", routingKey: "product", body: body);
    }
}