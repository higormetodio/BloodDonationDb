namespace BloodDonationDb.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        RegisterDate = DateTime.Now;
        Active = true;
    }

    public int Id { get; private set; }
    public DateTime RegisterDate { get; private set; }
    public bool Active { get; private set; }
    
    public void ToInactive()
    {
        Active = true;
    }
}