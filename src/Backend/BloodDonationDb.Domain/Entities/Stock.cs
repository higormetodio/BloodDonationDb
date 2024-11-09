namespace BloodDonationDb.Domain.Entities;

public class Stock
{
    public Stock(int bloodId, int quantity)
    {
        BloodId = bloodId;
        Quantity = quantity;
    }

    public int BloodId { get; private set; }
    public Blood Blood { get; private set; }
    public int Quantity { get; private set; }

    public void UpdateQuantity(int quantity)
    {
        Quantity += quantity;
    }
}