﻿using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        // void SendMessage(BaseMessage baseMessage, string queueName);
        void SendMessage(BaseMessage baseMessage);
    }
}
