using System.Threading.Tasks;

namespace SessionPlanner.Domain
{
    public interface IEventService
    {
        Task<PagedResultSet<EventSummary>> FindAllAsync(int pageIndex);
        Task<Event> FindByIdAsync(int id);
        Task<ServiceOperationResult<Event>> CreateAsync(CreateEventData data);
        Task<ServiceOperationResult<Event>> UpdateAsync(int id, UpdateEventData data);
    }
}