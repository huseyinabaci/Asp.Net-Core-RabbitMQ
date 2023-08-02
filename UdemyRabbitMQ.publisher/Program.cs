using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://bsalcqdp:gBYeExJBjvUCkXYatp5BjpmkOAzZrBBX@gull.rmq.cloudamqp.com/bsalcqdp");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.QueueDeclare("hello-queue", true, false, false);

string message = "hello world";

var messageBody = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

Console.WriteLine("Message Gönderilmiştir");

Console.ReadLine();