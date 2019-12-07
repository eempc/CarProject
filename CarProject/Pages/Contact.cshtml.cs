using CarProject.Helpers;
using CarProject.Protected;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CarProject {
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactFormModel Contact { get; set; }

        public string Message { get; set; }
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            // Azure key vault password retrieval happens here, it did not work with OnGetAsync(), the global variable did not hold the password
            string password = "";

            try {
                AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                SecretBundle secret = await keyVaultClient.GetSecretAsync(Emails.keyVaultUrl).ConfigureAwait(false);
                password = secret.Value;
            } catch (KeyVaultErrorException e) {
                Message = e.Message;
            }

            if (string.IsNullOrEmpty(password)) {
                Message = "Website is experiencing technical difficulties.";
                return Page();
            }

            // Initiating the mail and sending it

            string contactName = Contact.Name;
            string subject = $"Message from {contactName} with subject: {Contact.Subject}";
            string body = Contact.Message;

            await SendMail.SendGmail(contactName, subject, body, password);

            //MailAddress fromAddress = new MailAddress(Emails.fromEmailAddress, "No Reply");
            //MailAddress toAddress = new MailAddress(Emails.toEmailAddress);

            //SmtpClient smtp = new SmtpClient {
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromAddress.Address, password)
            //};

            //using MailMessage msg = new MailMessage(fromAddress, toAddress) {
            //    Subject = $"Message from {Contact.Name} with subject: {Contact.Subject}",
            //    Body = Contact.Message
            //};

            //await smtp.SendMailAsync(msg);
            //msg.Dispose(); // Probably isn't necessary
            //smtp.Dispose();

            return RedirectToPage("Index");
        }
    }

    public class ContactFormModel {
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public string Subject { get; set; }
    }

}