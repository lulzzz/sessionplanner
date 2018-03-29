using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SessionPlanner.Domain
{
    public class Event : AggregateRoot
    {
        private Event()
        {
            Submissions = new Collection<Submission>();
        }

        public Event(string name, DateTime startDate, DateTime endDate) : this()
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }

        public Collection<Submission> Submissions { get; set; }

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static OperationResult<Event> Create(CreateEventCommand command)
        {
            var result = new OperationResult<Event>();

            if (command.StartDate > command.EndDate)
            {
                result.AddValidationError("EndDate", "EndDate should be afer StartDate");
            }

            if (String.IsNullOrEmpty(command.Name))
            {
                result.AddValidationError("Name", "Name is required");
            }

            if(result.IsValid)
            {
                var newEvent = new Event(command.Name,command.StartDate,command.EndDate);
                result.Complete(newEvent);
            }

            return result;
        }

        /// <summary>
        /// Submits a new session proposal for the event
        /// </summary>
        /// <param name="session">Session to submit</param>
        /// <param name="submissionDate">The date the submission was made</param>
        public void SubmitSessionProposal(Session session, DateTime submissionDate)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            Submissions.Add(new Submission(session, SubmissionStatus.Submitted, submissionDate));
        }

        /// <summary>
        /// Rejects a session proposal
        /// </summary>
        /// <param name="session">Session to reject</param>
        public OperationResult RejectSessionProposal(Session session)
        {
            var result = new OperationResult();

            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            var submission = Submissions.FirstOrDefault(sub => sub.Session.Equals(session));

            if (submission == null)
            {
                result.AddValidationError("", "The provided session is not submitted for this event");
            }

            if(result.IsValid)
            {
                submission.Status = SubmissionStatus.Rejected;
            }
            
            return result;
        }

        /// <summary>
        /// Accepts a session proposal
        /// </summary>
        /// <param name="session">Session proposal to accept</param>
        public OperationResult AcceptSessionProposal(Session session)
        {
            var result = new OperationResult();

            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            var submission = Submissions.FirstOrDefault(sub => sub.Session.Equals(session));

            if (submission == null)
            {
                result.AddValidationError("", "The provided session is not submitted for this event");
            }

            if(result.IsValid)
            {
                submission.Status = SubmissionStatus.Accepted;
            }
            
            return result;
        }

        /// <summary>
        /// Updates the event details
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OperationResult UpdateEventDetails(UpdateEventCommand command)
        {
            var result = new OperationResult();

            if (command.StartDate > command.EndDate)
            {
                result.AddValidationError("EndDate", "EndDate must be after StartDate");
            }

            if (string.IsNullOrEmpty(command.Name))
            {
                result.AddValidationError("Name", "Name is required");
            }

            if (result.IsValid)
            {
                Name = command.Name;
                StartDate = command.StartDate;
                EndDate = command.EndDate;
            }

            return result;
        }
    }
}