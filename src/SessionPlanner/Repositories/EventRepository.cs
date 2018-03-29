using SessionPlanner.Domain;

namespace SessionPlanner.Repositories
{
    public class EventRepository: Repository<Event>, IEventRepository
    {
        public EventRepository(SessionPlannerDbContext dataContext):base(dataContext)
        {

        }
    }
}