

using Azure;
using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.UniversalAccessibility.Drawing;

namespace BloodDonationDb.Application.Services.ReportPdf;
public class ReportPdfService<T> : IReportPdfService<T> where T : class
{
    public byte[] GenerateReportPdf(string reportType, List<T> list)
    {
        PdfDocument pdfDocument = new PdfDocument();

        PdfPage page = pdfDocument.AddPage();

        XGraphics gfx = XGraphics.FromPdfPage(page);

        XFont font = new XFont("Verdana", 18, XFontStyleEx.Bold);

        gfx.DrawString($"Relatório de {reportType}",
            font,
            XBrushes.Black,
            new XRect(0, 20, page.Width!, page.Height!),
            new XStringFormat
            {
                Alignment = XStringAlignment.Center,
            });

        if (list.Any(l => l.GetType() == typeof(BloodStock)))
        {
            var listBloodStock = list.Cast<BloodStock>().ToList();

            int height = 60;

            height = CreatePdfStructureToBloodStock(BloodType.A, listBloodStock, gfx, page, height);
            height = CreatePdfStructureToBloodStock(BloodType.B, listBloodStock, gfx, page, height);
            height = CreatePdfStructureToBloodStock(BloodType.AB, listBloodStock, gfx, page, height);
            CreatePdfStructureToBloodStock(BloodType.O, listBloodStock, gfx, page, height);
        }

        if (list.Any(l => l.GetType() == typeof(DonationDonor)))
        {
            var listDonationDonors = list.Cast<DonationDonor>().ToList();

            int height = 60;

            CreatePdfStructureToDonationDonor(listDonationDonors, gfx, page, height);
        }

        using MemoryStream stream = new MemoryStream();
        pdfDocument.Save(stream, false);

        return stream.ToArray();
    }

    private int CreatePdfStructureToBloodStock(BloodType bloodType, List<BloodStock> listBloodStock, XGraphics gfx, PdfPage page, int height)
    {
        gfx.DrawString(
            $"Tipo Sanguíneo {bloodType}",
            new XFont("Verdana", 12, XFontStyleEx.BoldItalic),
            XBrushes.Black,
        new XRect(20, height, page.Width, page.Height),
        new XStringFormat()
        {
            LineAlignment = XLineAlignment.Near,
            Alignment = XStringAlignment.Near
        });

        height += 30;

        foreach (var bloodStock in listBloodStock)
        {
            if (bloodStock.BloodType == bloodType)
            {
                gfx.DrawString(
                    $"Fator RH:",
                    new XFont("Verdana", 12, XFontStyleEx.Bold),
                    XBrushes.Black,
                new XRect(20, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

                gfx.DrawString(
                    $"{bloodStock.RhFactor}",
                    new XFont("Verdana", 12, XFontStyleEx.Regular),
                    XBrushes.Black,
                new XRect(90, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

                gfx.DrawString(
                    $"Quantidade em ML:",
                    new XFont("Verdana", 12, XFontStyleEx.Bold),
                    XBrushes.Black,
                new XRect(160, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

                gfx.DrawString(
                   $"{bloodStock.Quantity}",
                   new XFont("Verdana", 12, XFontStyleEx.Regular),
                   XBrushes.Black,
               new XRect(295, height, page.Width, page.Height),
               new XStringFormat()
               {
                   LineAlignment = XLineAlignment.Near,
                   Alignment = XStringAlignment.Near
               });

                gfx.DrawString(
                    $"Quantidade Mínima Atingida:",
                    new XFont("Verdana", 12, XFontStyleEx.Bold),
                    XBrushes.Black,
                new XRect(350, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

                if (bloodStock.MinimumQuantityReached)
                {
                    gfx.DrawString(
                        $"{bloodStock.MinimumQuantityReached}",
                        new XFont("Verdana", 12, XFontStyleEx.Bold),
                        XBrushes.Red,
                        new XRect(550, height, page.Width, page.Height),
                        new XStringFormat()
                        {
                            LineAlignment = XLineAlignment.Near,
                            Alignment = XStringAlignment.Near
                        });
                }
                else
                {
                    gfx.DrawString(
                        $"{bloodStock.MinimumQuantityReached}",
                        new XFont("Verdana", 12, XFontStyleEx.Regular),
                        XBrushes.Black,
                        new XRect(550, height, page.Width, page.Height),
                        new XStringFormat()
                        {
                            LineAlignment = XLineAlignment.Near,
                            Alignment = XStringAlignment.Near
                        });
                }


                height += 30;
            }
        }

        return height + 30;
    }

    private void CreatePdfStructureToDonationDonor(List<DonationDonor> listDonationDonors, XGraphics gfx, PdfPage page, int height)
    {
        foreach (var donation in listDonationDonors)
        {
            gfx.DrawString(
                $"Data:",
                new XFont("Verdana", 12, XFontStyleEx.Bold),
                XBrushes.Black,
                new XRect(20, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            gfx.DrawString(
                $"{donation.When.ToString("d")}",
                new XFont("Verdana", 12, XFontStyleEx.Regular),
                XBrushes.Black,
                new XRect(60, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            height += 20;

            gfx.DrawString(
                $"Doador:",
                new XFont("Verdana", 12, XFontStyleEx.Bold),
                XBrushes.Black,
                new XRect(70, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            gfx.DrawString(
                $"{donation.Donor!.Name}",
                new XFont("Verdana", 12, XFontStyleEx.Regular),
                XBrushes.Black,
                new XRect(130, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            gfx.DrawString(
                $"Tipo:",
                new XFont("Verdana", 12, XFontStyleEx.Bold),
                XBrushes.Black,
                new XRect(395, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            gfx.DrawString(
               $"{donation.BloodStock.BloodType} - {donation.BloodStock.RhFactor}",
               new XFont("Verdana", 12, XFontStyleEx.Regular),
               XBrushes.Black,
               new XRect(435, height, page.Width, page.Height),
               new XStringFormat()
               {
                   LineAlignment = XLineAlignment.Near,
                   Alignment = XStringAlignment.Near
               });

            height += 20;

            gfx.DrawString(
                $"E-mail:",
                new XFont("Verdana", 12, XFontStyleEx.Bold),
                XBrushes.Black,
                new XRect(70, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            gfx.DrawString(
                $"{donation.Donor.Email}",
                new XFont("Verdana", 12, XFontStyleEx.Regular),
                XBrushes.Black,
                new XRect(125, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            gfx.DrawString(
                $"Quantidade:",
                new XFont("Verdana", 12, XFontStyleEx.Bold),
                XBrushes.Black,
                new XRect(395, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                });

            gfx.DrawString(
              $"{donation.Quantity} ml",
              new XFont("Verdana", 12, XFontStyleEx.Regular),
              XBrushes.Black,
              new XRect(480, height, page.Width, page.Height),
              new XStringFormat()
              {
                   LineAlignment = XLineAlignment.Near,
                   Alignment = XStringAlignment.Near
              });

            height += 40;
        }
    }
}
