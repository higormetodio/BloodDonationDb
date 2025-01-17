using BloodDonationDb.Comunication.Responses;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BloodDonationDb.API.Filter;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BloodDonationDbException bloodDonationDbException)
        {
            HandleProjectException(bloodDonationDbException, context);
        }
        else
        {
            ThrowUnknowException(context);
        }
    }

    private static void HandleProjectException(BloodDonationDbException blooddonationDbException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)blooddonationDbException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseError(blooddonationDbException.GetErrorMessages()));
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseError(ResourceMessageException.UNKNOW_ERROR));
    }

}
