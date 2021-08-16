using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Presentation.Infrastructure.MVC.CustomObjectResults
{
    public class InvalidModelStateObjectResult : ObjectResult
    {
        public InvalidModelStateObjectResult(ModelStateDictionary modelState)
        : base(ModelStateExtensions.GetErrors(modelState))
        {
            if (modelState == null)
                throw new ArgumentNullException(nameof(modelState));
            StatusCode = 422;
        }
        private class ModelStateExtensions
        {
            public static object GetErrors(ModelStateDictionary keyValues)
            {
                var errors = new Hashtable();
                foreach (var pair in keyValues)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                return new { errors };
            }
        }
    }
}
