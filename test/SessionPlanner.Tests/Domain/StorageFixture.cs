using System;
using SessionPlanner.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SessionPlanner.Tests.Domain
{
    public class StorageFixture : IDisposable
    {
        private readonly SessionPlannerDbContext _dataContext;
        private readonly ISessionRepository _sessionRepository;
        private readonly IEventRepository _eventRepository;

        public StorageFixture() 
        {
            var contextOptions = new DbContextOptionsBuilder<SessionPlannerDbContext>()
                .UseInMemoryDatabase("SessionPlanner_Test")
                .Options;

            _dataContext = new SessionPlannerDbContext(contextOptions);

            _eventRepository = new EventRepository(_dataContext);
            _sessionRepository = new SessionRepository(_dataContext);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public ISessionRepository SessionRepository => _sessionRepository;
        public IEventRepository EventRepository => _eventRepository;
    }
}