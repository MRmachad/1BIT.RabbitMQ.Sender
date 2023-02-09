using UMBIT.RabbitMQ.Sender;
using RabbitMQ.Client;
using System.Runtime.Serialization.Formatters.Binary;

var factory = new ConnectionFactory { HostName = "127.0.0.1"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

const string message = "Hello World!";
var t = new Teste() { name = "teste", teste = "teste" };

BinaryFormatter bf = new BinaryFormatter();
using (MemoryStream ms = new MemoryStream())
{

    bf.Serialize(ms, t);


    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "hello",
                         basicProperties: null,
                         body: ms.ToArray());

    Console.WriteLine($" [x] Sent {message}");

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}



