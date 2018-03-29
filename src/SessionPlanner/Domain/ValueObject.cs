namespace SessionPlanner.Domain
{
    /// <summary>
    /// Inherit from this class to model a value object
    /// </summary>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        /// <summary>
        /// Verifies that this object is the same as the other object.
        /// </summary>
        /// <param name="obj">Other object to compare</param>
        /// <returns>Returns true when this object matches the other object; Otherwise false.</returns>
        public override bool Equals(object obj)
        {
            var otherValueObject = obj as T;

            if (ReferenceEquals(otherValueObject, null))
            {
                return false;
            }

            return EqualsCore(otherValueObject);
        }

        /// <summary>
        /// Implement this method to create the structural comparison of the value object
        /// </summary>
        /// <param name="other">Object to verify equality with</param>
        /// <returns>Returns True when the objects are equal; Otherwise false.</returns>
        protected abstract bool EqualsCore(T other);

        /// <summary>
        /// Calculates the hash code for the value object
        /// </summary>
        /// <returns>Returns the hash code for the value object</returns>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        /// <summary>
        /// Implement this method to calculate the hash code for the object
        /// </summary>
        /// <returns>Returns the hash code for the object</returns>
        protected abstract int GetHashCodeCore();
    }
}