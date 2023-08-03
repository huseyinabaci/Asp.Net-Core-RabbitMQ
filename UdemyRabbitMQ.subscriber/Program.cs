using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://bsalcqdp:gBYeExJBjvUCkXYatp5BjpmkOAzZrBBX@gull.rmq.cloudamqp.com/bsalcqdp");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

var randomQueueName = channel.QueueDeclare().QueueName;

channel.QueueBind(randomQueueName, "logs-fanout", "", null);

channel.BasicQos(0, 1, false);

var consumer =  new EventingBasicConsumer(channel);

channel.BasicConsume(randomQueueName, false,consumer);

Console.WriteLine("Logları dinleniyor...");

consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500);

    Console.WriteLine("Gelen Mesaj:" + message);

    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();