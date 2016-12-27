using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using TMP.BNK.Core;
using TMP.BNK.Model;

namespace TMP.BNK.API.Filters
{
    public class ErrorCodeMapperAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            OperationResponse response;
            actionContext.Response.TryGetContentValue(out response);
            if (response != null && !response.IsSuccess)
            {
                switch (response.ErrorCode)
                {
                    case ErrorCodes.DEPOSIT_LIMIT_EXCEEDED:
                        response.Message = ErrorCodes.DEPOSIT_LIMIT_EXCEEDED_MESSAGE;
                        break;
                    case ErrorCodes.MINIMUM_BALANCE_EXCEEDED:
                        response.Message = ErrorCodes.MINIMUM_BALANCE_EXCEEDED_MESSAGE;
                        break;
                    case ErrorCodes.WITHDRAW_LIMIT_EXCEEDED:
                        response.Message = ErrorCodes.WITHDRAW_LIMIT_EXCEEDED_MESSAGE;
                        break;
                }

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
    }
}