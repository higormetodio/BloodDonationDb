using BloodDonationDb.API.Attributes;
using BloodDonationDb.Application.Services.ReportPdf;
using BloodDonationDb.Comunication.Requests;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.BloodStock;
using BloodDonationDb.Domain.Repositories.DonationDonor;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationDb.API.Controllers;

[AuthenticatedUser]
public class ReportsController : MyBloodDonationDbController
{
    private readonly IReportPdfService<BloodStock> _reportPdfBloodStockService;
    private readonly IBloodStockReadOnlyRepository _bloodStockRepository;
    private readonly IReportPdfService<DonationDonor> _reportPdfDonationDonorService;
    private readonly IDonationDonorReadOnlyRepository _donationDonorReadOnlyRepository;

    public ReportsController(
        IReportPdfService<BloodStock> reportPdfBloodStockService,
        IBloodStockReadOnlyRepository bloodStockRepository,
        IReportPdfService<DonationDonor> reportPdfDonationDonorService,
        IDonationDonorReadOnlyRepository donationDonorReadOnlyRepository)
    {
        _reportPdfBloodStockService = reportPdfBloodStockService;
        _bloodStockRepository = bloodStockRepository;
        _reportPdfDonationDonorService = reportPdfDonationDonorService;
        _donationDonorReadOnlyRepository = donationDonorReadOnlyRepository;
    }

    [HttpGet("blood-stock")]
    public async Task<IActionResult> GetReportBloodStock()
    {
        var listBloodStock = await _bloodStockRepository.GetAllBloodStocksAsync();

        var pdfBytes = _reportPdfBloodStockService.GenerateReportPdf("Estoque de Sangue por Tipo", listBloodStock.ToList());

        return File(pdfBytes, "application/pdf", "RelatorioBancoSanguePorTipo.pdf");
    }

    [HttpGet("donation-donor")]
    public async Task<IActionResult> GetReportDonationDonor([FromBody]DonationDateJson date)
    {
        var listDonationsDonor = await _donationDonorReadOnlyRepository.GetDonationDonorsByDate(date.StartDate, date.FinishDate);

        var pdfBytes = _reportPdfDonationDonorService.GenerateReportPdf($"Doações de {date.StartDate.ToString("d")} a {date.FinishDate.ToString("d")}", listDonationsDonor.ToList());

        return File(pdfBytes, "application/pdf", "RelatorioDoacoesPorData.pdf");
    }
}
