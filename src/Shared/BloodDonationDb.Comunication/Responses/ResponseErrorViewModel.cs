namespace BloodDonationDb.Comunication.Responses;
public class ResponseErrorViewModel
{
    public IList<string> Errors { get; private set; }

    public ResponseErrorViewModel(IList<string> errors)
    {
        Errors = errors;
    }

    public ResponseErrorViewModel(string error)
    {
        Errors = new List<string> { error };
    }
}
