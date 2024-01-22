using System.Threading.Tasks;
using Domain.Common;

namespace Application.Common.Contracts
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}