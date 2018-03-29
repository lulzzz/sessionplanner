using System.Collections.Generic;
using System.Threading.Tasks;
using SessionPlanner.Domain;

namespace SessionPlanner.Repositories
{
    /// <summary>
    /// Interface for the event repository
    /// </summary>
    public interface IEventRepository: IRepository<Event>
    {
        /// <summary>
        /// Finds all events without their detailed properties
        /// </summary>
        /// <returns>Returns a list of event summaries</returns>
        Task<PagedResultSet<EventSummary>> FindAllAsync(int pageIndex);
    }
}