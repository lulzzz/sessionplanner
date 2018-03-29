using SessionPlanner.Domain;

namespace SessionPlanner.Infrastructure.Repositories
{
    public class SessionRepository: Repository<Session>, ISessionRepository
    {
        public SessionRepository(SessionPlannerDbContext dataContext): base(dataContext)
        {
            
        }
    }
}