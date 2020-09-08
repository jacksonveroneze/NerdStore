using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler :
        INotificationHandler<ProdutoAbaixoEstoqueEvent>,
        INotificationHandler<PedidoIniciadoEvent>,
        INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatorHandler _mediatorHandler;
        public ProdutoEventHandler(IProdutoRepository produtoRepository, IEstoqueService estoqueService)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
        {
            Produto produto = await _produtoRepository.ObterPorId(notification.AggregateId);
        }

        public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
        {
            bool result = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

            if (result)
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(message.PedidoId, message.ClienteId, message.Total, message.ProdutosPedido, message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao));
            }
            else
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(message.PedidoId, message.ClienteId));
            }
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent notification, CancellationToken cancellationToken)
            => await _estoqueService.ReporListaProdutosPedido(notification.ProdutosPedido);
    }
}
