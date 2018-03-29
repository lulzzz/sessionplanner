using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SessionPlanner.Domain;
using SessionPlanner.Repositories;
using Xunit;

namespace SessionPlanner.Tests.Domain
{
    public class BehavesLikeSession : IClassFixture<StorageFixture>
    {
        public BehavesLikeSession(StorageFixture storage)
        {
            Storage = storage;

            Event = new Event("GOTOAmsterdam", new DateTime(2018, 6, 19), new DateTime(2018, 6, 21));
            Session = new Session("Build a Q&A Bot with DeepLearning4J", "Stuff", new[] { new Speaker("Willem Meints") });
        }

        protected StorageFixture Storage { get; }

        protected Session Session { get; }
        protected Event Event { get; }
    }

    public class WhenSubmittingSessions : BehavesLikeSession
    {

        public WhenSubmittingSessions(StorageFixture storage) : base(storage)
        {

        }

        [Fact]
        public async Task NewSubmissionIsCreated()
        {
            await Storage.EventRepository.InsertAsync(Event);
            await Storage.SessionRepository.InsertAsync(Session);

            Event.SubmitSessionProposal(Session, DateTime.Now);

            Event.Submissions.Count.Should().Be(1);
        }
    }

    public class WhenSessionIsRejected : BehavesLikeSession
    {
        public WhenSessionIsRejected(StorageFixture storage) : base(storage)
        {

        }

        [Fact]
        public async Task SubmissionIsMarkedAsRejected()
        {
            await Storage.EventRepository.InsertAsync(Event);
            await Storage.SessionRepository.InsertAsync(Session);

            Event.SubmitSessionProposal(Session, DateTime.Now);
            Event.RejectSessionProposal(Session);

            Event.Submissions.First().Status.Should().Be(SubmissionStatus.Rejected);
        }
    }

    public class WhenSessionIsAccepted : BehavesLikeSession
    {
        public WhenSessionIsAccepted(StorageFixture storage) : base(storage)
        {

        }

        [Fact]
        public async Task SubmissionIsMarkedAsAccepted()
        {
            await Storage.EventRepository.InsertAsync(Event);
            await Storage.SessionRepository.InsertAsync(Session);

            Event.SubmitSessionProposal(Session, DateTime.Now);
            Event.AcceptSessionProposal(Session);

            Event.Submissions.First().Status.Should().Be(SubmissionStatus.Accepted);
        }
    }
}