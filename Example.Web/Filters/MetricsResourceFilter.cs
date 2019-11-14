using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Example.Web.Filters
{
    public class MetricsResourceFilter : IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var actionDescriptor = context.ActionDescriptor;

            if (actionDescriptor != null)
            {
                var actionName = ((ControllerActionDescriptor) context.ActionDescriptor).ActionName;
                var controllerName = ((ControllerActionDescriptor) context.ActionDescriptor).ControllerName;

                context.HttpContext.AddMetricsCurrentRouteName($"{controllerName} - {actionName}");
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {            
        }
    }
}