using System.Collections.Generic;

namespace SessionPlanner.Domain
{
    /// <summary>
    /// A single session that might be submitted for a conference or meet up.
    /// </summary>
    public class Session : AggregateRoot
    {
        private Session()
        {
            Speakers = new List<Speaker>();
        }

        public Session(string title, string sessionAbstract, IEnumerable<Speaker> speakers)
        {
            Title = title;
            Abstract = sessionAbstract;
            Speakers = new List<Speaker>(speakers);
        }

        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Description { get; set; }
        public ICollection<Speaker> Speakers { get; set; }
    }
}