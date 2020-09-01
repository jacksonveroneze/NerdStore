using System.Threading.Tasks;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Messages.NerdStore.Core.Messages;

namespace NerdStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;

        Task<bool> EnviarComando<T>(T comando) where T : Command;

        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
