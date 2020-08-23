using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NerdStore.Core.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            if (ValidarComando(request) is false) return false;

            Pedido pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);

            PedidoItem pedidoItem = new PedidoItem(request.ProdutoId, request.Nome, request.Quantidade, request.ValorUnitario);

            if (pedido is null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(request.ClienteId);
                pedido.AdicionarItem(pedidoItem);

                _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                bool pedidoExistente = pedido.PedidoItemExistente(pedidoItem);

                pedido.AdicionarItem(pedidoItem);

                if (pedidoExistente)
                {
                    _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId));
                }
                else
                {
                    _pedidoRepository.AdicionarItem(pedidoItem);
                }
            }

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (ValidationFailure error in message.ValidationResult.Errors)
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }
    }
}
