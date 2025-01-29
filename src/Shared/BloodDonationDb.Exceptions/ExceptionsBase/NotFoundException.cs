using System.Net;

namespace BloodDonationDb.Exceptions.ExceptionsBase;
public class NotFoundException : BloodDonationDbException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages()
        => [Message];

    public override HttpStatusCode GetStatusCode()
        => HttpStatusCode.NotFound;
}
