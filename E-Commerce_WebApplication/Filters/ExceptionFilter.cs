using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace E_Commerce_WebApplication.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilterAttribute> _logger;

        public ExceptionFilter(ILogger<ExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            context.Result = new ViewResult
            {
                ViewName = "Error", 
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    Model = "An unexpected error occurred."
                }
            };

            context.ExceptionHandled = true;
        }
    }
}
