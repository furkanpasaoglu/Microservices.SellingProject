using AutoMapper;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;
        //private readonly ILogger<CreateOrderCommandHandler> logger;


        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _eventBus = eventBus;
            //this.logger = logger;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //logger.LogInformation("CreateOrderCommandHandler -> Handle method invoked");

            var addr = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);

            Order dbOrder = new(request.UserName,
                addr, request.CardTypeId, request.CardNumber, request.CardSecurityNumber, request.CardHolderName, request.CardExpiration, null);

            foreach (var orderItem in request.OrderItems)
            {
                dbOrder.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice, orderItem.PictureUrl, orderItem.Units);
            }

            await _orderRepository.AddAsync(dbOrder);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            //logger.LogInformation("CreateOrderCommandHandler -> dbOrder saved");

            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserName, dbOrder.Id);

            _eventBus.Publish(orderStartedIntegrationEvent);

            //logger.LogInformation("CreateOrderCommandHandler -> OrderStartedIntegrationEvent fired");

            return true;
        }
    }
}
