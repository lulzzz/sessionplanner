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
        public void RejectSessionProposal(Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            var submission = Submissions.FirstOrDefault(sub => sub.Session.Equals(session));

            if (submission == null)
            {
                throw new InvalidOperationException("The provided session is not submitted for this event");
            }

            submission.Status = SubmissionStatus.Rejected;
        }

        /// <summary>
        /// Accepts a session proposal
        /// </summary>
        /// <param name="session">Session proposal to accept</param>
        public void AcceptSessionProposal(Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            var submission = Submissions.FirstOrDefault(sub => sub.Session.Equals(session));

            if (submission == null)
            {
                throw new InvalidOperationException("The provided session is not submitted for this event");
            }

            submission.Status = SubmissionStatus.Accepted;
        }
    }
}