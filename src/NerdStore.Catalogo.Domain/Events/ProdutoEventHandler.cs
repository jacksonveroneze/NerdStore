using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NerdStore.Catalogo.Domain.Events
{
    class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository)
            => _produtoRepository = produtoRepository;

        public async Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
        {
            Produto produto = await _produtoRepository.ObterPorId(notification.AggregateId);

            // Fazer alguma coisa
            // Enviar e-mail, etc.
        }
    }
}
