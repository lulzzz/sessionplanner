using Microsoft.AspNetCore.Mvc.ModelBinding;
using SessionPlanner.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SessionPlanner.API.Utility
{
    public static class ModelStateDictionaryExtensionMethods
    {
        /// <summary>
        /// Adds all the validation errors found in the operation result to the model state.
        /// </summary>
        /// <param name="modelState">Model state dictionary to update</param>
        /// <param name="operationResult">Operation result to read from</param>
        public static void AddFromOperationResult(this ModelStateDictionary modelState, OperationResult operationResult)
        {
            foreach(var key in operationResult.ValidationErrors.Keys)
            {
                foreach(var message in operationResult.ValidationErrors[key])
                {
                    modelState.AddModelError(key, message);
                }
            }
        }
    }
}
