using System.Net;

namespace BloodDonationDb.Exceptions.ExceptionsBase;
public class ErrorOnValidationException : BloodDonationDbException
{
    public IList<string> Errors { get; private set; }

    public ErrorOnValidationException(IList<string> errors) : base(string.Empty)
    {
        Errors = errors;
    } 

    public override IList<string> GetErrorMessages() => Errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
