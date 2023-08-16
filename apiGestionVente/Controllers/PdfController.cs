using Microsoft.AspNetCore.Mvc;
using apiGestionVente.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        [HttpPost("generate")]
        public IActionResult GeneratePdf([FromBody] EmailRequest emailRequest)
        {
            MemoryStream memoryStream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            document.Open();

            AddHeader(document);
            AddContent(document, emailRequest);
            AddFooter(document);

            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            string fileName = $"Facture_{Guid.NewGuid()}.pdf";
            string filePath = Path.Combine("D:\\Facture", fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            MemoryStream pdfMemoryStream = new MemoryStream(bytes);
            return new FileStreamResult(pdfMemoryStream, "application/pdf");
        }

        private void AddHeader(Document document)
        {
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell cell = new PdfPCell(new Phrase("Facture d'achat", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.BLACK)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.AddCell(cell);

            document.Add(headerTable);

            AddEmptyLine(document, 2); // Ajouter des lignes vides
        }

        private void AddContent(Document document, EmailRequest emailRequest)
        {
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font contentFont = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);

            AddContentLine(document, $"Client : {emailRequest.client}", contentFont);
            AddContentLine(document, $"Voiture : {emailRequest.voiture}", contentFont);
            AddContentLine(document, $"Arrêté par la présente facture à la somme de : {emailRequest.somme} ariary", contentFont);
            AddContentLine(document, $"Date d'achat : {emailRequest.dateAchat}", contentFont);

            AddEmptyLine(document, 2);
        }

        private void AddContentLine(Document document, string content, Font font)
        {
            Paragraph paragraph = new Paragraph(content, font);
            paragraph.SpacingAfter = 5;
            document.Add(paragraph);
        }

        private void AddEmptyLine(Document document, int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                document.Add(new Paragraph(" "));
            }
        }

        private void AddFooter(Document document)
        {
            PdfPTable footerTable = new PdfPTable(1);
            footerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell cell = new PdfPCell(new Phrase($"Date de facture : {DateTime.Now}", new Font(Font.FontFamily.HELVETICA, 10,Font.BOLD, BaseColor.GRAY)));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            footerTable.AddCell(cell);

            document.Add(footerTable);
        }
    }
}
