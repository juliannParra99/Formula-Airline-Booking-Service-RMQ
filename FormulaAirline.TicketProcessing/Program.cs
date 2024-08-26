// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

/// Receiver/ consumer Connects to a RabbitMQ server and listens to the "bookings" queue for incoming messages. 
/// Upon receiving a message, it converts the message from a byte array to a string and 
/// logs it to the console, simulating the initiation of ticket processing.

Console.WriteLine("Welcome to the ticketing service");
var factory = new ConnectionFactory()
{

    HostName = "localhost",
    UserName = "user",
    Password = "mypass",
    VirtualHost = "/"
};

var conn = factory.CreateConnection();
using var channel = conn.CreateModel();

channel.QueueDeclare("bookings",
    durable: true,
    exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    //getting byte array
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"New ticket proccessing is initiated for {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();
