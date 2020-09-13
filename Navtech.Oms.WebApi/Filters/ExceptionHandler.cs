namespace Navtech.Oms.WebApi.Filters
{
    using System.Text;
    using System.Net.Http;
    using System.Web.Http.Filters;
    using FluentValidation;

    using Navtech.Oms.WebApi.Exceptions;
    using Navtech.Oms.Abstractions.Business.Exceptions;

    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // TODO: Create common object structure for exceptions messeges to be returned
            if (context.Exception is ValidationException validationException)
            {
                // Create Response with 512 HTTP status code for errors, this needs to be handled by API client
                context.Response = new HttpResponseMessage((System.Net.HttpStatusCode)512)
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(validationException.Errors), Encoding.UTF8, "application/json")
                };
            }
            else if (context.Exception is ModelStateException modelStateException)
            {
                // Create Response with 512 HTTP status code for errors, this needs to be handled by API client
                context.Response = new HttpResponseMessage((System.Net.HttpStatusCode)512)
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(modelStateException.ModelErrors), Encoding.UTF8, "application/json")
                };
            }
            else if (context.Exception is IBusinessException businessException)
            {
                // Create Response with 512 HTTP status code for errors, this needs to be handled by API client
                context.Response = new HttpResponseMessage((System.Net.HttpStatusCode)512)
                {
                    Content = new StringContent(businessException.Message, Encoding.UTF8, "text/html")
                };
            }
            
            else
            {
                context.Response = new HttpResponseMessage((System.Net.HttpStatusCode)512)
                {
                    Content = new StringContent("Error has occurred!", Encoding.UTF8, "text/html")
                };
            }
        }
    }
}
