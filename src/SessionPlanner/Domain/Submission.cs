using System;

namespace SessionPlanner.Domain
{
    public class Submission : Entity
    {
        private SubmissionStatus submitted;

        private Submission()
        {
            
        }

        public Submission(Session session, SubmissionStatus status, DateTime submissionDate)
        {
            Session = session;
            Status = status;
            Created = submissionDate;
        }

        public SubmissionStatus Status { get; set; }
        public Session Session { get; set; }
        public DateTime Created {get;set;}
    }
}