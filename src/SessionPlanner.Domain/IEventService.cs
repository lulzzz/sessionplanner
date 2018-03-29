using System.Threading.Tasks;

namespace SessionPlanner.Domain
{
    public interface IEventService
    {
        Task<OperationResult<Event>> CreateAsync(CreateEventCommand data);
    }
}