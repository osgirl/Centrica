using System;
using System.Collections.Generic;
using FibancciService.Models;
using RabbitMQ.Client;

namespace FibancciService.RabbitMQ
{
    public class RabbitMQClient
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "Topic_Exchange";
       private const string FibonacciQueueName = "FibonacciTopic_Queue";      
        private const string AllQueueName = "AllTopic_Queue";

        public RabbitMQClient()
        {
            CreateConnection();
        }

        private static void CreateConnection()
        {
            //_factory = new ConnectionFactory
            //{
            //    HostName = "localhost", UserName = "guest", Password = "guest"
            //};

            ConnectionFactory _factory = new ConnectionFactory();
            //_factory.UserName = "guest";
            //_factory.Password = "guest";
            //_factory.VirtualHost = "/";
            //_factory.Protocol = Protocols.DefaultProtocol;
            _factory.HostName = "localhost";
            _factory.Port = AmqpTcpEndpoint.UseDefaultPort;
          

            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "topic");

            _model.QueueDeclare(FibonacciQueueName, true, false, false, null);
           
            _model.QueueDeclare(AllQueueName, true, false, false, null);

            _model.QueueBind(FibonacciQueueName, ExchangeName, "fibonacci.series");

            _model.QueueBind(AllQueueName, ExchangeName, "fibonacci.*");
        }

        public void Close()
        {
            _connection.Close();
        }

        
        public void SendMessage(byte[] message, string routingKey)
        {
            _model.BasicPublish(ExchangeName, routingKey, null, message);
        }

        public void SendFibonacciSeries(FibonacciRange req)
        {
            SendMessage(req.Serialize(), "fibonacci.series");
        }

        public void SendFibonacciNum(Fibonacci req)
        {
            SendMessage(req.Serialize(), "fibonacci.series");
        }
    }  
}
