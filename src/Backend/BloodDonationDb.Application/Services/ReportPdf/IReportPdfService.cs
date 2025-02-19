namespace BloodDonationDb.Application.Services.ReportPdf;
public interface IReportPdfService<T>
{
    public byte[] GenerateReportPdf(string reportType, List<T> list);
}
