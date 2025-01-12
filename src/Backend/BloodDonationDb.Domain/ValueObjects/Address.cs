namespace BloodDonationDb.Domain.ValueObjects;

public record Address (string Street, string Number, string City, string State, string ZipCode, string Country);