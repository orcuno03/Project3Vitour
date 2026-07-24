using ClosedXML.Excel;
using Project3Vitour.Dtos.ReservationDtos;
using Project3Vitour.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Project3Vitour.Services.ExportServices
{
    public class ReservationExportService : IReservationExportService
    {
        private static readonly string[] Headers =
        {
            "#", "Ad Soyad", "E-posta", "Telefon", "Kişi", "Rezervasyon Tarihi", "Durum", "Not"
        };

        // Kontenjan mantigiyla ayni olsun diye iptaller haric tutulur
        private static int AktifKisiSayisi(List<ResultReservationByTourIdDto> reservations) =>
            reservations.Where(x => x.ReservationStatus != ReservationStatuses.Cancelled)
                        .Sum(x => x.PersonCount);

        public byte[] GenerateExcel(string tourTitle, List<ResultReservationByTourIdDto> reservations)
        {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Rezervasyonlar");

            // Baslik satiri
            sheet.Cell(1, 1).Value = $"{tourTitle} — Rezervasyon Listesi";
            sheet.Range(1, 1, 1, Headers.Length).Merge();
            sheet.Cell(1, 1).Style.Font.SetBold().Font.SetFontSize(14);
            sheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            sheet.Cell(2, 1).Value = $"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm} — Toplam {reservations.Count} kayıt, {AktifKisiSayisi(reservations)} kişi (iptaller hariç)";
            sheet.Range(2, 1, 2, Headers.Length).Merge();
            sheet.Cell(2, 1).Style.Font.SetItalic().Font.SetFontColor(XLColor.Gray);
            sheet.Cell(2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Sutun basliklari
            const int headerRow = 4;
            for (int i = 0; i < Headers.Length; i++)
            {
                var cell = sheet.Cell(headerRow, i + 1);
                cell.Value = Headers[i];
                cell.Style.Font.SetBold().Font.SetFontColor(XLColor.White);
                cell.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#2D9CDB"));
                cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            // Veri satirlari
            var row = headerRow + 1;
            for (int i = 0; i < reservations.Count; i++)
            {
                var r = reservations[i];
                sheet.Cell(row, 1).Value = i + 1;
                sheet.Cell(row, 2).Value = r.NameSurname;
                sheet.Cell(row, 3).Value = r.Email;
                sheet.Cell(row, 4).Value = r.Phone;
                sheet.Cell(row, 5).Value = r.PersonCount;
                sheet.Cell(row, 6).Value = r.ReservationDate.ToString("dd.MM.yyyy");
                sheet.Cell(row, 7).Value = r.ReservationStatus;
                sheet.Cell(row, 8).Value = r.Note;
                row++;
            }

            // Toplam kisi satiri (iptaller haric)
            if (reservations.Count > 0)
            {
                sheet.Cell(row, 4).Value = "Toplam Kişi (iptaller hariç):";
                sheet.Cell(row, 4).Style.Font.SetBold();
                sheet.Cell(row, 5).Value = AktifKisiSayisi(reservations);
                sheet.Cell(row, 5).Style.Font.SetBold();
            }

            var lastDataRow = Math.Max(row, headerRow + 1);
            sheet.Range(headerRow, 1, lastDataRow, Headers.Length).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            sheet.Range(headerRow, 1, lastDataRow, Headers.Length).Style.Border.SetInsideBorder(XLBorderStyleValues.Hair);
            sheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] GeneratePdf(string tourTitle, List<ResultReservationByTourIdDto> reservations)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(28);
                    page.DefaultTextStyle(x => x.FontSize(9).FontFamily(Fonts.Calibri));

                    page.Header().Column(col =>
                    {
                        col.Item().Text(tourTitle)
                            .FontSize(16).Bold().FontColor("#0D1B2A");
                        col.Item().Text("Rezervasyon Listesi")
                            .FontSize(11).FontColor("#2D9CDB");
                        col.Item().PaddingTop(2)
                            .Text($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm} — Toplam {reservations.Count} kayıt, {AktifKisiSayisi(reservations)} kişi (iptaller hariç)")
                            .FontSize(8).FontColor(Colors.Grey.Darken1);
                        col.Item().PaddingTop(8).LineHorizontal(1).LineColor("#2D9CDB");
                    });

                    page.Content().PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(24);  // #
                            columns.RelativeColumn(2);   // Ad Soyad
                            columns.RelativeColumn(2.4f);// E-posta
                            columns.RelativeColumn(1.5f);// Telefon
                            columns.ConstantColumn(35);  // Kisi
                            columns.RelativeColumn(1.3f);// Tarih
                            columns.RelativeColumn(1.4f);// Durum
                            columns.RelativeColumn(2.2f);// Not
                        });

                        table.Header(header =>
                        {
                            foreach (var h in Headers)
                            {
                                header.Cell()
                                      .Background("#2D9CDB")
                                      .Padding(5)
                                      .Text(h).Bold().FontColor(Colors.White).FontSize(8.5f);
                            }
                        });

                        for (int i = 0; i < reservations.Count; i++)
                        {
                            var r = reservations[i];
                            var bg = i % 2 == 0 ? "#FFFFFF" : "#F4F6FB";

                            void Cell(string text) =>
                                table.Cell().Background(bg).BorderBottom(0.5f).BorderColor("#E2E8F0")
                                     .Padding(5).Text(text ?? "-");

                            Cell((i + 1).ToString());
                            Cell(r.NameSurname);
                            Cell(r.Email);
                            Cell(r.Phone);
                            Cell(r.PersonCount.ToString());
                            Cell(r.ReservationDate.ToString("dd.MM.yyyy"));
                            Cell(r.ReservationStatus);
                            Cell(string.IsNullOrWhiteSpace(r.Note) ? "-" : r.Note);
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Vitour Yönetim Paneli — ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.Span(" / ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        x.TotalPages().FontSize(8).FontColor(Colors.Grey.Darken1);
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
