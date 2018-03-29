using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SessionPlanner.Domain;

using Xunit;

namespace SessionPlanner.Tests.Domain
{
    public class BehavesLikeEvent
    {
        public BehavesLikeEvent()
        {
            Event = new Event("GOTOAmsterdam", new DateTime(2018, 6, 19), new DateTime(2018, 6, 21));
            Session = new Session("Build a Q&A Bot with DeepLearning4J", "Stuff", new[] { new Speaker("Willem Meints") });
        }

        protected Session Session { get; }
        protected Event Event { get; }
    }

    public class WhenCreatingAnEvent: BehavesLikeEvent
    {
        CreateEventCommand Command => new CreateEventCommand("Test event", DateTime.Now,DateTime.Now.AddDays(1));

        [Fact]
        public void NewEventIsCreated()
        {
            var operationResult = Event.Create(Command);
            operationResult.Result.Should().NotBeNull();
        }
    }

    public class WhenSubmittingSessions : BehavesLikeEvent
    {
        [Fact]
        public void NewSubmissionIsCreated()
        {
            Event.SubmitSessionProposal(Session, DateTime.Now);
            Event.Submissions.Count.Should().Be(1);
        }
    }

    public class WhenSessionIsRejected : BehavesLikeEvent
    {
        [Fact]
        public void SubmissionIsMarkedAsRejected()
        {
            Event.SubmitSessionProposal(Session, DateTime.Now);
            Event.RejectSessionProposal(Session);

            Event.Submissions.First().Status.Should().Be(SubmissionStatus.Rejected);
        }
    }

    public class WhenUnsubmittedSessionIsRejected: BehavesLikeEvent
    {
        [Fact]
        public void ValidationErrorsAreReturned()
        {
            var result = Event.RejectSessionProposal(Session);
            result.IsValid.Should().BeFalse();
        }
    }

    public class WhenSessionIsAccepted : BehavesLikeEvent
    {
        [Fact]
        public void SubmissionIsMarkedAsAccepted()
        {
            Event.SubmitSessionProposal(Session, DateTime.Now);
            Event.AcceptSessionProposal(Session);

            Event.Submissions.First().Status.Should().Be(SubmissionStatus.Accepted);
        }
    }

    public class WhenUnsubmittedSessionIsAccepted: BehavesLikeEvent
    {
        [Fact]
        public void ValidationErrorsAreReturned()
        {
            var result = Event.AcceptSessionProposal(Session);

        }
    }

    public class WhenSessionDetailsAreUpdated: BehavesLikeEvent
    {
        private UpdateEventCommand Command => new UpdateEventCommand(
                "Sample session", DateTime.Now.AddDays(1).Date, DateTime.Now.AddDays(2).Date);

        [Fact]
        public void StartDateIsUpdated()
        {
            Event.UpdateEventDetails(Command);
            Event.StartDate.Should().Be(Command.StartDate);
        }

        [Fact]
        public void EndDateIsUpdated()
        {
            Event.UpdateEventDetails(Command);
            Event.EndDate.Should().Be(Command.EndDate);
        }

        [Fact]
        public void NameIsUpdated()
        {
            Event.UpdateEventDetails(Command);
            Event.Name.Should().Be(Command.Name);
        }
    }
}