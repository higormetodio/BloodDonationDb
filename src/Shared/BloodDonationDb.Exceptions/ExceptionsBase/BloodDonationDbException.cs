using System.Net;

namespace BloodDonationDb.Exceptions.ExceptionsBase;
public abstract class BloodDonationDbException : SystemException
{
    protected BloodDonationDbException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();

    public abstract HttpStatusCode GetStatusCode();
}
