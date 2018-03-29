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

            if (ReferenceEquals(otherEntity, null) || GetType() != obj.GetType() || Id == 0 || otherEntity.Id == 0)
            {
                return false;
            }

            return Id == otherEntity.Id || ReferenceEquals(otherEntity, this);
        }

        /// <summary>
        /// Gets the hash code for the object
        /// </summary>
        /// <returns>Returns the hash code for the object</returns>
        public override int GetHashCode()
        {
            return $"{GetType().FullName}-{Id}".GetHashCode();
        }
    }
}