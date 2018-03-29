using System;

namespace SessionPlanner.Domain
{
    public class EventSummary
    {
        public EventSummary(int id, string name, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get; set; }

        public string Name { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }
}