using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
         INotificationHandler<PedidoAtualizadoEvent>,
        INotificationHandler<PedidoItemAdiciondoEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoEvent>,
        INotificationHandler<PagamentoRealizadoEvent>,
        INotificationHandler<PagamentoRecusadoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHandler(IMediatorHandler mediatorHandler) => _mediatorHandler = mediatorHandler;

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken) =>
            Task.CompletedTask;

        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken) =>
            Task.CompletedTask;

        public Task Handle(PedidoItemAdiciondoEvent notification, CancellationToken cancellationToken) =>
            Task.CompletedTask;

        public async Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
            => await _mediatorHandler.EnviarComando(
                new CancelarProcessamentoPedidoCommand(notification.PedidoId, notification.ClienteId));

        public Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken) =>
            _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(notification.PedidoId, notification.ClienteId));

        public Task Handle(PagamentoRecusadoEvent notification, CancellationToken cancellationToken) =>
            _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(notification.PedidoId, notification.ClienteId));
    }
}
