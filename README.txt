The Web API "Fibonacci API" exposes two API's thru http:/localhost:53195/api/FibonacciRange and http:/localhost:53195/api/Fibonacci
The Postman utility is used to test the Web API's. Each of these API posts the messsage to RabbitMQ Queue FibonacciTopic_Queue
The WEB API and its raw JSON data is given below.

http:/localhost:53195/api/FibonacciRange
{
  "StartPosition": 3,
  "EndPosition": 10,
   "result": ""
}

http://localhost:53195/api/Fibonacci

{
  "Position": 99,
  "res": 0,
}


And the PostMan recieves http response. Also Web API posts this message onto the Message Queue as well.
Which can be consumed by other service .
At the present cache is used to store the generated series.
But there is memory issue if you try with 500,000th position.
This need to be fixed.
The asked range for 4000 to 5000 steps is calculated well and added unit test for it.
