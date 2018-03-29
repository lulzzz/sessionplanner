using Microsoft.AspNetCore.Mvc.ModelBinding;
using SessionPlanner.Domain;

namespace SessionPlanner.Utility
{
    public static class ModelStateExtensionMethods
    {
        public static void AddFromOperationResult(this ModelStateDictionary modelState, ServiceOperationResult operationResult)
        {
            foreach (var key in operationResult.ValidationErrors.Keys)
            {
                foreach (var message in operationResult.ValidationErrors[key])
                {
                    modelState.AddModelError(key, message);
                }
            }
        }
    }
}