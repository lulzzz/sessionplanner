using System.Threading.Tasks;
using SessionPlanner.Domain;

namespace SessionPlanner.Infrastructure.Repositories
{
    /// <summary>
    /// Implement this interface to create a new repository
    /// </summary>
    public interface IRepository<T> where T : AggregateRoot
    {
        /// <summary>
        /// Finds a single entity by its ID
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <returns>Returns the found entity</returns>
        Task<T> FindByIdAsync(int id);

        /// <summary>
        /// Inserts a new instance of the entity in the database
        /// </summary>
        /// <param name="instance">Instance to store</param>
        /// <returns>Returns the stored entity</returns>
        Task<T> InsertAsync(T instance);

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="instance">Instance of the entity to update</param>
        /// <returns>Returns the updated entity</returns>
        Task<T> UpdateAsync(T instance);

        /// <summary>
        /// Deletes an existing entity from the database
        /// </summary>
        /// <param name="instance">Entity to remove</param>
        /// <returns></returns>
        Task DeleteAsync(T instance);
    }
}