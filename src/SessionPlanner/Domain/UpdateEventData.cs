using System;

namespace SessionPlanner.Domain
{
    public class UpdateEventData : ValueObject<UpdateEventData>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        protected override bool EqualsCore(UpdateEventData other)
        {
            return other.Name == Name && other.StartDate == StartDate && other.EndDate == other.EndDate;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Name, StartDate, EndDate);
        }
    }
}