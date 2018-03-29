namespace SessionPlanner.Domain
{
    public class Speaker : Entity
    {
        private Speaker()
        {

        }

        public Speaker(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Biography { get; set; }
        public string Photo { get; set; }
    }
}