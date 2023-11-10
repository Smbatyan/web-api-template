namespace Core.RabbitMQ;

public interface IRabbitMQProducer
{
    public Task SendProductMessageAsync<T>(T message);
}