using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace OleSync.API.Shared
{
    public class WebResponse<T> : IActionResult
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public T? Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            var data = new
            {
                Data,
                StatusCode = (int)Status,
                ErrorMessage
            };
            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = (int)Status;
            JsonResult x = new(data)
            {
                StatusCode = (int)Status,
                ContentType = "application/json"
            };
            await x.ExecuteResultAsync(context);
        }

        public WebResponse(string error, HttpStatusCode errorStatus)
        {
            ErrorMessage = error;
            Status = errorStatus;
        }

        public WebResponse(ModelStateDictionary modelstate)
        {
            if (modelstate.Values.FirstOrDefault(a => a.Errors.Any()) != null)
                ErrorMessage = modelstate.Values.FirstOrDefault(a => a.Errors.Any()).Errors[0].ErrorMessage;

            Status = string.IsNullOrEmpty(ErrorMessage) ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
        }

        public WebResponse(T data)
        {
            ErrorMessage = string.Empty;
            Status = HttpStatusCode.OK;
            Data = data;
        }
    }

}
