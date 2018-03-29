using System.Collections.Generic;

namespace SessionPlanner.Domain
{
    public class ServiceOperationResult
    {

        public ServiceOperationResult()
        {
            ValidationErrors = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> ValidationErrors { get; }
        public bool IsValid => ValidationErrors.Count == 0;

        public void AddValidationError(string key, string message)
        {
            if (ValidationErrors.TryGetValue(key, out var errors))
            {
                errors.Add(message);
            }
            else
            {
                ValidationErrors.Add(key, new List<string>(new[] { message }));
            }
        }
    }

    public class ServiceOperationResult<T>: ServiceOperationResult where T : class
    {
        public ServiceOperationResult()
        {

        }


        public T Result { get; private set; }
        
        public ServiceOperationResult<T> Complete(T result)
        {
            Result = result;
            return this;
        }
    }
}