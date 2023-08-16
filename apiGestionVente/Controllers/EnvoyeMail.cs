using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using apiGestionVente.Model;

namespace apiGestionVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] EmailRequest emailRequest)
        {
            string expediteur = "elyse.rafano1844@gmail.com";
            string motDePasse = "zjdiwpbralgcxtqg";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(expediteur, motDePasse),
                EnableSsl = true
            };

            string sujet = "Facture d'achat";
            string contenu = GenerateFactureContent(emailRequest);

            MailMessage message = new MailMessage(expediteur, emailRequest.adresseDestinataire)
            {
                Subject = sujet,
                Body = contenu
            };

            try
            {
                smtpClient.Send(message);
                return Ok("L'e-mail a été envoyé avec succès !");
            }
            catch (Exception e)
            {
                return BadRequest("Erreur lors de l'envoi de l'e-mail : " + e.Message);
            }
        }

        private string GenerateFactureContent(EmailRequest emailRequest)
        {
            string factureContent = $"Facture d'achat\n\n" +
                                    $"Client : {emailRequest.client}\n" +
                                    $"Voiture : {emailRequest.voiture}\n" +
                                    $"Somme : {emailRequest.somme}\n" +
                                    $"Date d'achat : {emailRequest.dateAchat}\n" +
                                    $"Date d'émission : {DateTime.Now}\n";

            return factureContent;
        }
    }

   
}

