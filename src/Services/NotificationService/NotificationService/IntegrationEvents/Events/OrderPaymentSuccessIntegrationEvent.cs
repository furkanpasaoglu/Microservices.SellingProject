﻿using EventBus.Base.Events;

namespace NotificationService.IntegrationEvents.Events
{
    public class OrderPaymentSuccessIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; }

        public OrderPaymentSuccessIntegrationEvent(Guid orderId) => OrderId = orderId;
    }
}
