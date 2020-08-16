using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Events
{
    class ProdutoAbaixoEstoqueEvent : DomainEvent
    {
        public int QuantidadeRestante { get; set; }

        public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante) : base(aggregateId)
            => QuantidadeRestante = quantidadeRestante;
    }
}
