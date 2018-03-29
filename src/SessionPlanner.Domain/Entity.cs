namespace SessionPlanner.Domain
{
    /// <summary>
    /// Inherit from this class to implement entities
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets the ID of the entity
        /// </summary>
        /// <returns>Returns the ID value for the entity</returns>
        public int Id { get; private set; }

        /// <summary>
        /// Compares this object to another object to find out if it's the same object
        /// </summary>
        /// <param name="obj">Object to compare with</param>
        /// <returns>Returns true when the object is the same; Otherwise false.</returns>
        public override bool Equals(object obj)
        {
            var otherEntity = obj as Entity;

            if (ReferenceEquals(otherEntity, null) || GetType() != obj.GetType())
            {
                return false;
            }

            return Id == otherEntity.Id || ReferenceEquals(otherEntity, this);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}