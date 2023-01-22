# RabbitMQ in NET Core

A queue or a message broker provides the much-needed decoupling between microservices. And it prevents get into the anti-pattern of the distributed monolith.

You can think of a message broker like a post office. Its main responsibility is to broker messages between publishers and subscribers. 

Once a message is received by a message broker from a producer, it routes the message to a subscriber. The message broker pattern is one of the most useful patterns when it comes to decoupling microservices.

This project is based on this youtube course :

https://www.youtube.com/watch?v=w84uFSwulBI&list=PLXCqSX1D2fd_6bna8uP4-p3Y8wZxyB75G&index=3

## Producer project

1. Install **RabbitMQ Docker** image
```
docker run -d --hostname my-rabbit  --name ecomm-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

2. Go to http://localhost:15672 and give credentials guest / guest

<img src="/pictures/rabbitmq.png" title="rabbitmq on creation"  width="400">
<img src="/pictures/rabbitmq2.png" title="rabbitmq on creation"  width="800">
```

3. install packages
```
RabbitMQ.Client
```


## Consumer project

1. install packages
```
RabbitMQ.Client
```

2. Launch both *Consumer* and *Producer* and see the result
<img src="/pictures/message.png" title="message"  width="800">
```

3. Launch both *Consumer* and multiple instances of *Producer* and see the result
<img src="/pictures/multi_producer_consumer.png" title="mutliple instances"  width="800">
```

3. Launch both *Producer* and multiple instances of *Consumer* and see the result
<img src="/pictures/multiple_consumer.png" title="multiple consumer"  width="800">


## Direct Exchange

1. Direct Exchange Consumer
<img src="/pictures/direct_exchange_consumer.png" title="direct exchange consumer"  width="800">
<img src="/pictures/direct_exchange_consumer2.png" title="direct exchange consumer"  width="400">

2. Direct Exchange Producer
<img src="/pictures/direct_exchange_producer.png" title="direct exchange producer"  width="400">

3. Prefetch count
<img src="/pictures/prefetch_count.png" title="prefetch count"  width="800">





https://github.com/choudhurynirjhar/rabbitmq-demo