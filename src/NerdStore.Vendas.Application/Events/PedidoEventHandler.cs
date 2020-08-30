using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoRascunhoIniciadoEvent>,
         INotificationHandler<PedidoAtualizadoEvent>,
        INotificationHandler<PedidoItemAdiciondoEvent>
    {
        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken) =>
            Task.CompletedTask;

        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken) =>
            Task.CompletedTask;

        public Task Handle(PedidoItemAdiciondoEvent notification, CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}
