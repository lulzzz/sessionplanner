using Microsoft.EntityFrameworkCore;
using SessionPlanner.Domain;
using SessionPlanner.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace SessionPlanner.Infrastructure.Tests
{
    public class BehavesLikeEventRepository
    {
        protected SessionPlannerDbContext DataContext { get;}
        protected IEventRepository EventRepository { get;}

        public BehavesLikeEventRepository()
        {
            var options = new DbContextOptionsBuilder<SessionPlannerDbContext>()
                .UseInMemoryDatabase("SessionPlanner_Test")
                .Options;

            DataContext = new SessionPlannerDbContext(options);

            EventRepository = new EventRepository(DataContext);
        }
    }

    public class WhenRetrievingEvents: BehavesLikeEventRepository
    {
        public WhenRetrievingEvents():base()
        {
            IEnumerable<Event> events = Enumerable.Range(1, 40)
                .Select(x => new Event(
                    $"Event {x}", 
                    DateTime.Now.AddDays(x * 40), 
                    DateTime.Now.AddDays(x * 40 + 1)));

            DataContext.AddRange(events);
            DataContext.SaveChanges();
        }

        [Fact]
        public async Task RetrievesSinglePageOf20Records()
        {
            var results = await EventRepository.FindAllAsync(0);
            results.Records.Count().Should().Be(20);
        }

        [Fact]
        public async Task ReturnsCorrectPageIndex()
        {
            var results = await EventRepository.FindAllAsync(1);
            results.PageIndex.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task ReturnsTotalNumberOfRecords()
        {
            var results = await EventRepository.FindAllAsync(1);
            results.TotalRecords.Should().BeGreaterOrEqualTo(40);
        }
    }

    public class WhenAskedForSingleEvent: BehavesLikeEventRepository
    {
        public WhenAskedForSingleEvent(): base()
        {
            DataContext.Add(new Event("Test event", DateTime.Now, DateTime.Now.AddDays(1)));
            DataContext.SaveChanges();
        }

        [Fact]
        public async Task ReturnsExistingEvent()
        {
            var evt = DataContext.Events.Last();

            var foundEvent = await EventRepository.FindByIdAsync(evt.Id);
            foundEvent.Id.Should().Be(evt.Id);
        }
    }
}
