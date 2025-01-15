using System.Net;

namespace BloodDonationDb.Exceptions.ExceptionsBase;
public class InvalidLoginException() : BloodDonationDbException(ResourceMessageException.EMAIL_OR_PASSWORD_INVALID)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
