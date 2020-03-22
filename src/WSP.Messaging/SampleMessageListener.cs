using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WSP.Utils;

namespace WSP.Messaging
{
    public class SampleMessageListener
    {
        public void Listen()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
                var connection = factory.CreateConnection();

                var channel = connection.CreateModel();
                {
                    channel.QueueDeclare(queue: "DocumentQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume(queue: "DocumentQueue",
                                         autoAck: true,
                                         consumer: consumer);

                }
            }
            catch ( Exception ex)
            {

            }
            
        }

        private static async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var body = @event.Body;
            var message = Encoding.UTF8.GetString(body);

            SampleDocumentJob documentJob = JsonConvert.DeserializeObject<SampleDocumentJob>(message);
            JobQueue.Instance.Enqueue(documentJob);

            await Task.Yield();
        }
    }
}
