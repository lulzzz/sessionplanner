using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SessionPlanner.Domain;

namespace SessionPlanner.Infrastructure.Repositories
{
    /// <summary>
    /// Inherit from this repository to create a new repository
    /// </summary>
    public abstract class Repository<T> : IRepository<T>
        where T : AggregateRoot
    {
        private readonly SessionPlannerDbContext _dataContext;

        protected Repository(SessionPlannerDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Finds a single entity by its ID
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <returns>Returns the found entity</returns>
        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Inserts a new instance of the entity in the database
        /// </summary>
        /// <param name="instance">Instance to store</param>
        /// <returns>Returns the stored entity</returns>

        public async Task<T> InsertAsync(T instance)
        {
            await _dataContext.AddAsync(instance);
            await _dataContext.SaveChangesAsync();

            return instance;
        }


        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="instance">Instance of the entity to update</param>
        /// <returns>Returns the updated entity</returns>
        public async Task<T> UpdateAsync(T instance)
        {
            _dataContext.Update(instance);
            await _dataContext.SaveChangesAsync();

            return instance;
        }

        /// <summary>
        /// Deletes an existing entity from the database
        /// </summary>
        /// <param name="instance">Entity to remove</param>
        /// <returns></returns>
        public async Task DeleteAsync(T instance)
        {
            _dataContext.Remove(instance);
            await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the data context for the repository
        /// </summary>
        protected SessionPlannerDbContext DataContext => _dataContext;
    }
}