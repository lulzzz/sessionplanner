using System.Threading.Tasks;
using SessionPlanner.Repositories;

namespace SessionPlanner.Domain
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="data">Data for the new event</param>
        /// <returns>Returns the result of the operation</returns>
        public async Task<ServiceOperationResult<Event>> CreateAsync(CreateEventData data)
        {
            var operationResult = new ServiceOperationResult<Event>();

            if (data.StartDate > data.EndDate)
            {
                operationResult.AddValidationError("EndDate", "EndDate must be after StartDate");
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                operationResult.AddValidationError("Name", "Name is required");
            }

            if (operationResult.IsValid)
            {
                var newEvent = new Event(data.Name, data.StartDate, data.EndDate);
                var createdEvent = await _eventRepository.InsertAsync(newEvent);

                operationResult.Complete(createdEvent);
            }


            return operationResult;
        }

        /// <summary>
        /// Updates an event
        /// </summary>
        /// <param name="id">ID of the event</param>
        /// <param name="data">Data to update</param>
        /// <returns>Returns operation result</returns>
        public async Task<ServiceOperationResult<Event>> UpdateAsync(int id, UpdateEventData data)
        {
            var operationResult = new ServiceOperationResult<Event>();

            if (data.StartDate > data.EndDate)
            {
                operationResult.AddValidationError("EndDate", "EndDate must be after StartDate");
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                operationResult.AddValidationError("Name", "Name is required");
            }

            if (operationResult.IsValid)
            {
                var foundEvent = await _eventRepository.FindByIdAsync(id);

                if (foundEvent == null)
                {
                    operationResult.Complete(null);
                    return operationResult;
                }

                foundEvent.Name = data.Name;
                foundEvent.StartDate = data.StartDate;
                foundEvent.EndDate = data.EndDate;

                var updatedEvent = await _eventRepository.UpdateAsync(foundEvent);

                operationResult.Complete(updatedEvent);
            }

            return operationResult;
        }

        /// <summary>
        /// Finds all events
        /// </summary>
        /// <param name="pageIndex">Index of the page to load</param>
        /// <returns>Returns the paged resultset</returns>
        public async Task<PagedResultSet<EventSummary>> FindAllAsync(int pageIndex)
        {
            return await _eventRepository.FindAllAsync(pageIndex);
        }

        /// <summary>
        /// Finds a single event
        /// </summary>
        /// <param name="id">ID of the event</param>
        /// <returns>Returns the found event</returns>
        public async Task<Event> FindByIdAsync(int id)
        {
            return await _eventRepository.FindByIdAsync(id);
        }


    }
}