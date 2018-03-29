using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SessionPlanner.Domain;

namespace SessionPlanner.Infrastructure.Repositories
{
    /// <summary>
    /// Events repository
    /// </summary>
    public class EventRepository : Repository<Event>, IEventRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventRepository"/>
        /// </summary>
        /// <param name="dataContext"></param>
        /// <returns></returns>
        public EventRepository(SessionPlannerDbContext dataContext) : base(dataContext)
        {

        }

        /// <summary>
        /// Finds all events without their detailed properties
        /// </summary>
        /// <returns>Returns a list of event summaries</returns>
        public async Task<PagedResultSet<EventSummary>> FindAllAsync(int pageIndex)
        {
            var totalItemCount = await DataContext.Events.CountAsync();

            var records =  await DataContext.Events
                .OrderBy(x => x.Name)
                .Skip(pageIndex * 20).Take(20)
                .Select(x => new EventSummary(x.Id, x.Name, x.StartDate, x.EndDate))
                .ToListAsync();

            return new PagedResultSet<EventSummary>(pageIndex, 20, totalItemCount, records);
        }

        /// <summary>
        /// Finds a single event and its detailed properties
        /// </summary>
        /// <param name="id">ID of the event</param>
        /// <returns>Returns the found event</returns>
        public async override Task<Event> FindByIdAsync(int id)
        {
            return await DataContext.Events
                .Include(x => x.Submissions)
                .ThenInclude(x => x.Session)
                .ThenInclude(x => x.Speakers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}