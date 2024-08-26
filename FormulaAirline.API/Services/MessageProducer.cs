using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace FormulaAirline.API.Services
{
    /// Sends a serialized message to the specified RabbitMQ queue ("bookings") by establishing a connection, 
    /// creating a channel, declaring the queue, and publishing the message as a JSON-encoded byte array <summary>
    /// Sends a serialized message to the specified RabbitMQ queue ("bookings") by establishing a connection, 
    //is the producer, which sends the message to the queue.
    public class MessageProducer : IMessageProducer
    {


        public void SendingMessage<T>(T message)
        {
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

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "bookings", null, body: body);

        }
    }
}