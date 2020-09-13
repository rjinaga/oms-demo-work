namespace Navtech.Oms.WebApi.Filters
{
    using System.Net;
    using System.Web.Mvc;

    public class HttpStatusContentResult : ActionResult
    {
        readonly string _content;
        readonly HttpStatusCode _statusCode;
        readonly string _statusDescription;
        readonly string _contentType;

        public HttpStatusContentResult(string content,
                                       HttpStatusCode statusCode = HttpStatusCode.OK,
                                       string statusDescription = null,
                                       string contentType = "text/html")
        {
            _content = content;
            _statusCode = statusCode;
            _statusDescription = statusDescription;
            _contentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = (int)_statusCode;
            response.ContentType = _contentType;

            if (_statusDescription != null)
            {
                response.StatusDescription = _statusDescription;
            }

            if (_content != null)
            {
                context.HttpContext.Response.Write(_content);
            }
        }
    }
}
