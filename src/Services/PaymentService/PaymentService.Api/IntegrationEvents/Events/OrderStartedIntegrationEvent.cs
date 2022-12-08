using EventBus.Base.Events;
using System;

namespace PaymentService.Api.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent: IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public OrderStartedIntegrationEvent()
        {

        }

        public OrderStartedIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
