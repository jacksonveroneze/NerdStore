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

        public async Task Handle(PedidoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarListaProdutosPedido(notification.ProdutosPedido);
        }
    }
}
